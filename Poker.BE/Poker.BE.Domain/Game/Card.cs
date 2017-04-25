using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    
    public class Card
    {
        #region Enums
        public enum Suit
        {
            Hearts,
            Clubs,
            Spades,
            Diamonds
        }
        public enum Value
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace,
        }

        #endregion

        #region Properties
        protected Suit CardSuit { get; }
        protected Value CardNumber { get; }
        protected int ShuffledIndex { get; }//used for shuffling cards
        #endregion

        #region Methods
        public Card(Suit suit, Value num)
        {
            CardSuit = suit;
            CardNumber = num;
        }
        public void EnumerateCard()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            ShuffledIndex = rnd.Next();
        }
        public int CompareTo(Card CardToCompare)
        {
            if (ShuffledIndex > CardToCompare.ShuffledIndex)
                return 1;
            else if (ShuffledIndex < CardToCompare.ShuffledIndex)
                return -1;
            return 0;
        }
        #endregion
    }
}
