﻿using System;

namespace Poker.BE.Domain.Game
{

    public class Card : IComparable
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
        public enum State
        {
            FaceDown,
            FaceUp,
        }

        #endregion

        #region Constants
        public const int NVALUE = 13;
        public const int NSUIT = 4;
        #endregion

        #region Properties
        public int Index { get; }
        public Suit CardSuit { get; }
        public Value CardValue { get; }
        public int Number { get; private set; }
        public State CardState { get; set; }

        protected int ShuffledIndex { get; private set; }
        #endregion

        #region Constructors
        public Card(int index, Suit suit, Value val)
        {
            Index = index;
            CardSuit = suit;
            CardValue = val;
            Number = ValueToNumber(val);
            CardState = State.FaceUp;
        }

        public Card(int index, Suit suit, int num)
        {
            Index = index;
            CardSuit = suit;
            Number = num;
            CardValue = NumberToValue(Number);
            CardState = State.FaceUp;
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
                case 6: return Value.Six;
                case 7: return Value.Seven;
                case 8: return Value.Eight;
                case 9: return Value.Nine;
                case 10: return Value.Ten;
                case 11: return Value.Jack;
                case 12: return Value.Queen;
                case 13: return Value.King;
                default:
                    return default(Value);
            }
        }

        private int SuitToNumber(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts: return 0;
                case Suit.Clubs: return 1;
                case Suit.Spades: return 2;
                case Suit.Diamonds: return 3;
                default: return -1;
            }
        }
        #endregion

        #region Methods
        public int CardValueStrength()
        {
            if (Number == 1) return 14; // in case of an Ace
            else return this.Number;
        }

        public int GetSuitNumber()
        {
            return SuitToNumber(this.CardSuit);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Card;
            var result = 0;
            if (other != null)
            {
                result = (Number > other.Number) ? 1 : (Number < other.Number) ? -1 : 0;
            }
            return result;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            var other = obj as Card;

            if (other == null || obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            if(other.Number == Number & other.CardSuit == CardSuit)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} :{2}", CardSuit, CardValue, Number);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
        #endregion
    }
}
