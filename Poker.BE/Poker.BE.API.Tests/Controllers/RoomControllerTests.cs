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
using Poker.BE.Domain.Game;

namespace Poker.BE.API.Controllers.Tests
{
	[TestClass()]
	public class RoomControllerTests
	{

		#region Setup
		private RoomController ctrl;
		private UserManager userManager;
		private User user1;
		private User user2;

		public TestContext TestContext { get; set; }

		[TestInitialize]
		public void Before()
		{
			userManager = UserManager.Instance;
			user1 = new User();
			user2 = new User();
			userManager.Users.Add(user1.UserName, user1);
			userManager.Users.Add(user2.UserName, user2);
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
			userManager.Users.Remove(user1.UserName);
			userManager.Users.Remove(user2.UserName);
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
				User = user1.UserName

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
		[TestMethod()]
		public void EnterRoomSTest()
		{

			//Arrange
			int lvl = 25;
			CreateNewRoomResult roomResultContent;

			ctrl.CreateNewRoom(new CreateNewRoomRequest()
			{
				Level = lvl,
				User = user1.UserName
			}).TryGetContentValue(out roomResultContent);

			EnterRoomRequest request = new EnterRoomRequest()
			{
				Room = roomResultContent.Room,
				User = user2.UserName

			};

			var exStatus = HttpStatusCode.OK;
			var exResult = new EnterRoomResult()
			{
				ErrorMessage = "",
				Success = true,
				Player = 0
			};

			//Act
			var act = ctrl.EnterRoom(request);
			EnterRoomResult actContent;
			var hasContent = act.TryGetContentValue(out actContent);

			//Assert
			Assert.AreEqual(exStatus, act.StatusCode, "status code");
			Assert.IsTrue(hasContent, "has content");
			Assert.AreEqual(exResult.ErrorMessage, actContent.ErrorMessage, "error message");
			Assert.AreEqual(exResult.Success, actContent.Success, "success bool");
			Assert.AreNotEqual(default(int?), actContent.Player, "player not default");
			Assert.IsNotNull(actContent.Player, "player not null");

		}
		[TestMethod()]
		public void JoinNextHandTestSuccess()
		{

			//Arrange
			int lvl = 25;
			double defaultBuyIn = 100;
			CreateNewRoomResult roomResultContent;

			ctrl.CreateNewRoom(new CreateNewRoomRequest()
			{
				Level = lvl,
				User = user1.UserName
			}).TryGetContentValue(out roomResultContent);

			JoinNextHandRequest request = new JoinNextHandRequest()
			{
				Player = roomResultContent.Player,
				buyIn = defaultBuyIn,
				seatIndex = 2
			};

			var exStatus = HttpStatusCode.OK;
			var exResult = new JoinNextHandResult()
			{
				ErrorMessage = "",
				Success = true,
			};

			//Act
			var act = ctrl.JoinNextHand(request);
			JoinNextHandResult actContent;
			var hasContent = act.TryGetContentValue(out actContent);

			//Assert
			Assert.AreEqual(exStatus, act.StatusCode, "status code");
			Assert.IsTrue(hasContent, "has content");
			Assert.AreEqual(exResult.ErrorMessage, actContent.ErrorMessage, "error message");
			Assert.AreEqual(exResult.Success, actContent.Success, "success bool");

		}
		[TestMethod()]
		public void JoinNextHandTestFail()
		{

			//Arrange
			int lvl = 25;
			double defaultBuyIn = 100;
			CreateNewRoomResult roomResultContent;

			ctrl.CreateNewRoom(new CreateNewRoomRequest()
			{
				Level = lvl,
				User = user1.UserName
			}).TryGetContentValue(out roomResultContent);

			JoinNextHandRequest request1 = new JoinNextHandRequest()
			{
				Player = roomResultContent.Player,
				buyIn = defaultBuyIn,
				seatIndex = 15
			};


			var exStatus = HttpStatusCode.OK;
			var exResult = new JoinNextHandResult()
			{
				ErrorMessage = "",
				Success = false,
			};

			//Act
			var act1 = ctrl.JoinNextHand(request1);
			JoinNextHandResult actContent1;
			var hasContent1 = act1.TryGetContentValue(out actContent1);

			//Assert
			Assert.AreEqual(exStatus, act1.StatusCode, "status code");
			Assert.IsTrue(hasContent1, "has content");
			Assert.AreEqual(exResult.Success, actContent1.Success, "success bool");
		}
	}
}