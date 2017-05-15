using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Player
    {
        #region Constants
        public enum State
        {
            ActiveUnfolded,
            ActiveFolded,
            Passive
        }

        public const int NPRIVATE_CARDS = 2;
        #endregion

        #region Properties
        public State CurrentState { get; private set; }
        public Card[] PrivateCards { get; set; }
        public string Nickname { get; set; }
        #endregion

        #region Constructors
        public Player()
        {
            PrivateCards = new Card[NPRIVATE_CARDS];
            CurrentState = State.Passive;
        }
        #endregion

    }
}
