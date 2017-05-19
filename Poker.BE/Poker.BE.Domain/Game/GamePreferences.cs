using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class GamePreferences
    {
        #region Properties
        public int SmallBlind { get; set; }
        public int BigBlind { get { return SmallBlind * 2; } }
		
        #endregion

	}
}
