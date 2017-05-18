using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class NoLimitHoldem : GamePreferences
    {
		#region Fields
		#endregion


		#region Properties
		public int MinimumBlind { get; set; }
		#endregion

		#region Constructors
        public NoLimitHoldem()
		{
            MinimumBlind = 2;
		}
		#endregion

		#region Methods


		#endregion

	}
}
