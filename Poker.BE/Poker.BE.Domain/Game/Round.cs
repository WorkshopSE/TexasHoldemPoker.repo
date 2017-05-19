using Poker.BE.Domain.Utility.Exceptions;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Round
    {

        #region Enums
        public enum Move
        {
            check,
            call,
            bet,
            fold,
            raise,
            allin
        }
        #endregion

        #region fields
        private ICollection<Player> activeUnfoldedPlayers;
        private Turn currentTurn;
        //private Player dealer;        Is dealer needed??
        private Player currentPlayer;
        private Pot currentPot;
        private Dictionary<Player, int> liveBets;
        private int totalRaise;
        private int lastRaise;
        #endregion

        #region Properties
        public ICollection<Player> ActiveUnfoldedPlayers { get { return activeUnfoldedPlayers; } }
        public Turn CurrentTurn { get { return currentTurn; } }
        public Player CurrentPlayer { get { return currentPlayer; } }
        public Pot CurrentPot { get { return currentPot; } }
        public Dictionary<Player, int> LiveBets { get { return liveBets; } }
        public int TotalRaise { get { return totalRaise; } }
        public int LastRaise { get { return lastRaise; } }

        #endregion

        #region Constructors
        public Round(Player currentPlayer, ICollection<Player> activeUnfoldedPlayers, Pot currentPot)
        {
            this.currentPlayer = currentPlayer;
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.currentPot = currentPot;
            this.currentTurn = new Turn(currentPlayer, currentPot);
            this.liveBets = new Dictionary<Player, int>();
            foreach (Player player in activeUnfoldedPlayers)
            {
                LiveBets.Add(player, 0);
            }
            this.totalRaise = 0;
            this.lastRaise = 0;
        }
        #endregion

        #region Methods
        public void PlayMove(Move playMove, int amountToBetOrCall)
        {
            switch (playMove)
            {
                case Move.check:
                    {
                        CurrentTurn.Check();  // Do nothing??
                        break;
                    }
                case Move.call:
                    {
                        Call(amountToBetOrCall);
                        break;
                    }
                case Move.fold:
                    {
                        CurrentTurn.Fold();
                        Player playerToRemove = this.CurrentPlayer;

                        //Remover player from all pots
                        Pot partialPotIterator = CurrentPot;
                        while (partialPotIterator != null)
                        {
                            partialPotIterator.PlayersClaimPot.Remove(playerToRemove);
                            partialPotIterator = partialPotIterator.PartialPot;
                        }
                        
                        //Remove player from round
                        this.currentPlayer = this.ActiveUnfoldedPlayers.ElementAt((ActiveUnfoldedPlayers.ToList().IndexOf(this.CurrentPlayer) - 1) % ActiveUnfoldedPlayers.Count);
                        ActiveUnfoldedPlayers.Remove(playerToRemove);
                        return;
                        //break;
                    }
                case Move.bet:
                    {
                        //Check preconditions
                        if (LastRaise > 0 || TotalRaise > 0)
                            throw new IOException("Can't bet if someone had bet before... use raise move");
                        if (TotalRaise + amountToBetOrCall == CurrentPlayer.Wallet.amountOfMoney + LiveBets[CurrentPlayer])
                            throw new ArgumentOutOfRangeException("Can't bet all of your money... use all-in move");
                        RaisePreconditions(amountToBetOrCall);

                        //Make bet
                        Raise(amountToBetOrCall);
                        
                        break;
                    }
                case Move.raise:
                    {
                        //Check preconditions
                        if (LastRaise == 0)
                            throw new IOException("Can't raise if no one had bet before... use bet move");
                        if (TotalRaise + amountToBetOrCall == CurrentPlayer.Wallet.amountOfMoney + LiveBets[CurrentPlayer])
                            throw new ArgumentOutOfRangeException("Can't bet all of your money... use all-in move");
                        RaisePreconditions(amountToBetOrCall);

                        //Make Raise
                        Call(0);                  //First call whatever you need to call
                        Raise(amountToBetOrCall); //Than raise
                        break;
                    }
                case Move.allin:
                    {
                        //Check preconditions
                        int highestOtherAllIn = 0;
                        foreach (Player player in ActiveUnfoldedPlayers)    //find highest all-in of the other players at the table
                        {
                            if (player != this.CurrentPlayer && player.Wallet.amountOfMoney + LiveBets[player] > highestOtherAllIn)
                                highestOtherAllIn = player.Wallet.amountOfMoney;
                        }
                        if (this.CurrentPlayer.Wallet.amountOfMoney > highestOtherAllIn)
                            throw new IOException("all-in is bigger than the highest other player's all-in... use bet\raise move");
                        if (CurrentPlayer.Wallet.amountOfMoney == 0)
                            throw new ArgumentException("You're already all-in!!");

                        //Make all-in
                        if (this.CurrentPlayer.Wallet.amountOfMoney + LiveBets[currentPlayer] <= TotalRaise)
                            Call(this.CurrentPlayer.Wallet.amountOfMoney);
                        else
                        {
                            Call(0);
                            if (LiveBets[currentPlayer] != totalRaise)
                                throw new ArgumentException("Didn't call right!");
                            RaisePreconditions(this.CurrentPlayer.Wallet.amountOfMoney + LiveBets[CurrentPlayer] - TotalRaise);
                            Raise(this.CurrentPlayer.Wallet.amountOfMoney + LiveBets[CurrentPlayer] - TotalRaise);

                        }
                        CurrentTurn.AllIn();



                        break;
                    }
                default:
                    {
                        //TODO: print invalid move exception
                        throw new ArgumentException("Invalid Move");
                    }
            }
            //Change Player
            CalculateNextPlayer();
            CurrentTurn.CurrentPlayer = this.CurrentPlayer;
        }
        private void CalculateNextPlayer()
        {
            do
                this.currentPlayer = this.ActiveUnfoldedPlayers.ElementAt((ActiveUnfoldedPlayers.ToList().IndexOf(this.CurrentPlayer) + 1) % ActiveUnfoldedPlayers.Count);
            while (this.CurrentPlayer.CurrentState == Player.State.ActiveAllIn);

        }

        private void Call(int amountToBetOrCall)
        {
            if (amountToBetOrCall == 0)
                amountToBetOrCall = TotalRaise - LiveBets[CurrentPlayer];
            int playerCurrentBet = LiveBets[CurrentPlayer];

            Pot partialPotIterator = CurrentPot;
            Pot lastPartialPot = partialPotIterator;
            int amountToAdd = amountToBetOrCall;    //how much money does the player need to add in order to claim the pot
            int lastPlayerCurrentBet = 0;

            while (partialPotIterator != null && partialPotIterator.AmountToClaim > 0 && amountToBetOrCall > 0)
            {
                if (playerCurrentBet < partialPotIterator.AmountToClaim)
                {
                    if (amountToBetOrCall + playerCurrentBet >= partialPotIterator.AmountToClaim) //if regular call
                        amountToAdd = partialPotIterator.AmountToClaim - playerCurrentBet;
                    else    //if call all-in
                        amountToAdd = amountToBetOrCall;
                    CurrentTurn.Call(amountToAdd);
                    LiveBets[CurrentPlayer] += amountToAdd;
                    partialPotIterator.Value += amountToAdd;
                    amountToBetOrCall -= amountToAdd;
                    lastPlayerCurrentBet = playerCurrentBet;
                    playerCurrentBet = 0;
                    
                }
                else
                {
                    playerCurrentBet -= partialPotIterator.AmountToClaim;
                }

                //Add the current player to claiming this partial pot
                if (!partialPotIterator.PlayersClaimPot.Contains(CurrentPlayer))
                    partialPotIterator.PlayersClaimPot.Add(CurrentPlayer);

                lastPartialPot = partialPotIterator;
                partialPotIterator = partialPotIterator.PartialPot;
            }
            
            if (amountToBetOrCall + playerCurrentBet > lastPartialPot.AmountToClaim)
                throw new ArgumentException("Not enough partial pots were created! Somthing isn't right!");
            
            if (CurrentPlayer.Wallet.amountOfMoney == 0) //if call all-in move
            {
                //Add new partial pot in the middle
                Pot newPartialPot = new Pot(lastPartialPot.BasePot);
                newPartialPot.PartialPot = lastPartialPot;
                lastPartialPot.BasePot = newPartialPot;
                if (newPartialPot.BasePot != null)
                    newPartialPot.BasePot.PartialPot = newPartialPot;
                else
                    this.currentPot = newPartialPot;
                
                //Update partial pot's fields
                foreach (Player player in lastPartialPot.PlayersClaimPot)
                    newPartialPot.PlayersClaimPot.Add(player);
                lastPartialPot.PlayersClaimPot.Remove(CurrentPlayer);

                newPartialPot.AmountToClaim = amountToAdd + lastPlayerCurrentBet;
                lastPartialPot.AmountToClaim -= newPartialPot.AmountToClaim;
                newPartialPot.Value = lastPartialPot.Value - (lastPartialPot.AmountToClaim * lastPartialPot.PlayersClaimPot.Count);
                lastPartialPot.Value = lastPartialPot.AmountToClaim * lastPartialPot.PlayersClaimPot.Count;

                
            }
        }

        private void RaisePreconditions(int amountToRaise)
        {
            if (LastRaise > amountToRaise)
                throw new ArgumentException("Can't raise less than last raise");
            int highestAllIn = 0;
            foreach (Player player in ActiveUnfoldedPlayers)    //find highest all-in at the table
            {
                if (player.Wallet.amountOfMoney + LiveBets[player] > highestAllIn)
                    highestAllIn = player.Wallet.amountOfMoney + LiveBets[player];
            }
            if (TotalRaise + amountToRaise > highestAllIn)
                throw new ArgumentOutOfRangeException("Raise is bigger than the highest player's all-in");
        }

        private void Raise(int amountToRaise)
        {
            Pot lastPartialPot = currentPot;
            while (lastPartialPot.PartialPot != null)
                lastPartialPot = lastPartialPot.PartialPot;

            lastPartialPot.Value += amountToRaise;
            lastPartialPot.AmountToClaim += amountToRaise;
            CurrentTurn.Bet(amountToRaise);
            LiveBets[CurrentPlayer] += amountToRaise;
            lastRaise = amountToRaise;
            totalRaise += LastRaise;

            //reset the players claiming this pot
            lastPartialPot.PlayersClaimPot = new List<Player>();
            lastPartialPot.PlayersClaimPot.Add(CurrentPlayer);

            if (currentPlayer.Wallet.amountOfMoney == 0)
                lastPartialPot.PartialPot = new Pot(lastPartialPot);
        }
        #endregion
    }
}