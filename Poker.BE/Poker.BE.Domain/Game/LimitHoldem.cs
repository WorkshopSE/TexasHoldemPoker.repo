using Poker.BE.CrossUtility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public class LimitHoldem : GamePreferencesDecorator
    {

        public LimitHoldem(GamePreferences preferences, double limitAmount) : base(preferences)
        {
            Limit = limitAmount;
        }
    }
}
