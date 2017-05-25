using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Utility.Exceptions;
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
            var player1 = new Player() { Nickname = "test player 1" };
            var player2 = new Player() { Nickname = "test player 2" };
            var player3 = new Player() { Nickname = "test player 3" };
            player1.AddMoney(500);
            player2.AddMoney(350);
            player3.AddMoney(600);
            var activeUnfoldedPlayers = new List<Player>();
            activeUnfoldedPlayers.Add(player1);
            activeUnfoldedPlayers.Add(player2);
            activeUnfoldedPlayers.Add(player3);
            var pot = new Pot(null);
            var gameConfig = new GameConfig();
            var round = new Round(player1, activeUnfoldedPlayers, pot, true, gameConfig);
            Exception expectedException1 = new Exception();
            Exception expectedException2 = new Exception();

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
            catch (GameRulesException e)
            {
                expectedException1 = e;
            }

            round.PlayMove(Round.Move.raise, 70);
            var res3 = round.CurrentPlayer == player1 && pot.PlayersClaimPot.Contains(player3) && round.LiveBets[player3] == 120
                        && pot.AmountToClaim == 120 && pot.Value == 220 && round.LastRaise == 70 && round.TotalRaise == 120;

            round.PlayMove(Round.Move.allin, 0);
            var res4 = round.CurrentPlayer == player2 && pot.PlayersClaimPot.Contains(player1) && round.LiveBets[player1] == 500
                        && pot.AmountToClaim == 500 && pot.Value == 670 && round.LastRaise == 380 && round.TotalRaise == 500
                        && pot.PartialPot != null && pot.PartialPot.AmountToClaim == 0 && pot.PartialPot.Value == 0;

            round.PlayMove(Round.Move.allin, 0);
            var res5 = round.CurrentPlayer == player3 && round.CurrentPot.PlayersClaimPot.Contains(player1) && round.CurrentPot.PlayersClaimPot.Contains(player2)
                        && !round.CurrentPot.PartialPot.PlayersClaimPot.Contains(player2) && round.CurrentPot.PartialPot.PlayersClaimPot.Contains(player1)
                        && round.LiveBets[player2] == 350 && round.CurrentPot.AmountToClaim == 350 && round.CurrentPot.Value == 820
                        && round.CurrentPot.PartialPot.Value == 150 && round.CurrentPot.PartialPot.AmountToClaim == 150;

            try
            {
                round.PlayMove(Round.Move.allin, 0);
            }
            catch (GameRulesException e)
            {
                expectedException2 = e;
            }

            round.PlayMove(Round.Move.fold, 0);
            var res6 = !round.CurrentPot.PlayersClaimPot.Contains(player3) && !round.CurrentPot.PartialPot.PlayersClaimPot.Contains(player3)
                        && round.LiveBets[player3] == 120 && round.CurrentPot.AmountToClaim == 350 && round.CurrentPot.Value == 820
                        && round.CurrentPot.PartialPot.Value == 150 && round.CurrentPot.PartialPot.AmountToClaim == 150;

            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
            Assert.AreEqual(expectedException1.Message, "Can't raise less than last raise");
            Assert.IsTrue(res3);
            Assert.IsTrue(res4);
            Assert.IsTrue(res5);
            Assert.AreEqual(expectedException2.Message, "all-in is bigger than the highest other player's all-in... use bet\raise move");
            Assert.IsTrue(res6);


        }
    }
}