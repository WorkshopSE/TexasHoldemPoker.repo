using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.API.Controllers;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.API.Controllers.Tests
{
    [TestClass()]
    public class PokerGamePlayControllerTests
    {
        #region Setup
        private PokerGamePlayController ctrl;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            ctrl = new PokerGamePlayController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
        }

        [TestCleanup]
        public void After()
        {
            ((PokerGamePlayService)ctrl.Service).Clear();
            ctrl = null;
        }
        #endregion

        [TestMethod()]
        public void PlayMoveTest()
        {
            //TODO
        }
    }
}