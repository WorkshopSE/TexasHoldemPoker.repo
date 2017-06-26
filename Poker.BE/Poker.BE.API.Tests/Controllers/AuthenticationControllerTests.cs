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
	public class AuthenticationControllerTests
	{
		#region Setup
		private AuthenticationController ctrl;

		public TestContext TestContext { get; set; }

		[TestInitialize]
		public void Before()
		{
			ctrl = new AuthenticationController()
			{
				Request = new HttpRequestMessage(),
				Configuration = new System.Web.Http.HttpConfiguration()
			};
		}

		[TestCleanup]
		public void After()
		{
			((AuthenticationService)ctrl.Service).Clear();
			ctrl = null;
		}
		#endregion

		[TestMethod()]
		public void LoginTest()
		{
			//Arrange
			var pw = "123456";
			var username = "johnny";
			var deposit = 100.0;
			ctrl.SignUp(new SignUpRequest()
			{
				Password = pw,
				UserName = username,
				Deposit = deposit
			});

			var request = new LoginRequest()
			{
				Password = pw,
				UserName = username
			};

			var exStatus = HttpStatusCode.OK;
			var exResult = new LoginResult()
			{
				ErrorMessage = "",
				Success = true,
				UserName = "1"
			};

			//Act
			var act = ctrl.Login(request);
			LoginResult actContent;
			var hasContent = act.TryGetContentValue(out actContent);

			//Assert
			Assert.AreEqual(exStatus, act.StatusCode, "status code");
			Assert.IsTrue(hasContent, "has content");
			Assert.AreEqual(exResult.ErrorMessage, actContent.ErrorMessage, "error message");
			Assert.AreEqual(exResult.Success, actContent.Success, "success bool");
			Assert.AreNotEqual(default(int?), actContent.UserName, "user not default");
			Assert.IsNotNull(actContent.UserName, "user not null");
		}

		[TestMethod()]
		public void LogoutTest()
		{
			//Arrange
			SignUpResult signup;
			ctrl.SignUp(new SignUpRequest() { Deposit = 100.0, Password = "123456", UserName = "johnny" })
				.TryGetContentValue(out signup);

			LoginResult login;
			ctrl.Login(new LoginRequest() { Password = "123456", UserName = "johnny" })
				.TryGetContentValue(out login);

			LogoutRequest request = new LogoutRequest()
			{
				UserName = login.UserName
			};

			//Act
			var act = ctrl.Logout(request);
			LogoutResult actContent;

			//Assert
			Assert.IsTrue(act.TryGetContentValue(out actContent));
			Assert.AreEqual("", actContent.ErrorMessage, "error message");
			Assert.AreEqual(true, actContent.Success, "success");
			Assert.AreEqual(login.UserName, actContent.UserName, "username");
			Assert.AreEqual(true, actContent.Output, "output");
		}

		[TestMethod()]
		public void SignUpTest()
		{
			//Arrange
			SignUpRequest request = new SignUpRequest()
			{
				Deposit = 100,
				Password = "123456",
				UserName = "johnny"
			};

			//Act
			var act = ctrl.SignUp(request);
			var actValue = default(SignUpResult);
			var actHasContent = act.TryGetContentValue(out actValue);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, act.StatusCode);
			Assert.IsTrue(actHasContent);
			Assert.AreEqual("", actValue.ErrorMessage);
			Assert.AreEqual(true, actValue.Success);
			Assert.IsNotNull(actValue.UserName);
		}

        [TestMethod]
        public void SignUpTest2()
        {
            //Arrange
            SignUpRequest request = new SignUpRequest()
            {
                UserName = "idan",
                Password = "123456",
                Deposit = 20.5,
            };

            //Act
            var act = ctrl.SignUp(request);
            var actValue = default(SignUpResult);
            var actHasContent = act.TryGetContentValue(out actValue);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode);
            Assert.IsTrue(actHasContent);
            Assert.AreEqual("", actValue.ErrorMessage);
            Assert.AreEqual(true, actValue.Success);
            Assert.IsNotNull(actValue.UserName);
        }
	}
}