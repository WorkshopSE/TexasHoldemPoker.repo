﻿using Poker.BE.CrossUtility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public class NoLimitHoldem : GamePreferences
    {
        #region Constructors
        /// <summary>
        /// default configuration
        /// </summary>
        public NoLimitHoldem(string name = "Unknown Room", double buyInCost = 100.0, double minimumBet = 10.0, double antes = 0,
                                int minNumOfPlayers = 2, int maxNumOfPlayers = 10, bool isSpectatorsAllowed = true)
        {
            Name = name;
            BuyInCost = buyInCost;
            MinimumBet = minimumBet;
            AntesValue = antes;
            MinNumberOfPlayers = minNumOfPlayers;
            MaxNumberOfPlayers = maxNumOfPlayers;
            IsSpactatorsAllowed = isSpectatorsAllowed;
        }
        #endregion

        #region Methods
        public override void CheckBuyIn(double amountOfMoney)
        {
            if (amountOfMoney < BuyInCost)
            {
                throw new NotEnoughMoneyException("Not enough money to buy in!");
            }
        }

        public override void CheckPlayers(int numOfPlayers)
        {
            if (numOfPlayers < MIN_NUMBER_OF_PLAYERS && numOfPlayers > MAX_NUMBER_OF_PLAYERS)
            {
                throw new NotEnoughPlayersException("Number of players is too low/high");
            }
        }

        public override void CheckRaise(double raiseAmount)
        {
            if (raiseAmount < MinimumBet)
            {
                throw new WrongIOException("Raise amount is less than minimum bet");
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            var other = obj as NoLimitHoldem;
            if (other == null) return false;

            return
                //this.GamePreferences.Equals(other.GamePreferences) && // undone: override GamePreferences.Equals(:object)
                this.BuyInCost.Equals(other.BuyInCost) &&
                this.IsSpactatorsAllowed == other.IsSpactatorsAllowed &&
                this.MaxNumberOfPlayers == other.MaxNumberOfPlayers &&
                this.MinimumBet == other.MinimumBet &&
                this.AntesValue == other.AntesValue &&
                this.Name.Equals(other.Name) &&
                this.MinNumberOfPlayers == other.MinNumberOfPlayers;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}