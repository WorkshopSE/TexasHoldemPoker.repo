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

        #region Properties
        public TestContext TestContext { get; set; }
        #endregion

        #region Setup

        //public DeckTests()
        //{
        //    TestContext = new TestContext();
        //}

        [TestInitialize]
        public void Before()
        {
            deck = new Deck();
        }


        [TestCleanup]
        public void After()
        {
            //deck = null;
        }
        #endregion

        #region Tests
        [TestMethod()]
        public void DeckTest()
        {
            //Arrange
            var expected = new Card[] {
                // Clubs
                new Card(0, Card.Suit.Clubs, Card.Value.Ace),
                new Card(1, Card.Suit.Clubs, Card.Value.Two),
                new Card(2, Card.Suit.Clubs, Card.Value.Three),
                new Card(3, Card.Suit.Clubs, Card.Value.Four),
                new Card(4, Card.Suit.Clubs, Card.Value.Five),
                new Card(5, Card.Suit.Clubs, Card.Value.Six),
                new Card(6, Card.Suit.Clubs, Card.Value.Seven),
                new Card(7, Card.Suit.Clubs, Card.Value.Eight),
                new Card(8, Card.Suit.Clubs, Card.Value.Nine),
                new Card(9, Card.Suit.Clubs, Card.Value.Ten),
                new Card(10, Card.Suit.Clubs, Card.Value.Jack),
                new Card(11, Card.Suit.Clubs, Card.Value.Queen),
                new Card(12, Card.Suit.Clubs, Card.Value.King),

                // Diamonds
                new Card(13, Card.Suit.Diamonds, Card.Value.Ace),
                new Card(14, Card.Suit.Diamonds, Card.Value.Two),
                new Card(15, Card.Suit.Diamonds, Card.Value.Three),
                new Card(16, Card.Suit.Diamonds, Card.Value.Four),
                new Card(17, Card.Suit.Diamonds, Card.Value.Five),
                new Card(18, Card.Suit.Diamonds, Card.Value.Six),
                new Card(19, Card.Suit.Diamonds, Card.Value.Seven),
                new Card(20, Card.Suit.Diamonds, Card.Value.Eight),
                new Card(21, Card.Suit.Diamonds, Card.Value.Nine),
                new Card(22, Card.Suit.Diamonds, Card.Value.Ten),
                new Card(23, Card.Suit.Diamonds, Card.Value.Jack),
                new Card(24, Card.Suit.Diamonds, Card.Value.Queen),
                new Card(25, Card.Suit.Diamonds, Card.Value.King),

                // Hearts
                new Card(26, Card.Suit.Hearts, Card.Value.Ace),
                new Card(27, Card.Suit.Hearts, Card.Value.Two),
                new Card(28, Card.Suit.Hearts, Card.Value.Three),
                new Card(29, Card.Suit.Hearts, Card.Value.Four),
                new Card(30, Card.Suit.Hearts, Card.Value.Five),
                new Card(31, Card.Suit.Hearts, Card.Value.Six),
                new Card(32, Card.Suit.Hearts, Card.Value.Seven),
                new Card(33, Card.Suit.Hearts, Card.Value.Eight),
                new Card(34, Card.Suit.Hearts, Card.Value.Nine),
                new Card(35, Card.Suit.Hearts, Card.Value.Ten),
                new Card(36, Card.Suit.Hearts, Card.Value.Jack),
                new Card(37, Card.Suit.Hearts, Card.Value.Queen),
                new Card(38, Card.Suit.Hearts, Card.Value.King),

                // Spades
                new Card(39, Card.Suit.Spades, Card.Value.Ace),
                new Card(40, Card.Suit.Spades, Card.Value.Two),
                new Card(41, Card.Suit.Spades, Card.Value.Three),
                new Card(42, Card.Suit.Spades, Card.Value.Four),
                new Card(43, Card.Suit.Spades, Card.Value.Five),
                new Card(44, Card.Suit.Spades, Card.Value.Six),
                new Card(45, Card.Suit.Spades, Card.Value.Seven),
                new Card(46, Card.Suit.Spades, Card.Value.Eight),
                new Card(47, Card.Suit.Spades, Card.Value.Nine),
                new Card(48, Card.Suit.Spades, Card.Value.Ten),
                new Card(49, Card.Suit.Spades, Card.Value.Jack),
                new Card(50, Card.Suit.Spades, Card.Value.Queen),
                new Card(51, Card.Suit.Spades, Card.Value.King),
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
            //TODO: Test that Depends on Random - save the CEID of the random number.

            //Arrange
            var expected = new Deck().Cards.ToArray();

            //Act
            deck.ShuffleCards();
            var actual = deck.Cards.ToArray();

            //Assert
            int equalCount = 0;
            int shapeCount = 0;
            for (int j = 0; j < Deck.NCARDS; j++)
            {

                TestContext.WriteLine("ex{0}={1}; ac{0}={2}", j, expected[j], actual[j]);

                if (expected[j].Equals(actual[j]))
                {
                    equalCount++;
                }

                if (expected[j].CardSuit == actual[j].CardSuit)
                {
                    shapeCount++;
                }
            }

            double hitRate = (double)equalCount / Deck.NCARDS;
            double shapeRate = (double)shapeCount / Deck.NCARDS;

            // log
            TestContext.WriteLine("\nHit Count: {0}\n Shape Count {1}\n\n", equalCount, shapeCount);
            TestContext.WriteLine("\nHit Rate: {0}\n Shape Rate {1}\n\n", hitRate, shapeRate);

            TestContext.WriteLine("EXPECTED:");
            int i = 0;
            foreach (var item in expected)
            {
                TestContext.WriteLine(item.ToString());
                i++;
            }

            TestContext.WriteLine("\n\nACTUAL:");
            i = 0;
            foreach (var item in actual)
            {
                TestContext.WriteLine(item.ToString());
                i++;
            }

            // hit rate less than 10%
            Assert.IsTrue(hitRate < 0.1);

        }
        #endregion

    }
}