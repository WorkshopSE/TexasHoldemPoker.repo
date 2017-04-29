using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
    [TestClass()]
    public class ChairTests
    {
        [TestMethod]
        public void ChairTest()
        {
            //simple constructor
        }

        [TestMethod]
        public void TakeTest()
        {
            //Arrange
            var chair = new Chair(0);

            //Act
            Task<bool> t1 = Task<bool>.Factory.StartNew(() => { return chair.Take(); });
            Task<bool> t2 = Task<bool>.Factory.StartNew(() => { return chair.Take(); });

            //Assert
            Task.WaitAll(new Task[] { t1, t2 });
            Assert.IsFalse(t1.Result == t2.Result);

        }

        [TestMethod]
        public void ReleaseTest()
        {
            //Arrange
            var chair = new Chair(0);


            //Act
            var res1 = chair.Take();
            var res2 = chair.Take();
            chair.Release();
            var res3 = chair.Take();

            //Assert
            Assert.IsTrue(res1);
            Assert.IsFalse(res2);
            Assert.IsTrue(res3);
        }
    }
}