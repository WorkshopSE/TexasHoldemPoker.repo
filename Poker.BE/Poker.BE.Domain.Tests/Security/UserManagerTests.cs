using Poker.BE.CrossUtility.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Core;

namespace Poker.BE.Domain.Security.Tests
{
    [TestClass()]
    public class UserManagerTests
    {

        #region Setup
        private UserManager userManager;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            userManager = UserManager.Instance;
        }

        [TestCleanup]
        public void After()
        {
            userManager.Clear();
        }
        #endregion

        [TestMethod()]
        public void AddUserTest()
        {
            //Arrange
            var userManager = UserManager.Instance;

            //Act
            var res1 = userManager.AddUser("yossi", "password", 100);
            Exception res2 = null;
            try
            {
                userManager.AddUser("yossi", "mypassword", 200);
            }
            catch (Exception ex)
            {
                res2 = ex;
            }

            userManager.Clear();
            var res3 = userManager.AddUser("yossi", "mypassword", 200);
            Exception res4 = null;
            try
            {
                userManager.AddUser("dana", "pass", 100);
            }
            catch (Exception ex)
            {
                res4 = ex;
            }
            Exception res5 = null;
            try
            {
                userManager.AddUser("bill", "password", -100);
            }
            catch (Exception ex)
            {
                res5 = ex;
            }

            //Assert
            Assert.IsTrue(userManager.Users.ContainsKey(res1.UserName));
            Assert.IsNotNull(res2);
            Assert.IsTrue(userManager.Users.ContainsKey(res3.UserName));
            Assert.IsNotNull(res4);
            Assert.IsNotNull(res5);
        }

        [TestMethod()]
        public void LogInTest()
        {
            //Arrange
            var userManager = UserManager.Instance;

            //Act
            Exception res1 = null;
            var user1 = default(User);
            try
            {
                user1 = userManager.Login("GAL", "password");
            }
            catch (Exception ex)
            {
                res1 = ex;
            }

            var user = userManager.AddUser("yossi", "mypassword", 200);
            Exception res2 = null;
            try
            {
                userManager.Login("yossi", "password");
            }
            catch (Exception ex)
            {
                res2 = ex;
            }

            var res3 = userManager.Login("yossi", "mypassword") != null;

            //Assert
            Assert.IsNotNull(res1);
            //Assert.IsNotNull(res2);
            Assert.IsTrue(user.IsConnected);
        }

        //[TestMethod()]
        //public void EditProfileTest()
        //{
        //    // TODO Gal / Tomer
        //    throw new NotImplementedException();
        //}

    }
}