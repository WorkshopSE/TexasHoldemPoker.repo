using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
    [TestClass()]
    public class DeckTests
    {

        #region Fields
        private Deck deck;
        #endregion

        #region Setup
        [TestInitialize]
        public void Before()
        {
            deck = new Deck();
        }

        [TestCleanup]
        public void After()
        {
            deck = null;
        }
        #endregion

        #region Tests
        [TestMethod()]
        public void DeckTest()
        {
            //Arrange
            var expected = new Card[] {
                // Clubs
                new Card(Card.Suit.Clubs, Card.Value.Ace),
                new Card(Card.Suit.Clubs, Card.Value.Two),
                new Card(Card.Suit.Clubs, Card.Value.Three),
                new Card(Card.Suit.Clubs, Card.Value.Four),
                new Card(Card.Suit.Clubs, Card.Value.Five),
                new Card(Card.Suit.Clubs, Card.Value.Six),
                new Card(Card.Suit.Clubs, Card.Value.Seven),
                new Card(Card.Suit.Clubs, Card.Value.Eight),
                new Card(Card.Suit.Clubs, Card.Value.Nine),
                new Card(Card.Suit.Clubs, Card.Value.Ten),
                new Card(Card.Suit.Clubs, Card.Value.Jack),
                new Card(Card.Suit.Clubs, Card.Value.Queen),
                new Card(Card.Suit.Clubs, Card.Value.King),

                // Diamonds
                new Card(Card.Suit.Diamonds, Card.Value.Ace),
                new Card(Card.Suit.Diamonds, Card.Value.Two),
                new Card(Card.Suit.Diamonds, Card.Value.Three),
                new Card(Card.Suit.Diamonds, Card.Value.Four),
                new Card(Card.Suit.Diamonds, Card.Value.Five),
                new Card(Card.Suit.Diamonds, Card.Value.Six),
                new Card(Card.Suit.Diamonds, Card.Value.Seven),
                new Card(Card.Suit.Diamonds, Card.Value.Eight),
                new Card(Card.Suit.Diamonds, Card.Value.Nine),
                new Card(Card.Suit.Diamonds, Card.Value.Ten),
                new Card(Card.Suit.Diamonds, Card.Value.Jack),
                new Card(Card.Suit.Diamonds, Card.Value.Queen),
                new Card(Card.Suit.Diamonds, Card.Value.King),

                // Hearts
                new Card(Card.Suit.Hearts, Card.Value.Ace),
                new Card(Card.Suit.Hearts, Card.Value.Two),
                new Card(Card.Suit.Hearts, Card.Value.Three),
                new Card(Card.Suit.Hearts, Card.Value.Four),
                new Card(Card.Suit.Hearts, Card.Value.Five),
                new Card(Card.Suit.Hearts, Card.Value.Six),
                new Card(Card.Suit.Hearts, Card.Value.Seven),
                new Card(Card.Suit.Hearts, Card.Value.Eight),
                new Card(Card.Suit.Hearts, Card.Value.Nine),
                new Card(Card.Suit.Hearts, Card.Value.Ten),
                new Card(Card.Suit.Hearts, Card.Value.Jack),
                new Card(Card.Suit.Hearts, Card.Value.Queen),
                new Card(Card.Suit.Hearts, Card.Value.King),

                // Spades
                new Card(Card.Suit.Spades, Card.Value.Ace),
                new Card(Card.Suit.Spades, Card.Value.Two),
                new Card(Card.Suit.Spades, Card.Value.Three),
                new Card(Card.Suit.Spades, Card.Value.Four),
                new Card(Card.Suit.Spades, Card.Value.Five),
                new Card(Card.Suit.Spades, Card.Value.Six),
                new Card(Card.Suit.Spades, Card.Value.Seven),
                new Card(Card.Suit.Spades, Card.Value.Eight),
                new Card(Card.Suit.Spades, Card.Value.Nine),
                new Card(Card.Suit.Spades, Card.Value.Ten),
                new Card(Card.Suit.Spades, Card.Value.Jack),
                new Card(Card.Suit.Spades, Card.Value.Queen),
                new Card(Card.Suit.Spades, Card.Value.King),
            };

            //Act
            var actual = new Deck().Cards.ToArray();


            //Assert
            for (int i = 0; i < Deck.NCARDS; i++)
            {
                Assert.AreEqual<Card>(expected[i], actual[i]);
            }
        }

        [TestMethod()]
        public void ShuffleCardsTest()
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

    }
}