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
        public void PlayMove(Move playMove, int amountToBet)
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
                        //----------Put money in pot----------//
                        //Pot partialPotToCheck = CurrentPot;
                        int amountToCall = TotalRaise - LiveBets[CurrentPlayer];
                        int playerCurrentBet = LiveBets[CurrentPlayer];

                        //as long as the player can "fill" this pot, put in the needed amount and mov to the next partial pot
                        Pot lastPartialPot = FillBasePots(ref amountToCall, ref playerCurrentBet);



                        if (amountToCall + playerCurrentBet > lastPartialPot.AmountToClaim)
                            throw new ArgumentException("Not enough partial pots were created! Somthing isn't right!");
                        else if (amountToCall + playerCurrentBet < lastPartialPot.AmountToClaim) //if call all-in move
                        {
                            //Add new partial pot in the middle
                            Pot newPartialPot = new Pot(lastPartialPot.BasePot);
                            newPartialPot.PartialPot = lastPartialPot;
                            lastPartialPot.BasePot = newPartialPot;
                            newPartialPot.BasePot.PartialPot = newPartialPot;

                            //Set new partial pot fields
                            foreach (Player player in lastPartialPot.PlayersClaimPot)
                                newPartialPot.PlayersClaimPot.Add(player);
                            newPartialPot.PlayersClaimPot.Add(CurrentPlayer);
                            newPartialPot.AmountToClaim = amountToCall + playerCurrentBet;
                            newPartialPot.Value = newPartialPot.PlayersClaimPot.Count * newPartialPot.AmountToClaim;

                            //Update last partial pot fields
                            lastPartialPot.AmountToClaim -= newPartialPot.AmountToClaim;
                            lastPartialPot.Value -= newPartialPot.Value;
                        }
                        CurrentTurn.Call(amountToCall);

                        break;
                    }
                case Move.fold:
                    {
                        CurrentTurn.Fold();
                        Player playerToRemove = this.CurrentPlayer;
                        this.currentPlayer = this.ActiveUnfoldedPlayers.ElementAt((ActiveUnfoldedPlayers.ToList().IndexOf(this.CurrentPlayer) - 1) % ActiveUnfoldedPlayers.Count);
                        ActiveUnfoldedPlayers.Remove(playerToRemove);
                        break;
                    }
                case Move.bet:
                    {
                        //Check preconditions
                        if (LastRaise > 0 || TotalRaise > 0)
                            throw new IOException("Can't bet if someone had bet before... use raise move");
                        if (amountToBet == CurrentPlayer.Wallet.amountOfMoney)
                            throw new ArgumentOutOfRangeException("Can't bet all of your money... use all-in move");
                        int highestAllIn = 0;
                        foreach (Player player in ActiveUnfoldedPlayers)    //find highest all-in at the table
                        {
                            if (player.Wallet.amountOfMoney > highestAllIn)
                                highestAllIn = player.Wallet.amountOfMoney;
                        }
                        if (amountToBet > highestAllIn)
                            throw new ArgumentOutOfRangeException("Bet is bigger than the highest player's all-in");

                        //Make bet
                        PlayMove(Move.call, 0);     //First call whatever you need to call

                        Pot lastPartialPot = currentPot;
                        while (lastPartialPot.PartialPot != null)
                            lastPartialPot = lastPartialPot.PartialPot;
                        lastPartialPot.Value += amountToBet;
                        lastPartialPot.AmountToClaim += amountToBet;
                        CurrentTurn.Bet(amountToBet);
                        lastRaise = amountToBet;
                        totalRaise += LastRaise;
                        LiveBets[CurrentPlayer] = TotalRaise;
                        break;
                    }
                case Move.raise:
                    {
                        //Check preconditions
                        if (LastRaise == 0)
                            throw new IOException("Can't raise if no one had bet before... use bet move");
                        if (TotalRaise + amountToBet == CurrentPlayer.Wallet.amountOfMoney + LiveBets[CurrentPlayer])
                            throw new ArgumentOutOfRangeException("Can't bet all of your money... use all-in move");
                        int highestAllIn = 0;
                        foreach (Player player in ActiveUnfoldedPlayers)    //find highest all-in at the table
                        {
                            if (player.Wallet.amountOfMoney + LiveBets[player] > highestAllIn)
                                highestAllIn = player.Wallet.amountOfMoney;
                        }
                        if (TotalRaise + amountToBet > highestAllIn)
                            throw new ArgumentOutOfRangeException("Raise is bigger than the highest player's all-in");

                        //Make raise
                        CurrentTurn.Raise(amountToBet);
                        lastRaise = amountToBet;
                        totalRaise += LastRaise;
                        LiveBets[CurrentPlayer] = LastRaise;
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
                        if (this.CurrentPlayer.Wallet.amountOfMoney <= TotalRaise)
                            PlayMove(Move.call, this.CurrentPlayer.Wallet.amountOfMoney);
                        else
                        {
                            PlayMove(Move.call, TotalRaise);
                            PlayMove(Move.raise, this.CurrentPlayer.Wallet.amountOfMoney);
                            //this.currentPot.PartialPot = new Pot(this.currentPot);
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

        private Pot FillBasePots(ref int amountToCall, ref int playerCurrentBet)
        {
            Pot partialPotIterator = CurrentPot;

            while (partialPotIterator.PartialPot != null)
            {
                if (amountToCall + playerCurrentBet < partialPotIterator.AmountToClaim)
                    throw new ArgumentException("Not enough money to fill all pots");

                if (playerCurrentBet < partialPotIterator.AmountToClaim)
                {
                    int amountToAdd = partialPotIterator.AmountToClaim - playerCurrentBet; //how much money does the player need to add in order to claim the pot
                    partialPotIterator.Value += amountToAdd;
                    LiveBets[CurrentPlayer] += amountToAdd;
                    CurrentTurn.Call(amountToAdd);
                    amountToCall -= amountToAdd;
                    playerCurrentBet = 0;
                }
                else
                {
                    playerCurrentBet -= partialPotIterator.AmountToClaim;
                }

                //Add the current player to claiming this partial pot
                if (!partialPotIterator.PlayersClaimPot.Contains(CurrentPlayer))
                    partialPotIterator.PlayersClaimPot.Add(CurrentPlayer);

                partialPotIterator = partialPotIterator.PartialPot;
            }

            return partialPotIterator;
        }
        #endregion
    }
}



/* Call move old while
   while (amountToCall + playerCurrentBet > partialPotToCheck.AmountToClaim)
                        {
                            if (partialPotToCheck.PartialPot == null)
                            {
                                throw new ArgumentException("Not enough partial pots were created! Somthing isn't right!");
                            }

                            if (playerCurrentBet < partialPotToCheck.AmountToClaim)
                            {
                                int amountToAdd = partialPotToCheck.AmountToClaim - playerCurrentBet; //how much money does the player need to add in order to claim the pot
                                partialPotToCheck.Value += amountToAdd;
                                LiveBets[CurrentPlayer] += amountToAdd;
                                CurrentTurn.Call(amountToAdd);
                                amountToCall -= amountToAdd;
                                playerCurrentBet = 0;
                            }
                            else
                            {
                                playerCurrentBet -= partialPotToCheck.AmountToClaim;
                            }

                            if (!partialPotToCheck.PlayersClaimPot.Contains(CurrentPlayer))
                                partialPotToCheck.PlayersClaimPot.Add(CurrentPlayer);

                            partialPotToCheck = partialPotToCheck.PartialPot;
                        }
*/

/* All-in move old while
        Pot tempPot = currentPot;
        int playerCurrentBet = liveBets[currentPlayer];
        while (currentPlayer.Wallet.amountOfMoney + playerCurrentBet > tempPot.AmountToClaim)
        {
            if (playerCurrentBet < tempPot.AmountToClaim)
            {
                int amountToAdd = tempPot.AmountToClaim - playerCurrentBet; //how much money does the player need to add in order to claim the pot
                tempPot.Value += amountToAdd;
                liveBets[currentPlayer] += amountToAdd;
                currentPlayer.Wallet.amountOfMoney -= amountToAdd;
                playerCurrentBet = 0;
            }
            else
            {
                playerCurrentBet -= tempPot.AmountToClaim;
            }
            
            if (!tempPot.PlayersClaimPot.Contains(currentPlayer))
                tempPot.PlayersClaimPot.Add(currentPlayer);
            
            
            if (tempPot.PartialPot == null)
            {
                tempPot.PartialPot = new Pot(tempPot);
            }
            tempPot = partialPotToCheck.PartialPot;
        }*/
