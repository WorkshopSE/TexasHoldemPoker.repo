using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
    [TestClass]
    public class HandTests
    {
        [TestMethod()]
        public void EndHandTest()
        {
            //Assert
            var player1 = new Player() { Nickname = "test player 1" };
            var player2 = new Player() { Nickname = "test player 2" };
            var player3 = new Player() { Nickname = "test player 3" };
            player1.AddMoney(500);
            player2.AddMoney(500);
            player3.AddMoney(500);
            var players = new List<Player>();
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            Hand hand = new Hand(player3, players, new NoLimitHoldem());

            player1.PrivateCards = new Card[2] { new Card(Card.Suit.Hearts, Card.Value.Queen), new Card(Card.Suit.Hearts, Card.Value.Three) };
            player2.PrivateCards = new Card[2] { new Card(Card.Suit.Hearts, Card.Value.Eight), new Card(Card.Suit.Spades, Card.Value.Nine) };
            player3.PrivateCards = new Card[2] { new Card(Card.Suit.Spades, Card.Value.Seven), new Card(Card.Suit.Diamonds, Card.Value.Six) };
            Card card1 = new Card(Card.Suit.Clubs, Card.Value.Eight);
            Card card2 = new Card(Card.Suit.Hearts, Card.Value.King);
            Card card3 = new Card(Card.Suit.Hearts, Card.Value.Ten);
            Card card4 = new Card(Card.Suit.Diamonds, Card.Value.Nine);
            Card card5 = new Card(Card.Suit.Hearts, Card.Value.Nine);
            Card[] tableCards = new Card[5] { card1, card2, card3, card4, card5 };
            hand.CommunityCards = tableCards;

            //Act
            hand.CurrentRound.CurrentPot.PlayersClaimPot.Add(player1);
            hand.CurrentRound.CurrentPot.PlayersClaimPot.Add(player2);
            hand.CurrentRound.CurrentPot.PlayersClaimPot.Add(player3);
            hand.CurrentRound.CurrentPot.Value = 300;
            hand.EndHand();
            
            var res1 = player2.Wallet.AmountOfMoney == 800;

            hand.CurrentRound.CurrentPot.PartialPot = new Pot();
            hand.CurrentRound.CurrentPot.PartialPot.PlayersClaimPot.Add(player1);
            hand.CurrentRound.CurrentPot.PartialPot.PlayersClaimPot.Add(player3);
            hand.CurrentRound.CurrentPot.PartialPot.Value = 200;
            hand.EndHand();

            var res2 = player1.Wallet.AmountOfMoney == 700 && player2.Wallet.AmountOfMoney == 1100 && player3.Wallet.AmountOfMoney == 500;

            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
        }
    }
}