﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.API.Controllers;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
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
                User = 1
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
            Assert.AreNotEqual(default(int?), actContent.User, "user not default");
            Assert.IsNotNull(actContent.User, "user not null");
        }

        [TestMethod()]
        public void LogoutTest()
        {
            // TODO
            throw new NotImplementedException();
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
            Assert.IsNotNull(actValue.User);
        }
    }
}