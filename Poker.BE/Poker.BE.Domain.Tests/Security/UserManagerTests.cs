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
			userManager.RemoveUser("yossi");
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
			Assert.IsTrue(userManager.IsUserExists(res1.UserName));
			Assert.IsNotNull(res2);
			Assert.IsTrue(userManager.IsUserExists(res3.UserName));
			Assert.IsNotNull(res4);
			Assert.IsNotNull(res5);
		}

		[TestMethod()]
		public void RemoveUserTest()
		{
            //Arrange
            var userManager = UserManager.Instance;

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
            var userManager = UserManager.Instance;

			//Act
			Exception res1 = null;
			try
			{
				userManager.LogIn("GAL", "password");
			}
			catch (Exception ex)
			{
				res1 = ex;
			}
			var user = userManager.AddUser("yossi", "mypassword", 200);
			Exception res2 = null;
			try
			{
				userManager.LogIn("yossi", "password");
			}
			catch (Exception ex)
			{
				res2 = ex;
			}
			var res3 = userManager.LogIn("yossi", "mypassword") != null;

			//Assert
			Assert.IsNotNull(res1);
			Assert.IsNotNull(res2);
			Assert.IsTrue(user.IsConnected);
		}

		[TestMethod()]
		public void EditProfileTest()
		{
            //Arrange
            var userManager = UserManager.Instance;

			//Act
			var res1 = userManager.EditProfile("yossi", "yossi", "password", "hat");
			userManager.AddUser("yossi", "mypassword", 200);
			var res2 = userManager.EditProfile("yossi", "yossi", "pass", "hat");
			var res3 = userManager.EditProfile("yossi", "yossi", "password", "hat");
			userManager.AddUser("dana", "danapassword", 200);
			var res4 = userManager.EditProfile("yossi", "dana", "password", "hat");
			var res5 = userManager.EditProfile("yossi", "bill", "password", "hat");

			//Assert
			Assert.IsFalse(res1);
			Assert.IsFalse(res2);
			Assert.IsTrue(res3);
			Assert.IsFalse(res4);
			Assert.IsTrue(res5);
		}

	}
}