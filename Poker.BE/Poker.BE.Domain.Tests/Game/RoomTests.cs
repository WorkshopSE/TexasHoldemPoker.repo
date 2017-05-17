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

        private TestContext testContext;

        public TestContext TestContext { get { return testContext; } set { testContext = value; } }

        [TestMethod]
        public void RoomTest()// (:Player, :Preferences)
        {
            //Arrange
            Player player = new Player();
            GamePreferences preferences = new GamePreferences();

            //Act
            var result = new Room(player, preferences);

            //Assert
            TestContext.WriteLine("end of roomTest");
        }

        [TestMethod()]
        public void RoomTest1() // ()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RoomTest2() // (:Player)
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RoomTest3() // (:Player, :GameConfig)
        {
            // TODO
            throw new NotImplementedException();
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