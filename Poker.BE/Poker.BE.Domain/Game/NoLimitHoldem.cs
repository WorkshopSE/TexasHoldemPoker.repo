using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Exceptions;
using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class NoLimitHoldem : GamePreferences
    {
        #region Constructors
        /// <summary>
        /// default configuration
        /// </summary>
        public NoLimitHoldem()
        {
            Name = "Unknown Room";
            BuyInCost = 100.0;
            MinimumBet = 10.0;
            AntesValue = 0;
            MaxNumberOfPlayers = 10;
            MinNumberOfPlayers = 2;
            IsSpactatorsAllowed = true;
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