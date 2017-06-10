using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.API.Controllers;
using Poker.BE.Service.Services;
using Poker.BE.Service.Modules.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Results;
using System.Net.Http;
using System.Net;
using FakeItEasy;
using Poker.BE.Service.IServices;
using Poker.BE.Domain.Security;
using Poker.BE.Domain.Core;

namespace Poker.BE.API.Controllers.Tests
{
	[TestClass()]
	public class RoomControllerTests
	{

		#region Setup
		private RoomController ctrl;
		private UserManager userManager;
		private User user;

		public TestContext TestContext { get; set; }

		[TestInitialize]
		public void Before()
		{
			userManager = UserManager.Instance;
			user = new User();
			userManager.Users.Add(user.UserName, user);
			ctrl = new RoomController()
			{
				Request = new System.Net.Http.HttpRequestMessage(),
				Configuration = new System.Web.Http.HttpConfiguration()
			};
		}

		[TestCleanup]
		public void After()
		{
			((RoomsService)ctrl.Service).Clear();
			ctrl = null;
			userManager.Users.Remove(user.UserName);
		}
		#endregion

		[TestMethod()]
		public void CreateRoomTest()
		{

			//Arrange
			int lvl = 20;
			
			CreateNewRoomRequest request = new CreateNewRoomRequest()
			{
				Level = lvl,
				User = user.UserName

			};

			var exStatus = HttpStatusCode.OK;
			var exResult = new CreateNewRoomResult()
			{
				ErrorMessage = "",
				Success = true,
				Room = 0,
				Player = 0
			};

			//Act
			var act = ctrl.CreateNewRoom(request);
			CreateNewRoomResult actContent;
			var hasContent = act.TryGetContentValue(out actContent);

			//Assert
			Assert.AreEqual(exStatus, act.StatusCode, "status code");
			Assert.IsTrue(hasContent, "has content");
			Assert.AreEqual(exResult.ErrorMessage, actContent.ErrorMessage, "error message");
			Assert.AreEqual(exResult.Success, actContent.Success, "success bool");
			Assert.AreNotEqual(default(int?), actContent.Room, "room not default");
			Assert.AreNotEqual(default(int?), actContent.Player, "player not default");
			Assert.IsNotNull(actContent.Room, "room not null");
			Assert.IsNotNull(actContent.Player, "player not null");
			
		}
		// UNDONE: @gwainer - gal, please continue my work from here
		//[TestMethod()]
		//public void EnterRoomTest()
		//{
		//    // TODO
		//    throw new NotImplementedException();
		//}
	}
}