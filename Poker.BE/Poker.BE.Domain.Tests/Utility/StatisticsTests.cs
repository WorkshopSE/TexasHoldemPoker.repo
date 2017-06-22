using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Tests
{
    [TestClass()]
    public class StatisticsTests
    {
        #region Setup
        private Statistics statistics;

        [TestInitialize]
        public void Before()
        {
            statistics = new Statistics();
        }

        [TestCleanup]
        public void After()
        {
            statistics = null;
        }
        #endregion

        [TestMethod()]
        public void AddHandStatisticTest()
        {
            //Act
            statistics.AddHandStatistic(150.5);
            statistics.AddHandStatistic(49.5);
            statistics.AddHandStatistic(-50);
            var res1 = statistics.GamesPlayed == 3 &&
                        statistics.GrossProfits == 200 &&
                        statistics.GrossLosses == 50;

            //Assert
            Assert.IsTrue(res1);
        }

        [TestMethod()]
        public void CombineStatisticsTest()
        {
            //Arrange
            statistics.AddHandStatistic(125);
            statistics.AddHandStatistic(-30);
            var newStatistics = new Statistics();
            newStatistics.AddHandStatistic(75);
            newStatistics.AddHandStatistic(-70);

            //Act
            statistics.CombineStatistics(newStatistics);
            var res1 = statistics.GamesPlayed == 4 &&
                        statistics.GrossProfits == 200 &&
                        statistics.GrossLosses == 100;

            //Assert
            Assert.IsTrue(res1);
        }

        [TestMethod()]
        public void GetWinRateTest()
        {
            //Arrange
            statistics.AddHandStatistic(120);
            statistics.AddHandStatistic(30);
            statistics.AddHandStatistic(-20);
            statistics.AddHandStatistic(-20);

            //Assert
            Assert.AreEqual(statistics.GetWinRate(), 27.5);
        }

        [TestMethod()]
        public void GetGrossProfitWinRateTest()
        {
            //Arrange
            statistics.AddHandStatistic(120);
            statistics.AddHandStatistic(30);
            statistics.AddHandStatistic(-20);
            statistics.AddHandStatistic(-20);

            //Assert
            Assert.AreEqual(statistics.GetGrossProfitWinRate(), 37.5);
        }
    }
}