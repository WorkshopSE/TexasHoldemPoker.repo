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
        private RoomController _ctrl;
        private UserManager _userManager;
        private GameCenter _gameCenter;
        private User _user;
        private int _level;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            _level = 20;
            _userManager = UserManager.Instance;
            _gameCenter = GameCenter.Instance;
            _user = new User();
            _userManager.Users.Add(_user.UserName, _user);
            _ctrl = new RoomController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
        }

        [TestCleanup]
        public void After()
        {
            ((RoomsService)_ctrl.Service).Clear();
            _ctrl = null;
        }
        #endregion

        [TestMethod()]
        public void CreateRoomTest()
        {

            //Arrange

            CreateNewRoomRequest request = new CreateNewRoomRequest()
            {
                Level = _level,
                User = _user.UserName

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
            var act = _ctrl.CreateNewRoom(request);
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

        //UNDONE: @idanizi - taking this from gal.
        [TestMethod()]
        public void EnterRoomTest()
        {
            //Arrange
            Player creator;
            var room = _gameCenter.CreateNewRoom(_level, new NoLimitHoldem(), out creator);

            EnterRoomRequest request = new EnterRoomRequest()
            {
                Room = room.GetHashCode(),
                User = _user.UserName
            };

            //Act
            var act = _ctrl.EnterRoom(request);
            EnterRoomResult actContent;
            var hasContent = act.TryGetContentValue(out actContent);

            //Assert
            TestContext.WriteLine("error message: '{0}'", (actContent != null && actContent.ErrorMessage != "") ? actContent.ErrorMessage : "null");
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode, "status code");
            Assert.IsTrue(hasContent, "has content");
            Assert.AreEqual("", actContent.ErrorMessage, "error message");
            Assert.AreEqual(true, actContent.Success, "success");
            Assert.AreNotEqual(default(int?), actContent.Player, "player not default value");
            Assert.IsNotNull(actContent.Player, "player not null");
        }

        [TestMethod()]
        public void JoinNextHandTest()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}