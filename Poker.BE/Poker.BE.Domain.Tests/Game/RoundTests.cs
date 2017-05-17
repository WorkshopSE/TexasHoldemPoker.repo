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
            var player1 = new Player();
            var player2 = new Player();
            var player3 = new Player();
            var activeUnfoldedPlayers = new List<Player>();
            //var round = new Round(player1, )
        }
    }
}