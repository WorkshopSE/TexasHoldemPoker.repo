using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Game;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Core.Tests
{
    [TestClass()]
    public class UserTests
    {

        #region Setup
        private User user;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            user = new User();
        }

        [TestCleanup]
        public void After()
        {
            user = null;
        }
        #endregion

        [TestMethod()]
        public void CreateNewRoomTest()
        {
            //Arrange

            //Act
            Player creator;
            var actRoom = user.CreateNewRoom(1, new NoLimitHoldem(), out creator);

            //Assert
            Assert.IsTrue(user.Players.Contains(creator, new Utility.AddressComparer<Player>()));
        }

        [TestMethod()]
        public void EnterRoomTest()
        {
            //Arrange
            Player creator;
            var expRoom = user.CreateNewRoom(1, new NoLimitHoldem() { Name = "test room" }, out creator);
            creator.Nickname = "test player";

            //Act
            try
            {
                user.EnterRoom(expRoom);
                Assert.Fail("expected exception");
            }
            catch (CrossUtility.Exceptions.RoomRulesException e)
            {
                TestContext.WriteLine(e.Message);
            }
            var user2 = new User();
            var player2 = user2.EnterRoom(expRoom);

            //Assert
            Assert.IsTrue(user.Players.Contains(creator, new Utility.AddressComparer<Player>()));
            Assert.IsTrue(user2.Players.Contains(player2, new Utility.AddressComparer<Player>()));
        }

        [TestMethod]
        public void JoinNextHand()
        {
            //Arrange
            Player creator;
            var room = user.CreateNewRoom(1, new NoLimitHoldem() { Name = "test room" }, out creator);

            //Act
            try
            {
                user.JoinNextHand(new Player(), 0, room.Preferences.BuyInCost);
                Assert.Fail("expected exception");
            }
            catch (CrossUtility.Exceptions.PlayerNotFoundException)
            {
            }

            user.JoinNextHand(creator, 0, room.Preferences.BuyInCost + 20.2);

            //Assert
            Assert.AreEqual(Player.State.ActiveFolded, user.Players.Single().CurrentState);
        }

        [TestMethod()]
        public void StandUpToSpactateTest()
        {
            //Arrange
            Player creator;
            var room = user.CreateNewRoom(1, new NoLimitHoldem() { Name = "test room" }, out creator);
            user.JoinNextHand(creator, 0, room.Preferences.BuyInCost + 20.2);
            user.Players.Single().CurrentState = Player.State.ActiveFolded;

            //Act
            try
            {
                user.StandUpToSpactate(new Player());
                Assert.Fail("expected exception");
            }
            catch (CrossUtility.Exceptions.PlayerNotFoundException)
            {
            }

            var act = user.StandUpToSpactate(user.Players.Single());

            //Assert
            Assert.AreEqual(Player.State.Passive, user.Players.Single().CurrentState);
            Assert.AreEqual(room.Preferences.BuyInCost + 20.2, act);
        }

        [TestMethod()]
        public void ExitRoomTest()
        {
            //Arrange
            Player player;
            user.CreateNewRoom(1, new NoLimitHoldem(), out player);
            user.UserStatistics.AddHandStatistic(130);
            player.PlayerStatistics.AddHandStatistic(120);
            player.PlayerStatistics.AddHandStatistic(-120);

            //Act
            user.ExitRoom(player);
            var res1 = user.UserStatistics.GamesPlayed == 3 &&
                        user.UserStatistics.GrossProfits == 250 &&
                        user.UserStatistics.GrossLosses == 120;


            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(!user.Players.Contains(player));
        }

        [TestMethod()]
        public void GetWinRateStatisticsTest()
        {
            //Arrange
            Player player1 = new Player("a");
            Player player2 = new Player("b");
            user.Players.Add(player1);
            user.Players.Add(player2);
            user.UserStatistics.AddHandStatistic(130);
            user.UserStatistics.AddHandStatistic(-70);
            player1.AddStatistics(20);
            player1.AddStatistics(-10);
            player2.AddStatistics(-20);
            
            //Assert
            Assert.AreEqual(user.GetWinRateStatistics(), 10);
        }

        [TestMethod()]
        public void GetGrossProfitWinRateStatisticsTest()
        {
            //Arrange
            Player player1 = new Player("a");
            Player player2 = new Player("b");
            user.Players.Add(player1);
            user.Players.Add(player2);
            user.UserStatistics.AddHandStatistic(130);
            user.UserStatistics.AddHandStatistic(-70);
            player1.AddStatistics(20);
            player1.AddStatistics(-10);
            player2.AddStatistics(-20);

            //Assert
            Assert.AreEqual(user.GetGrossProfitWinRateStatistics(), 30);
        }
    }
}