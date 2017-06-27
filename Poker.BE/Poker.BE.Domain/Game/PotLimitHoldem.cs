
using Poker.BE.CrossUtility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public class PotLimitHoldem : GamePreferencesDecorator
    {
        //Constructors
        public PotLimitHoldem(GamePreferences preferences) : base(preferences)
        {
            Limit = 0;
        }

        //Methods
        public void ChangePotLimitValue(double potValue)
        {
            Limit = potValue;
        }
    }
}
