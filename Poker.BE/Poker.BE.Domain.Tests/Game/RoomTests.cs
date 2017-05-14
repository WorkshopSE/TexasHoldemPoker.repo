using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Exceptions;
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

        [TestMethod()]
        //[ExpectedException(typeof(NotEnoughPlayersException))]
        public void StartNewHandTest()
        {
            //Arrange
            Player player1 = new Player();
            GamePreferences preferences = new GamePreferences();
            GameCenter center = new GameCenter();
            Room room = new Room(player1, preferences);
            Exception expectedExcetpion = null;
            //Act
            try
            {
                room.StartNewHand();
            }
            catch (NotEnoughPlayersException ex)
            {
                expectedExcetpion = ex;
            }
            // Assert - precondition lower than 2 players
            Assert.AreEqual(expectedExcetpion.Message, "Its should be at least 2 active players to start new hand!");

            Player player2 = center.EnterRoom(room);
            try
            {
                room.StartNewHand();
            }
            catch (NotEnoughPlayersException ex)
            {
                expectedExcetpion = ex;
            }
            // Assert - precondition no active players
            Assert.AreEqual(expectedExcetpion.Message, "Its should be at least 2 active players to start new hand!");

            //More Test when UC020: Join Next Hand completed, needed for make players Active

        }

        [TestMethod()]
        public void ChoosePlayMoveTest()
        {
            //Arrange
            Player player1 = new Player();
            GamePreferences preferences = new GamePreferences();
            GameCenter center = new GameCenter();
            Room room = new Room(player1, preferences);
            Exception expectedExcetpion = null;
            //Act
            try
            {
                room.ChoosePlayMove(Round.Move.call);
            }
            catch (NotEnoughPlayersException ex)
            {
                expectedExcetpion = ex;
            }
            // Assert - precondition lower than 2 players
            Assert.AreEqual(expectedExcetpion.Message, "Its should be at least 2 active players to play move");

            Player player2 = center.EnterRoom(room);
            try
            {
                room.ChoosePlayMove(Round.Move.call);
            }
            catch (NotEnoughPlayersException ex)
            {
                expectedExcetpion = ex;
            }
            // Assert - precondition no active players
            Assert.AreEqual(expectedExcetpion.Message, "Its should be at least 2 active players to play move");

            //More Test when UC020: Join Next Hand completed, needed for make players Active
        }
    }
}