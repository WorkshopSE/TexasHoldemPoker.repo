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
            round.PlayMove(Round.Move.bet, 50);
            var res1 = round.CurrentPlayer == player2 && pot.PlayersClaimPot.Contains(player1) && round.LiveBets[player1] == 50
                        && pot.AmountToClaim == 50 && pot.Value == 50 && round.LastRaise == 50 && round.TotalRaise == 50;

            round.PlayMove(Round.Move.call, 0);
            var res2 = round.CurrentPlayer == player3 && pot.PlayersClaimPot.Contains(player2) && round.LiveBets[player2] == 50
                        && pot.AmountToClaim == 50 && pot.Value == 100 && round.LastRaise == 50 && round.TotalRaise == 50;

            try
            {
                round.PlayMove(Round.Move.raise, 20);
            }
            catch(ArgumentException e)
            {
                expectedException = e;
            }

            round.PlayMove(Round.Move.raise, 70);
            var res3 = round.CurrentPlayer == player1 && pot.PlayersClaimPot.Contains(player3) && round.LiveBets[player3] == 120
                        && pot.AmountToClaim == 120 && pot.Value == 220 && round.LastRaise == 70 && round.TotalRaise == 120;


            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
            Assert.AreEqual(expectedException.Message, "Can't raise less than last raise");
            Assert.IsTrue(res3);

        }
    }
}