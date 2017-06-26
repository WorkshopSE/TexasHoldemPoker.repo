using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Poker.BE.API.Controllers.Tests
{
    [TestClass()]
    public class LeagueControllerTests
    {

        #region Setup
        private LeagueController _ctrl;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            _ctrl = new LeagueController() {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration(),
            };
        }

        [TestCleanup]
        public void After()
        {

        }
        #endregion

        [TestMethod()]
        public void GetAllLeaguesTest()
        {
            // TODO - Gal / Tomer please write this test

            //Arrange

            //Act

            //Assert
        }
    }
}