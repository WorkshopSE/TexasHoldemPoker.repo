using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Core.Tests
{
    [TestClass()]
    public class UserTests
    {

        #region Setup
        private User user;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            user = new User();
        }

        [TestCleanup]
        public void After()
        {
            user = null;
        }
        #endregion

        // TODO: Tomer / Ariel: unit testing for user class
        //[TestMethod()]
        //public void UserTest()
        //{

        //}

        //[TestMethod()]
        //public void ConnectTest()
        //{

        //}

        //[TestMethod()]
        //public void DisconnectTest()
        //{

        //}

        [TestMethod()]
        public void EnterRoomTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CreateNewRoomTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void JoinNextHandTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void StandUpToSpactateTest()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}