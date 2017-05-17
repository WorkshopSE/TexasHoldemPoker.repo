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
    public class TurnTests
    {
        //[TestMethod()]
        //public void CheckTest()
        //{
        //    throw new NotImplementedException();
        //}

        [TestMethod()]
        public void CallTest()
        {
            //Arrange
            var player = new Player();
            var turn = new Turn(player);
            Exception expectedException = null;

            //Act
            try
            {
                turn.Call(50);
            }
            catch (NotEnoughMoneyException e)
            {
                expectedException = e;
            }

            player.AddMoney(200);
            turn.Call(50);
            var res2 = player.Wallet.amountOfMoney != 150;

            //Assert
            Assert.AreEqual(expectedException.Message, "Player doesn't have enough money!");
            Assert.IsFalse(res2);
        }

        [TestMethod()]
        public void FoldTest()
        {
            //Arrange
            var player = new Player();
            var turn = new Turn(player);

            //Act
            turn.Fold();
            var res1 = turn.CurrentPlayer.CurrentState == Player.State.ActiveFolded;

            //Assert
            Assert.IsTrue(res1);
        }

        [TestMethod()]
        public void BetTest()
        {
            //Arrange
            var player = new Player();
            var turn = new Turn(player);
            Exception expectedException = null;

            //Act
            try
            {
                turn.Bet(50);
            }
            catch (NotEnoughMoneyException e)
            {
                expectedException = e;
            }

            player.AddMoney(200);
            turn.Bet(50);
            var res2 = player.Wallet.amountOfMoney != 150;

            //Assert
            Assert.AreEqual(expectedException.Message, "Player doesn't have enough money!");
            Assert.IsFalse(res2);
        }

        [TestMethod()]
        public void RaiseTest()
        {
            //Arrange
            var player = new Player();
            var turn = new Turn(player);
            Exception expectedException = null;

            //Act
            try
            {
                turn.Raise(50);
            }
            catch (NotEnoughMoneyException e)
            {
                expectedException = e;
            }

            player.AddMoney(200);
            turn.Raise(50);
            var res2 = player.Wallet.amountOfMoney != 150;

            //Assert
            Assert.AreEqual(expectedException.Message, "Player doesn't have enough money!");
            Assert.IsFalse(res2);
        }

        [TestMethod()]
        public void AllInTest()
        {
            //Arrange
            var player = new Player();
            var turn = new Turn(player);
            Exception expectedException = null;

            //Act
            try
            {
                turn.AllIn();
            }
            catch (ArgumentException e)
            {
                expectedException = e;
            }

            player.AddMoney(200);
            turn.AllIn();
            var res1 = player.Wallet.amountOfMoney == 0;
            var res2 = player.CurrentState == Player.State.ActiveAllIn;

            //Assert
            Assert.AreEqual(expectedException.Message, "You're already all-in!!");
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
        }
    }
}