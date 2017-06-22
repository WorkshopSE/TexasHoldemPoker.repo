using Poker.BE.CrossUtility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public class LimitHoldem : GamePreferencesDecorator
    {

        public LimitHoldem(GamePreferences preferences, double limitAmount) : base(preferences)
        {
            Limit = limitAmount;
        }

        public override void CheckRaise(double raiseAmount)
        {
            base.CheckRaise(raiseAmount);

            if (raiseAmount > Limit)
            {
                throw new WrongIOException("Can't raise more than room's raise limit");
            }
        }
    }
}
