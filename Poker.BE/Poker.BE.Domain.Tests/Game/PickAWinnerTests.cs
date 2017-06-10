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
    public class PickAWinnerTests
    {
        [TestMethod()]
        public void GetWinnerTest()
        {
            //Arrange
            Player player1 = new Player("a");
            Player player2 = new Player("b");
            Player player3 = new Player("c");
            player1.PrivateCards = new Card[2] { new Card(Card.Suit.Hearts, Card.Value.Queen), new Card(Card.Suit.Hearts, Card.Value.Three) };
            player2.PrivateCards = new Card[2] { new Card(Card.Suit.Hearts, Card.Value.Eight), new Card(Card.Suit.Spades, Card.Value.Nine) };
            player3.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Seven), new Card(Card.Suit.Diamonds, Card.Value.Six) };
            List<Player> players = new List<Player>();
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            Card card1 = new Card(Card.Suit.Clubs, Card.Value.Eight);
            Card card2 = new Card(Card.Suit.Hearts, Card.Value.King);
            Card card3 = new Card(Card.Suit.Hearts, Card.Value.Ten);
            Card card4 = new Card(Card.Suit.Diamonds, Card.Value.Nine);
            Card card5 = new Card(Card.Suit.Hearts, Card.Value.Nine);
            Card[] tableCards = new Card[5] { card1, card2, card3, card4, card5 };
            PickAWinner pickAWinner;
            List<Player> winners;

            //Act
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res1 = winners.ElementAt(0).Nickname == "b" && winners.Count == 1;

            player1.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Ace), new Card(Card.Suit.Hearts, Card.Value.Two) };
            player2.PrivateCards = new Card[2] { new Card(Card.Suit.Clubs, Card.Value.Ace), new Card(Card.Suit.Clubs, Card.Value.Seven) };
            player3.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Queen), new Card(Card.Suit.Diamonds, Card.Value.Six) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res2 = winners.Count == 2 && winners.ElementAt(0).Nickname == "a" && winners.ElementAt(1).Nickname == "b";

            player3.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Ten), new Card(Card.Suit.Diamonds, Card.Value.Six) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res3 = winners.Count == 1 && winners.ElementAt(0).Nickname == "c";

            player2.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Nine), new Card(Card.Suit.Spades, Card.Value.Six) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res4 = winners.Count == 1 && winners.ElementAt(0).Nickname == "b";

            player1.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Seven), new Card(Card.Suit.Clubs, Card.Value.Six) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res5 = winners.Count == 1 && winners.ElementAt(0).Nickname == "a";

            player3.PrivateCards = new Card[2] { new Card(Card.Suit.Hearts, Card.Value.Ace), new Card(Card.Suit.Hearts, Card.Value.Jack) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res6 = winners.Count == 1 && winners.ElementAt(0).Nickname == "c";

            player2.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Nine), new Card(Card.Suit.Clubs, Card.Value.Nine) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res7 = winners.Count == 1 && winners.ElementAt(0).Nickname == "b";

            player1.PrivateCards = new Card[2] { new Card(Card.Suit.Hearts, Card.Value.Queen), new Card(Card.Suit.Hearts, Card.Value.Jack) };
            pickAWinner = new PickAWinner(players, tableCards);
            winners = pickAWinner.GetWinners();
            var res8 = winners.Count == 1 && winners.ElementAt(0).Nickname == "a";

            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
            Assert.IsTrue(res3);
            Assert.IsTrue(res4);
            Assert.IsTrue(res5);
            Assert.IsTrue(res6);
            Assert.IsTrue(res7);
            Assert.IsTrue(res8);
        }
    }
}