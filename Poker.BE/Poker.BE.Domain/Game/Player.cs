using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Player
    {
        #region Enums
        public enum State
        {
            ActiveUnfolded,
            ActiveFolded,
            Passive
        }
        #endregion
        #region Properties
        public State CurrentState { get; private set; }
        #endregion

    }
}
