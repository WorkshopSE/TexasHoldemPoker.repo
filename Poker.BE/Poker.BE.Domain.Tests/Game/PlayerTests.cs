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
        [TestMethod()]
        public void AddMoneyTest()
        {
            //Arrange
            var player = new Player();

            //Act
            player.AddMoney(100);

            //Assert
            Assert.AreEqual(player.Wallet.amountOfMoney, 100);
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
            Assert.AreEqual(player.Wallet.amountOfMoney, 50);
        }
    }
}