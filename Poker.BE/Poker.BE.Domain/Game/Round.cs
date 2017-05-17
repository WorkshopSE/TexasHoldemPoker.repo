using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Round
    {
		#region Fields
		private int roundNumber; 
        // 1 - New Round, 2- Flop, 3-River, 4-Turn
        private Deck deck;
		#endregion

		#region Constructors
        public Round()
		{
            roundNumber = 1;
            PlayPreFlop();
        }

		#endregion

		#region Methods


		#endregion

	}

}
