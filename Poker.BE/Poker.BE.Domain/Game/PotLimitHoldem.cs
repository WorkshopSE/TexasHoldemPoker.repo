using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
