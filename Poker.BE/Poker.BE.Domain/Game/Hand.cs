using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Hand
    {

        #region Constants
        public static readonly int MINIMAL_NUMBER_OF_ACTIVE_PLAYERS;
        #endregion
        private Deck deck;

        public Hand(Deck deck)
        {
            this.deck = deck;
        }
    }
}
