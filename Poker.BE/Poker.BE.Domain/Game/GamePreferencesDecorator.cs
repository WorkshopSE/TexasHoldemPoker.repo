
using Poker.BE.CrossUtility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public abstract class GamePreferencesDecorator : GamePreferences
    {
        //Fields
        protected GamePreferences preferences;

        //Properties
        public double Limit { get; protected set; }

        #region Constructors
        public GamePreferencesDecorator(GamePreferences preferences)
        {
            this.preferences = preferences;
        }
        #endregion

        #region Methods
        public override void CheckBuyIn(double amountOfMoney)
        {
            preferences.CheckBuyIn(amountOfMoney);
        }

        public override void CheckPlayers(int numOfPlayers)
        {
            preferences.CheckPlayers(numOfPlayers);
        }

        public override void CheckRaise(double raiseAmount)
        {
            preferences.CheckRaise(raiseAmount);

            if (raiseAmount > Limit)
            {
                throw new WrongIOException("Can't raise more than room's raise limit");
            }
        }
        #endregion
    }
}
