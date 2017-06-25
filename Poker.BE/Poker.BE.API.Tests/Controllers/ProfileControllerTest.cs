using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.API.Controllers;
using Poker.BE.Domain.Security;
using Poker.BE.Domain.Core;
using Poker.BE.Service.Services;
using System.Net;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System.Net.Http;

namespace Poker.BE.API.Tests.Controllers
{
	[TestClass]
	public class ProfileControllerTest
	{
		#region Setup
		private ProfileController ctrl;
		private UserManager userManager;
		private User user;

		public TestContext TestContext { get; set; }

		[TestInitialize]
		public void Before()
		{
			userManager = UserManager.Instance;
			user = new User();
			userManager.Users.Add(user.UserName, user);

			ctrl = new ProfileController()
			{
				Request = new System.Net.Http.HttpRequestMessage(),
				Configuration = new System.Web.Http.HttpConfiguration()
			};
		}

		[TestCleanup]
		public void After()
		{
			((ProfileService)ctrl.Service).Clear();
			ctrl = null;
		}
		#endregion
		[TestMethod]
		public void EditProfileTest()
		{
			//Arrange
			var request = new EditProfileRequest()
			{
				oldUserName = user.UserName,
				newUserName = "GAL",
				newPassword = "Password2",
				newAvatar = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
			};

			var exStatus = HttpStatusCode.OK;
			var exResult = new EditProfileResult()
			{
				ErrorMessage = "",
				Success = true,
				newUserName = "GAL",
				newAvatar = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
				newPassword = "Password2"
			};

			//Act
			var act = ctrl.EditProfile(request);
			EditProfileResult actContent;
			var hasContent = act.TryGetContentValue(out actContent);

			//Assert
			Assert.AreEqual(exStatus, act.StatusCode, "status code");
			Assert.IsTrue(hasContent, "has content");
			Assert.AreEqual(exResult.ErrorMessage, actContent.ErrorMessage, "error message");
			Assert.AreEqual(exResult.Success, actContent.Success, "success bool");
			Assert.AreEqual(exResult.newUserName, actContent.newUserName, "user not default");
			Assert.AreEqual(exResult.newPassword, actContent.newPassword, "user not default");
			CollectionAssert.AreEquivalent(exResult.newAvatar, actContent.newAvatar, "user not default");
		}

		[TestMethod]
		public void GetProfileTest()
		{
			//Arrange
			var request = new GetProfileRequest()
			{
				UserName = user.UserName
			};

			var exStatus = HttpStatusCode.OK;
			var exResult = new GetProfileResult()
			{
				ErrorMessage = "",
				Success = true,
				UserName = user.UserName,
				Avatar = null,
				Password=user.Password
			};

			//Act
			var act = ctrl.GetProfile(request);
			GetProfileResult actContent;
			var hasContent = act.TryGetContentValue(out actContent);

			//Assert
			Assert.AreEqual(exStatus, act.StatusCode, "status code");
			Assert.IsTrue(hasContent, "has content");
			Assert.AreEqual(exResult.ErrorMessage, actContent.ErrorMessage, "error message");
			Assert.AreEqual(exResult.Success, actContent.Success, "success bool");
			Assert.AreEqual(exResult.UserName, actContent.UserName, "user not default");
			Assert.AreEqual(exResult.Password, actContent.Password, "user not default");
			Assert.AreEqual(exResult.Avatar, actContent.Avatar, "user not default");
		}
	}
}
