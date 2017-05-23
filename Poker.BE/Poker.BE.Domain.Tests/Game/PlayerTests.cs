using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Utility.Exceptions;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        #region Set Up
        private Player player = default(Player);

        [TestInitialize]
        public void Before()
        {
            player = new Player();
        }

        [TestCleanup]
        public void After()
        {
            player = null;
        }
        #endregion


        [TestMethod()]
        public void PlayerTest()
        {
            // simple constructor
        }

        [TestMethod()]
        public void JoinToTableTest()
        {
            //Arrange
            var expected1 = true;
            var expected2 = Player.State.Passive;
            var expected3 = Player.State.ActiveUnfolded;

            //Act
            var actual2 = player.CurrentState;
            var actual1 = player.JoinToTable(100.0);
            var actual3 = player.CurrentState;

            //Assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
        }

        private class PlayerStub : Player
        {
            public void Fold()
            {
                CurrentState = State.ActiveFolded;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Utility.Exceptions.PlayerModeException))]
        public void StandUpTest() // already a spectator
        {
            player = new PlayerStub();
            
            //Arrange
            var expected1 = 10.3;
            var expected0 = Player.State.ActiveFolded;
            var expected2 = Player.State.Passive;

            //Act
            var actual0 = player.CurrentState;
            var actual1 = player.StandUp();
            var actual2 = player.CurrentState;

            //Assert
            Assert.AreEqual(expected0, actual0);
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod()]
        [ExpectedException(typeof(Utility.Exceptions.PlayerModeException))]
        public void StandUpTest1() // need to fold first
        {
            player = new PlayerStub();

            //Arrange
            var expected1 = 10.3;
            var expected0 = Player.State.ActiveFolded;
            var expected2 = Player.State.Passive;

            //Act
            var actual0 = player.CurrentState;
            player.JoinToTable(10.3);
            var actual1 = player.StandUp();
            var actual2 = player.CurrentState;

            //Assert
            Assert.AreEqual(expected0, actual0);
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod()]
        public void StandUpTest2() // good
        {
            // shadowing
            var player = new PlayerStub();

            //Arrange
            var expected1 = 10.3;
            var expected0 = Player.State.ActiveFolded;
            var expected2 = Player.State.Passive;

            //Act
            player.JoinToTable(10.3);
            player.Fold();

            var actual0 = player.CurrentState;
            var actual1 = player.StandUp();
            var actual2 = player.CurrentState;

            //Assert
            Assert.AreEqual(expected0, actual0);
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod()]
        public void AddMoneyTest()
        {
            //Arrange
            var player = new Player();

            //Act
            player.AddMoney(100);

            //Assert
            Assert.AreEqual(player.Wallet.AmountOfMoney, 100);
        }

        [TestMethod()]
        public void SubstractMoneyTest()
        {
            //Arrange
            var player = new Player();
            Exception expectedException = null;

            //Act
            try
            {
                player.SubstractMoney(100);
            }
            catch (NotEnoughMoneyException e)
            {
                expectedException = e;
            }

            player.AddMoney(150);
            player.SubstractMoney(100);

            //Assert
            Assert.AreEqual(expectedException.Message, "Player doesn't have enough money!");
            Assert.AreEqual(player.Wallet.AmountOfMoney, 50);
        }

        [TestMethod]
        public void Equals()
        {
            //Arrange
            var other = new Player();

            //Act
            var afalse = player.Equals(other);
            player.Nickname = "test";
            other.Nickname = "test";
            var atrue = player.Equals(other);

            //Assert
            Assert.IsTrue(atrue);
            Assert.IsFalse(afalse);
        }
    }
}