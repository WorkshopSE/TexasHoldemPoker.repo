﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using Poker.BE.Domain.Utility;

namespace Poker.BE.API.Controllers.Tests
{
    /* Alias */
    using FRBCReq = FindRoomsByCriteriaRequest;

    [TestClass()]
    public class RoomControllerTests
    {

        #region Setup
        private RoomController _ctrl;
        private UserManager _userManager;
        private GameCenter _gameCenter;
        private User _user;
        private int _level;
        private NoLimitHoldem _config;
        private int _securityKey;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {
            _level = 20;
            _userManager = UserManager.Instance;
            _gameCenter = GameCenter.Instance;
            _user = new User();
            _userManager.Users.Add(_user.UserName, _user);
            _securityKey = 1;
            _user.Connect(_securityKey);
            _ctrl = new RoomController()
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            _config = new NoLimitHoldem();
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
                UserName = _user.UserName,
                SecurityKey = _securityKey
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

        [TestMethod()]
        public void EnterRoomTest()
        {
            //Arrange
            Player creator;
            var room = _gameCenter.CreateNewRoom(_level, new NoLimitHoldem(), out creator);

            EnterRoomRequest request = new EnterRoomRequest()
            {
                Room = room.GetHashCode(),
                UserName = _user.UserName,
                SecurityKey = _securityKey,
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


            TestContext.WriteLine("\nContent:");
            foreach (var property in actContent.GetType().GetProperties())
            {
                TestContext.WriteLine("{0} : {1}", property.Name, property.GetValue(actContent));
            }
        }

        [TestMethod()]
        public void JoinNextHandTest()
        {
            //Arrange
            var room = default(Room);
            var player = default(Player);

            room = _user.CreateNewRoom(_level, _config, out player);

            JoinNextHandRequest request = new JoinNextHandRequest()
            {
                BuyIn = room.Preferences.BuyInCost,
                Player = player.GetHashCode(),
                SeatIndex = 1,
                UserName = _user.UserName,
                SecurityKey = _securityKey,
            };

            //Act
            var act = _ctrl.JoinNextHand(request);
            JoinNextHandResult actContent;
            var hasContent = act.TryGetContentValue(out actContent);

            //Assert
            TestContext.WriteLine("error message: '{0}'", (actContent != null && actContent.ErrorMessage != "") ? actContent.ErrorMessage : "null");
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode, "status code");
            Assert.IsTrue(hasContent, "has contact");
            Assert.AreEqual("", actContent.ErrorMessage, "error message");
            Assert.AreEqual(true, actContent.Success, "success");
        }

        [TestMethod()]
        public void StandUpToSpactateTest()
        {
            //Arrange
            Player player;
            var room = _user.CreateNewRoom(_level, _config, out player);
            double buyIn = room.Preferences.BuyInCost;
            int seatIndex = 0;
            _user.JoinNextHand(player, seatIndex, buyIn);
            player.Fold();
            StandUpToSpactateRequest request = new StandUpToSpactateRequest()
            {
                Player = player.GetHashCode(),
                UserName = _user.UserName,
                SecurityKey = _securityKey,
            };

            //Act
            var act = _ctrl.StandUpToSpactate(request);
            StandUpToSpactateResult actContent;
            var hasContent = act.TryGetContentValue(out actContent);

            //Assert
            TestContext.WriteLine("error message: '{0}'", (actContent != null && actContent.ErrorMessage != "") ? actContent.ErrorMessage : "null");
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode, "status code");
            Assert.IsTrue(hasContent, "has contact");
            Assert.AreEqual("", actContent.ErrorMessage, "error message");
            Assert.AreEqual(true, actContent.Success, "success");
            Assert.AreEqual(buyIn, actContent.RemainingMoney, "remaining money");
            Assert.AreEqual(_user.UserBank.Money, actContent.UserBankMoney, "user bank money");
            Assert.AreEqual(_user.UserStatistics, actContent.UserStatistics, "user statistics object");
        }

        [TestMethod()]
        public void LeaveRoomTest()
        {
            //Arrange
            var player = default(Player);
            var room = _user.CreateNewRoom(_level, _config, out player);
            LeaveRoomRequest request = new LeaveRoomRequest()
            {
                Player = player.GetHashCode(),
                UserName = _user.UserName,
                SecurityKey = _securityKey,
            };

            //Act
            var act = _ctrl.LeaveRoom(request);
            var actContent = default(LeaveRoomResult);
            var hasContent = act.TryGetContentValue(out actContent);

            //Assert
            TestContext.WriteLine("error message: '{0}'", (actContent != null && actContent.ErrorMessage != "") ? actContent.ErrorMessage : "null");
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode, "status code");
            Assert.IsTrue(hasContent, "has contact");
            Assert.AreEqual("", actContent.ErrorMessage, "error message");
            Assert.AreEqual(true, actContent.Success, "success");
            Assert.AreEqual(false, _user.Players.Contains(player, new AddressComparer<Player>()));
            Assert.IsNotNull(actContent.UserStatistics, "user statistics not null");
        }

        [TestMethod()]
        public void GetAllRoomsTest()
        {
            //Arrange
            Player creator;
            _user.CreateNewRoom(_level, new NoLimitHoldem() { Name = "room 1" }, out creator);
            _user.CreateNewRoom(_level, new NoLimitHoldem() { Name = "room 2" }, out creator);
            _user.CreateNewRoom(_level, new NoLimitHoldem() { Name = "room 3" }, out creator);
            int roomCount = 3;

            //Act
            var act = _ctrl.GetAllRooms();
            var actContent = default(FindRoomsByCriteriaResult);
            var hasContent = act.TryGetContentValue(out actContent);

            //Assert
            TestContext.WriteLine("error message: '{0}'", (actContent != null && actContent.ErrorMessage != "") ? actContent.ErrorMessage : "null");
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode, "status code");
            Assert.IsTrue(hasContent, "has contact");
            Assert.AreEqual("", actContent.ErrorMessage, "error message");
            Assert.AreEqual(true, actContent.Success, "success");

            // print the result rooms
            TestContext.WriteLine("rooms:");
            foreach (var room in actContent.Rooms)
            {
                foreach (var property in room.GetType().GetProperties())
                {

                    TestContext.WriteLine("{0} : {1}", property.Name, property.GetValue(room));
                }

                TestContext.WriteLine("");
            }

            Assert.AreEqual(roomCount, actContent.Rooms.Count(), "room count");
        }

        [TestMethod()]
        public void FindRoomsByCriteriaTest()
        {
            //Arrange
            Player player = null;
            Room room = _user.CreateNewRoom(_level, _config, out player);
            var roomCount = 1;


            FRBCReq request = new FRBCReq()
            {
                BetSize = room.Preferences.BuyInCost,
                Criterias = new string[] { FRBCReq.BET_SIZE, FRBCReq.LEVEL },
                Level = _level,
                Player = player.GetHashCode(),
                CurrentNumberOfPlayers = room.Players.Count,
                MaxNumberOfPlayers = room.Preferences.MaxNumberOfPlayers,
                MinimumBuyIn = room.Preferences.BuyInCost,
            };

            //Act
            var act = _ctrl.FindRoomsByCriteria(request);
            var actContent = default(FindRoomsByCriteriaResult);
            var hasContent = act.TryGetContentValue(out actContent);

            //Assert
            TestContext.WriteLine("error message: '{0}'", (actContent != null && actContent.ErrorMessage != "") ? actContent.ErrorMessage : "null");
            Assert.AreEqual(HttpStatusCode.OK, act.StatusCode, "status code");
            Assert.IsTrue(hasContent, "has contact");
            Assert.AreEqual("", actContent.ErrorMessage, "error message");
            Assert.AreEqual(true, actContent.Success, "success");

            // print the result rooms
            TestContext.WriteLine("rooms:");
            foreach (var roomRes in actContent.Rooms)
            {
                foreach (var property in roomRes.GetType().GetProperties())
                {

                    TestContext.WriteLine("{0} : {1}", property.Name, property.GetValue(roomRes));
                }

                TestContext.WriteLine("");
            }

            Assert.AreEqual(roomCount, actContent.Rooms.Count(), "room count");
        }

        //[TestMethod()]
        //public void GetAllRoomsOfLeagueTest()
        //{
        //    // TODO - for Gal
        //    throw new NotImplementedException();
        //}
    }
}