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
            Null,
            Check,
            Call,
            Bet,
            Fold,
            Raise,
            Allin
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
            liveBets = new Dictionary<Player, double>();
            foreach (Player player in activeUnfoldedPlayers)
            {
                LiveBets.Add(player, 0);
            }
            currentPlayer = this.activeUnfoldedPlayers.ElementAt((this.activeUnfoldedPlayers.ToList().IndexOf(dealer) + 1) % this.activeUnfoldedPlayers.Count);
            lastPlayerToRaise = dealer;

            //Set up raise info
            totalRaise = 0;
            lastRaise = 0;


            //Others
            this.currentPot = currentPot;
            currentTurn = new Turn(currentPlayer);
            this.config = config;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A standart betting round - Starts from the player sitting at the small blind.
        /// Each player decides his play move(call, fold, raise, etc) in his turn.
        /// </summary>
        /// <remarks>UC024: Play a Betting Round</remarks>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.kdfvqg7xrjgw"/>
        /// <returns>The remaining active unfolded players at the end of this round</returns>
        public ICollection<Player> PlayBettingRound()
        {
            //Check Preconditions
            if (activeUnfoldedPlayers.Count < Hand.MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new NotEnoughPlayersException("Not enough players to start round!");
            }

            //Play round until everyone is called or folded
            while (LastPlayerToRaise != CurrentPlayer)
            {
                //Waiting for the player to choose play move
                while (CurrentPlayer.PlayMove == default(Move)) ;
                PlayMove(CurrentPlayer.PlayMove, CurrentPlayer.AmountToBetOrCall);
            }

            return activeUnfoldedPlayers;
        }

        /// <summary>
        /// Make the move the current player decided to do.
        /// </summary>
        public void PlayMove(Move playMove, double amountToBetOrCall)
        {
            switch (playMove)
            {
                case Move.Check:
                    {
                        Check();
                        break;
                    }
                case Move.Call:
                    {
                        Call(amountToBetOrCall);
                        break;
                    }
                case Move.Fold:
                    {
                        Fold();
                        break;
                    }
                case Move.Bet:
                    {
                        Bet(amountToBetOrCall);
                        break;
                    }
                case Move.Raise:
                    {
                        //Check preconditions
                        if (LastRaise == 0)
                            throw new GameRulesException("Can't raise if no one had bet before... use bet move");
                        if (TotalRaise + amountToBetOrCall == CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer])
                            throw new WrongIOException("Can't use raise move for all of your money... use all-in move");
                        RaisePreconditions(amountToBetOrCall);

                        //Make Raise
                        Call(0);                  //First call whatever you need to call
                        Raise(amountToBetOrCall); //Then raise
                        break;
                    }
                case Move.Allin:
                    {
                        Allin();
                        break;
                    }
                default:
                    {
                        //TODO: print invalid move exception
                        throw new GameRulesException("Invalid Move");
                    }
            }

            //Change to next player
            CalculateNextPlayer();
        }
        #endregion

        #region Private Functions
        private void CalculateNextPlayer()
        {
            do
            {
                currentPlayer = ActiveUnfoldedPlayers.ElementAt((ActiveUnfoldedPlayers.ToList().IndexOf(CurrentPlayer) + 1) % ActiveUnfoldedPlayers.Count);
            }
            while (CurrentPlayer.CurrentState == Player.State.ActiveAllIn && LastPlayerToRaise != CurrentPlayer);

            CurrentTurn.CurrentPlayer = this.CurrentPlayer;
        }

        private void Check()
        {
            //if no one had raised
            if (!(isPreflop && TotalRaise == config.AntesValue) && !(!isPreflop && TotalRaise == 0) &&
                !(isPreflop &&
                    currentPlayer == activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(dealer) + 2) % activeUnfoldedPlayers.Count) &&
                    liveBets[CurrentPlayer] == config.MinimumBet + config.AntesValue))
            {
                throw new GameRulesException("Can't check if someone had raised before");
            }
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
                    CurrentPlayer.SubstractMoney(amountToAdd);
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
            //CurrentTurn.Call(amountToBetOrCall);

            if (amountToBetOrCall + playerCurrentBet > lastPartialPot.AmountToClaim)
                throw new WrongIOException("Not enough partial pots were created! Something isn't right!");

            //if this is a Call All-In move then make new partial pot
            if (CurrentPlayer.Wallet.AmountOfMoney == 0) 
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

        private void Fold()
        {
            CurrentPlayer.Fold();
            Player playerToRemove = CurrentPlayer;

            //Remover player from all pots
            Pot partialPotIterator = CurrentPot;
            while (partialPotIterator != null)
            {
                partialPotIterator.PlayersClaimPot.Remove(playerToRemove);
                partialPotIterator = partialPotIterator.PartialPot;
            }

            //Remove player from round
            currentPlayer = ActiveUnfoldedPlayers.ElementAt((ActiveUnfoldedPlayers.ToList().IndexOf(CurrentPlayer) - 1) % ActiveUnfoldedPlayers.Count);
            ActiveUnfoldedPlayers.Remove(playerToRemove);
        }

        private void Bet(double amountToBetOrCall)
        {
            //Check preconditions
            if (LastRaise > 0 || TotalRaise > 0)
                throw new GameRulesException("Can't bet if someone had bet before... use raise move");
            if (TotalRaise + amountToBetOrCall == CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer])
                throw new WrongIOException("Can't use bet move for all of your money... use all-in move");
            RaisePreconditions(amountToBetOrCall);

            //Make bet
            Raise(amountToBetOrCall);
        }

        private void Raise(double amountToRaise)
        {
            Pot lastPartialPot = currentPot;
            while (lastPartialPot.PartialPot != null)
                lastPartialPot = lastPartialPot.PartialPot;

            lastPartialPot.Value += amountToRaise;
            lastPartialPot.AmountToClaim += amountToRaise;
            CurrentPlayer.SubstractMoney(amountToRaise);
            LiveBets[CurrentPlayer] += amountToRaise;
            lastRaise = amountToRaise;
            totalRaise += LastRaise;

            //reset the players claiming this pot
            lastPartialPot.PlayersClaimPot = new List<Player>
            {
                CurrentPlayer
            };
            if (currentPlayer.Wallet.AmountOfMoney == 0)
                lastPartialPot.PartialPot = new Pot(lastPartialPot);

            lastPlayerToRaise = CurrentPlayer;
        }

        private void Allin()
        {
            //Check preconditions
            double highestOtherAllIn = 0;
            foreach (Player player in ActiveUnfoldedPlayers)    //find highest all-in of the other players at the table
            {
                if (player != CurrentPlayer && player.Wallet.AmountOfMoney + LiveBets[player] > highestOtherAllIn)
                    highestOtherAllIn = player.Wallet.AmountOfMoney;
            }
            if (CurrentPlayer.Wallet.AmountOfMoney > highestOtherAllIn)
                throw new GameRulesException("all-in is bigger than the highest other player's all-in... use bet\raise move");
            if (CurrentPlayer.Wallet.AmountOfMoney == 0)
                throw new WrongIOException("You're already all-in!!");

            //Make all-in
            if (CurrentPlayer.Wallet.AmountOfMoney + LiveBets[currentPlayer] <= TotalRaise)
                Call(CurrentPlayer.Wallet.AmountOfMoney);
            else
            {
                Call(0);
                if (LiveBets[currentPlayer] != totalRaise)
                    throw new WrongIOException("Didn't call right!");
                RaisePreconditions(CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer] - TotalRaise);
                Raise(CurrentPlayer.Wallet.AmountOfMoney + LiveBets[CurrentPlayer] - TotalRaise);
            }
            CurrentPlayer.AllIn();
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
        #endregion
    }
}