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
    public class RoomTests
    {

        private TestContext testContext;

        public TestContext TestContext { get { return testContext; } set { testContext = value; } }

        [TestMethod]
        public void RoomTest()
        {
            //Arrange
            Player player = new Player();
            GamePreferences preferences = new GamePreferences();

            //Act
            var result = new Room(player, preferences);

            //Assert
            TestContext.WriteLine("end of roomTest");
        }

        //TODO tests

        //[TestMethod]
        //public void StartNewHandTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void SendMessageTest()
        //{
        //    throw new NotImplementedException();
        //}

    }
}