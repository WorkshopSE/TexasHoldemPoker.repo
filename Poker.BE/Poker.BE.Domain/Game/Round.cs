using Poker.BE.Domain.Utility.Exceptions;
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

        #region Fields
        private ICollection<Player> activeUnfoldedPlayers;
        private Turn currentTurn;
        private Player dealer;
        private Player currentPlayer;
        private Pot currentPot;
        private Dictionary<Player, double> liveBets;
        private double totalRaise;
        private double lastRaise;
        private Player lastPlayerToRaise;
        private bool isPreflop;
        private GameConfig config;
        #endregion

        #region Properties
        public ICollection<Player> ActiveUnfoldedPlayers { get { return activeUnfoldedPlayers; } }
        public Turn CurrentTurn { get { return currentTurn; } }
        public Player CurrentPlayer { get { return currentPlayer; } }
        public Pot CurrentPot { get { return currentPot; } }
        public Dictionary<Player, double> LiveBets { get { return liveBets; } }
        public double TotalRaise { get { return totalRaise; } }
        public double LastRaise { get { return lastRaise; } }
        public Player LastPlayerToRaise { get { return lastPlayerToRaise; } }

        #endregion

        #region Constructors
        public Round(Player dealer, ICollection<Player> activeUnfoldedPlayers, Pot currentPot, bool isPreflop, GameConfig config)
        {
            this.isPreflop = isPreflop;

            //Set up players info
            this.dealer = dealer;
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.liveBets = new Dictionary<Player, double>();
            foreach (Player player in activeUnfoldedPlayers)
            {
                LiveBets.Add(player, 0);
            }
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((this.activeUnfoldedPlayers.ToList().IndexOf(dealer) + 1) % this.activeUnfoldedPlayers.Count);
            this.lastPlayerToRaise = dealer;

            //Set up raise info
            this.totalRaise = 0;
            this.lastRaise = 0;
            

            //Others
            this.currentPot = currentPot;
            this.currentTurn = new Turn(currentPlayer, currentPot);
            this.config = config;
        }
        #endregion

        #region Methods
        public void startRound(bool isPreFlop)
        {
            lastPlayerToRaise = CurrentPlayer;
            //TODO
        }

        public double PlayMove(Move playMove, double amountToBetOrCall)
        {
            switch (playMove)
            {
                case Move.check:
                    {
                        //if no one had raised
                        if (TotalRaise == config.AntesValue)
                        {
                            CurrentTurn.Check();  // Do nothing??
                        }
                        else if ((isPreflop &&
                                currentPlayer == this.activeUnfoldedPlayers.ElementAt((this.activeUnfoldedPlayers.ToList().IndexOf(dealer) + 2) % this.activeUnfoldedPlayers.Count)) &&
                                liveBets[CurrentPlayer] == config.MinimumBet + config.AntesValue)
                        {
                            CurrentTurn.Check();    // Do nothing??
                        }
                        else
                            throw new GameRulesException("Can't check if someone had raised before");
                        
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
                        return TotalRaise;
                        //break;
                    }
                case Move.bet:
                    {
                        //Check preconditions
                        if (LastRaise > 0 || TotalRaise > 0)
                            throw new GameRulesException("Can't bet if someone had bet before... use raise move");
                        if (TotalRaise + amountToBetOrCall == CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer])
                            throw new WrongIOException("Can't use bet move for all of your money... use all-in move");
                        RaisePreconditions(amountToBetOrCall);

                        //Make bet
                        Raise(amountToBetOrCall);

                        break;
                    }
                case Move.raise:
                    {
                        //Check preconditions
                        if (LastRaise == 0)
                            throw new GameRulesException("Can't raise if no one had bet before... use bet move");
                        if (TotalRaise + amountToBetOrCall == CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer])
                            throw new WrongIOException("Can't use raise move for all of your money... use all-in move");
                        RaisePreconditions(amountToBetOrCall);

                        //Make Raise
                        Call(0);                  //First call whatever you need to call
                        Raise(amountToBetOrCall); //Than raise
                        break;
                    }
                case Move.allin:
                    {
                        //Check preconditions
                        double highestOtherAllIn = 0;
                        foreach (Player player in ActiveUnfoldedPlayers)    //find highest all-in of the other players at the table
                        {
                            if (player != this.CurrentPlayer && player.Wallet.AmountOfMoney + LiveBets[player] > highestOtherAllIn)
                                highestOtherAllIn = player.Wallet.AmountOfMoney;
                        }
                        if (this.CurrentPlayer.Wallet.AmountOfMoney > highestOtherAllIn)
                            throw new GameRulesException("all-in is bigger than the highest other player's all-in... use bet\raise move");
                        if (CurrentPlayer.Wallet.AmountOfMoney == 0)
                            throw new WrongIOException("You're already all-in!!");

                        //Make all-in
                        if (this.CurrentPlayer.Wallet.AmountOfMoney + LiveBets[currentPlayer] <= TotalRaise)
                            Call(this.CurrentPlayer.Wallet.AmountOfMoney);
                        else
                        {
                            Call(0);
                            if (LiveBets[currentPlayer] != totalRaise)
                                throw new WrongIOException("Didn't call right!");
                            RaisePreconditions(this.CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer] - TotalRaise);
                            Raise(this.CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer] - TotalRaise);

                        }
                        CurrentTurn.AllIn();



                        break;
                    }
                default:
                    {
                        //TODO: print invalid move exception
                        throw new GameRulesException("Invalid Move");
                    }
            }
            //Change Player
            CalculateNextPlayer();
            CurrentTurn.CurrentPlayer = this.CurrentPlayer;

            return TotalRaise;
        }
        #endregion

        #region Private Functions
        private void CalculateNextPlayer()
        {
            do
                this.currentPlayer = this.ActiveUnfoldedPlayers.ElementAt((ActiveUnfoldedPlayers.ToList().IndexOf(this.CurrentPlayer) + 1) % ActiveUnfoldedPlayers.Count);
            while (this.CurrentPlayer.CurrentState == Player.State.ActiveAllIn);

        }

        private void Call(double amountToBetOrCall)
        {
            if (amountToBetOrCall == 0)
                amountToBetOrCall = TotalRaise - LiveBets[CurrentPlayer];
            double playerCurrentBet = LiveBets[CurrentPlayer];

            Pot partialPotIterator = CurrentPot;
            Pot lastPartialPot = partialPotIterator;
            double amountToAdd = amountToBetOrCall;    //how much money does the player need to add in order to claim the pot
            double lastPlayerCurrentBet = 0;

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
                throw new WrongIOException("Not enough partial pots were created! Something isn't right!");

            if (CurrentPlayer.Wallet.AmountOfMoney == 0) //if call all-in move
            {
                //Add new partial pot in the middle
                Pot newPartialPot = new Pot(lastPartialPot.BasePot)
                {
                    PartialPot = lastPartialPot
                };
                lastPartialPot.BasePot = newPartialPot;

                if (newPartialPot.BasePot != null)
                {
                    newPartialPot.BasePot.PartialPot = newPartialPot;
                }
                else
                {
                    currentPot = newPartialPot;
                }

                //Update partial pot's fields
                foreach (Player player in lastPartialPot.PlayersClaimPot)
                {
                    newPartialPot.PlayersClaimPot.Add(player);
                }

                lastPartialPot.PlayersClaimPot.Remove(CurrentPlayer);

                newPartialPot.AmountToClaim = amountToAdd + lastPlayerCurrentBet;
                lastPartialPot.AmountToClaim -= newPartialPot.AmountToClaim;
                newPartialPot.Value = lastPartialPot.Value - (lastPartialPot.AmountToClaim * lastPartialPot.PlayersClaimPot.Count);
                lastPartialPot.Value = lastPartialPot.AmountToClaim * lastPartialPot.PlayersClaimPot.Count;
            }
        }

        private void RaisePreconditions(double amountToRaise)
        {
            if (LastRaise > amountToRaise)
            {
                throw new GameRulesException("Can't raise less than last raise");
            }

            double highestAllIn = 0;

            //Note: find highest all-in at the table
            foreach (Player player in ActiveUnfoldedPlayers)
            {
                if (player.Wallet.AmountOfMoney + LiveBets[player] > highestAllIn)
                {
                    highestAllIn = player.Wallet.AmountOfMoney + LiveBets[player];
                }
            }

            if (TotalRaise + amountToRaise > highestAllIn)
            {
                throw new GameRulesException("Raise is bigger than the highest player's all-in");
            }
        }

        private void Raise(double amountToRaise)
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

            if (currentPlayer.Wallet.AmountOfMoney == 0)
                lastPartialPot.PartialPot = new Pot(lastPartialPot);

            lastPlayerToRaise = CurrentPlayer;
        }
        #endregion
    }
}