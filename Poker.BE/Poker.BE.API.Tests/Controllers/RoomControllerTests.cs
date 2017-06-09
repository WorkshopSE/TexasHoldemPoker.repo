using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.API.Controllers.Tests
{
    [TestClass()]
    public class RoomControllerTests
    {

        #region Setup
        private RoomController ctrl;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            ctrl = new RoomController() {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
        }

        [TestCleanup]
        public void After()
        {
            ctrl = null;
        }
        #endregion

        // UNDONE: @gwainer - gal, please continue my work from here
        //[TestMethod()]
        //public void EnterRoomTest()
        //{
        //    // TODO
        //    throw new NotImplementedException();
        //}
    }
}