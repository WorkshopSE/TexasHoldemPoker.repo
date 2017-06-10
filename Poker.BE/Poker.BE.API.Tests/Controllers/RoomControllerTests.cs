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

namespace Poker.BE.API.Controllers.Tests
{
	[TestClass()]
	public class RoomControllerTests
	{

		#region Setup
		private RoomController ctrl;
		private AuthenticationController Usersctrl;

		public TestContext TestContext { get; set; }

		[TestInitialize]
		public void Before()
		{
			ctrl = new RoomController()
			{
				Request = new System.Net.Http.HttpRequestMessage(),
				Configuration = new System.Web.Http.HttpConfiguration()
			};
			Usersctrl = new AuthenticationController()
			{
				Request = new HttpRequestMessage(),
				Configuration = new System.Web.Http.HttpConfiguration()
			};
		}

		[TestCleanup]
		public void After()
		{
			((RoomsService)ctrl.Service).Clear();
			ctrl = null;
			((AuthenticationService)Usersctrl.Service).Clear();
			Usersctrl = null;
		}
		#endregion

		[TestMethod()]
		public void CreateRoomTest()
		{

			//Arrange
			var pw = "678901";
			var username = "gal";
			var deposit = 90.0;
			int lvl = 20;

			SignUpRequest UserRequest = new SignUpRequest()
			{
				Deposit = deposit,
				Password = pw,
				UserName = username
			};

			var arrange = Usersctrl.SignUp(UserRequest);
			var arrangeValue = default(SignUpResult);
			var arrangeHasContent = arrange.TryGetContentValue(out arrangeValue);

			CreateNewRoomRequest request = new CreateNewRoomRequest()
			{
				Level = lvl,
				User = arrangeValue.User
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