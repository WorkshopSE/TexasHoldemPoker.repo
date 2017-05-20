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
    public class RoomTests
    {

        #region Setup
        public TestContext TestContext { get; set; }
        private Room room;

        [TestInitialize]
        public void Before()
        {
            room = new Room();
        }

        [TestCleanup]
        public void After()
        {
            room = null;
        }
        #endregion

        [TestMethod]
        public void RoomTest()// (:Player, :Preferences)
        {
            //Arrange
            var player = new Player();
            var preferences = new GamePreferences();
            var expected = new Room(player, preferences);


            //Act
            var actual = new Room(player, preferences);

            //Assert

        }

        [TestMethod()]
        public void RoomTest2() // (:Player)
        {
            //Arrange
            var player = new Player();
            var expected = player;

            //Act
            var actual = new Room(player);

            //Assert
            Assert.IsTrue(actual.Players.Count == 1);
            Assert.AreEqual(expected, actual.Players.First());
            Assert.IsTrue(actual.PassivePlayers.Contains(expected));
        }

        [TestMethod()]
        public void RoomTest3() // (:Player, :GameConfig)
        {
            //Arrange
            var player = new Player();
            var confing = new GameConfig() {
                BuyInCost = 50.2,
                GamePrefrences = new GamePreferences(),
                IsSpactatorsAllowed = false,
                MaxNumberOfActivePlayers = 8,
                MaxNumberOfPlayers = 9,
                MinimumBet = 
            };
            var expected = ;

            //Act
            var actual = 1;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void JoinPlayerToTableTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RemovePlayerTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClearAllTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CreatePlayerTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void StartNewHandTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TakeChairTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void LeaveChairTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}