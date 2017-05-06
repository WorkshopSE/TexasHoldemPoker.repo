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
            Ace,
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
        }

        #endregion

        #region Constants
        public const int NVALUE = 13;
        public const int NSUIT = 4;
        #endregion

        #region Fields
        private int number;
        #endregion

        #region Properties
        protected Suit CardSuit { get; }
        protected Value CardValue { get; }

        // TODO: ?
        protected int ShuffledIndex { get; private set; }
        #endregion

        #region Constructors
        public Card(Suit suit, Value num)
        {
            CardSuit = suit;
            CardValue = num;
        }
        #endregion

        #region Private Functions
        private int ValueToNumber(Value val)
        {
            switch (val)
            {
                case Value.Ace: return 1;
                case Value.Two: return 2;
                case Value.Three: return 3;
                case Value.Four: return 4;
                case Value.Five: return 5;
                case Value.Six: return 6;
                case Value.Seven: return 7;
                case Value.Eight: return 8;
                case Value.Nine: return 9;
                case Value.Ten: return 10;
                case Value.Jack: return 11;
                case Value.Queen: return 12;
                case Value.King: return 13;
                default: return -1;
            }
        }
        private Value NumberToValue(int num)
        {
            switch (num)
            {
                case 1: return Value.Ace;
                case 2: return Value.Two;
                case 3: return Value.Three;
                case 4: return Value.Four;
                case 5: return Value.Five;
                default:
                    break;
            }
            return default(Value);
        }
        #endregion

        #region Methods
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
