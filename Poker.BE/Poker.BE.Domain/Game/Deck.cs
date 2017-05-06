﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    /// <summary> Defined the hand that the active players are playing poker at the room </summary>
    /// 
    /// <remarks>
    /// <author>Gal Wainer</author>
    /// <lastModified>2017-04-25</lastModified>
    /// 
    /// comments by:
    /// <author>Idan Izicovich</author>
    /// 
    /// </remarks>
    public class Deck
    {
        #region Constants
        public const int NCARDS = checked(Card.NSUIT * Card.NVALUE);
        #endregion

        #region Fields
        private Card[] cards;
        private int shuffleTimes;
        private Random random;
        #endregion

        #region Constructors
        public Deck()
        {
            cards = GetFullDeck();
            shuffleTimes = 2;
            random = new Random();
        }

        public Deck(int shuffleTimes) : this()
        {
            this.shuffleTimes = shuffleTimes;
        }
        #endregion

        #region Private Functions
        private Card[] GetFullDeck()
        {
            Card[] result = new Card[NCARDS];
            int index = 0;

            // inner function (delegate) for code reuse of this loop:
            Func<Card.Suit, int, int> loop = (currSuit, currIndex) =>
             {
                 for (int currNumber = 1; currNumber <= Card.NVALUE; currNumber++)
                 {
                     result[index] = new Card(currSuit, currNumber);
                     index++;
                 }

                 return index;
             };

            // Clubs
            loop(Card.Suit.Clubs, index);

            // Diamonds
            loop(Card.Suit.Diamonds, index);

            // Hearts
            loop(Card.Suit.Hearts, index);

            // Spades
            loop(Card.Suit.Spades, index);

            return result;
        }
        #endregion

        #region Methods
        public void ShuffleCards()
        {
            for (int i = 0; i < shuffleTimes; i++)
            {
                // splitting the deck to 2 parts
                var split1 = cards.ToList();
                split1.RemoveRange(cards.Length / 2, cards.Length / 2);

                var split2 = cards.ToList();
                split2.RemoveRange(0, cards.Length / 2);

                // merging the parts randomly, at the same time (parallel)
                var merged = new List<Card>();
                while (split1.Count > 0 | split2.Count > 0)
                {
                    int index;

                    Parallel.Invoke(
                        () =>
                        {
                            index = random.Next(split1.Count);
                            merged.Add(split1.ElementAt(index));
                            split1.RemoveAt(index);
                        },
                        () =>
                        {
                            index = random.Next(split2.Count);
                            merged.Add(split2.ElementAt(index));
                            split2.RemoveAt(index);
                        });
                }

                // swapping randomly

            }
        }

        /// <summary>
        /// pulling one card from the deck
        /// </summary>
        /// <returns>a card</returns>
        public Card PullCard()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// pulling v cards from the deck
        /// </summary>
        /// <param name="v">number of cards to pull</param>
        /// <returns>collection of cards</returns>
        public ICollection<Card> PullCards(int v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
