using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
    [TestClass()]
    public class RoundTests
    {
        [TestMethod()]
        public void RoundTest()
        {
            //Is this count as simple constructor??
        }

        [TestMethod()]
        public void PlayMoveTest()
        {
            //Arrange
            var player1 = new Player();
            var player2 = new Player();
            var player3 = new Player();
            player1.AddMoney(500);
            player2.AddMoney(350);
            player3.AddMoney(600);
            var activeUnfoldedPlayers = new List<Player>();
            activeUnfoldedPlayers.Add(player1);
            activeUnfoldedPlayers.Add(player2);
            activeUnfoldedPlayers.Add(player3);
            var pot = new Pot(null);
            var round = new Round(player1, activeUnfoldedPlayers, pot);
            Exception expectedException = new Exception();

            //Act
            round.PlayMove(Round.Move.check, 0);
            var res1 = round.CurrentPlayer == player2;

            try
            {
                round.PlayMove(Round.Move.raise, 50);
            }
            catch (IOException e)
            {
                expectedException = e;
            }
            round.PlayMove(Round.Move.bet, 50);
            //var res2 = round.CurrentPlayer == player3 && round.CurrentPot.AmountToClaim

            //Assert
            Assert.IsTrue(res1);
            Assert.AreEqual(expectedException.Message, "Can't raise if no one had bet before... use bet move");

        }
    }
}