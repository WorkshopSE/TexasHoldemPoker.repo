using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Security.Tests
{
    [TestClass()]
    public class UserManagerTests
    {
        [TestMethod()]
        public void UserManagerTest()
        {
            // simple constructor
        }

        [TestMethod()]
        public void AddUserTest()
        {
            //Arrange
            var userManager = new UserManager();

            //Act
            var res1 = userManager.AddUser("yossi", "password", 100);
            var res2 = userManager.AddUser("yossi", "mypassword", 200);
            userManager.RemoveUser("yossi");
            var res3 = userManager.AddUser("yossi", "mypassword", 200);
            var res4 = userManager.AddUser("dana", "pass", 100);
            var res5 = userManager.AddUser("bill", "password", -100);

            //Assert
            Assert.IsTrue(res1);
            Assert.IsFalse(res2);
            Assert.IsTrue(res3);
            Assert.IsFalse(res4);
            Assert.IsFalse(res5);
        }

        [TestMethod()]
        public void RemoveUserTest()
        {
            //Arrange
            var userManager = new UserManager();

            //Act
            userManager.AddUser("yossi", "password", 100);
            var res1 = userManager.RemoveUser("yossi");
            var res2 = userManager.RemoveUser("yossi");

            //Assert
            Assert.IsTrue(res1);
            Assert.IsFalse(res2);
        }

        [TestMethod()]
        public void LogInTest()
        {
            //Arrange
            var userManager = new UserManager();

            //Act
            var res1 = userManager.LogIn("yossi", "password") != null;
            userManager.AddUser("yossi", "mypassword", 200);
            var res2 = userManager.LogIn("yossi", "password") != null;
            var res3 = userManager.LogIn("yossi", "mypassword") != null;

            //Assert
            Assert.IsFalse(res1);
            Assert.IsFalse(res2);
            Assert.IsTrue(res3);
        }

        [TestMethod()]
        public void LogOutTest()
        {
            // simple code
        }

        [TestMethod()]
        public void EditProfileTest()
        {
            //Arrange
            var userManager = new UserManager();

            //Act
            var res1 = userManager.EditProfile("yossi", "dana", "password", "hat");
            userManager.AddUser("yossi", "mypassword", 200);
            var res2 = userManager.EditProfile("yossi", "dana", "pass", "hat");
            var res3 = userManager.EditProfile("yossi", "dana", "password", "hat");
            userManager.AddUser("dana", "danapassword", 200);
            var res4 = userManager.EditProfile("yossi", "dana", "password", "hat");

            //Assert
            Assert.IsFalse(res1);
            Assert.IsFalse(res2);
            Assert.IsTrue(res3);
            Assert.IsFalse(res4);
        }
        
    }
}