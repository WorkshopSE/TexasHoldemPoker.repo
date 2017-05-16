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
    public class GameCenterTests
    {
        #region Set Up
        
        public TestContext TestContext { get; set; }
        private GameCenter gameCenter;

        [TestInitialize]
        public void Before()
        {
            gameCenter = GameCenter.Instance;
        }

        [TestCleanup]
        public void After()
        {
            gameCenter = null;
        }
        #endregion

        [TestMethod()]
        public void GameCenterTest()
        {
            // TODO - simple constructor - may not need to test?
            throw new NotImplementedException();
        }

        #region Find an Existing Room - Functions Overloading Tests
        [TestMethod()]
        public void FindRoomsByCriteriaTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FindRoomsByCriteriaTest1()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FindRoomsByCriteriaTest2()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FindRoomsByCriteriaTest3()
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void EnterRoomTest()
        {
            //Arrange
            var expected = new Player();
            var actual = default(Player);
            Room room = new Room(new Player());

            //Act
            actual = gameCenter.EnterRoom(room);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateNewRoomTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void JoinNextHandTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void StandUpToSpactateTest()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}