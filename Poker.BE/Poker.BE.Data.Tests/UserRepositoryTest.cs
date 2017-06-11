using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Core;
using Poker.BE.Data.Entities;

namespace Poker.BE.Data.Tests
{
    /// <summary>
    /// Data Tests integration for user repository
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {

        #region Setup
        private ICollection<User> Users;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Before()
        {

        }

        [TestCleanup]
        public void After()
        {

        }

        public UserRepositoryTest()
        {
            Users = new List<User>();
        }
        #endregion

        // TODO
        //[TestMethod]
        //public void AddingUserToDataBaseTest()
        //{
        //    //Arrange

        //    //Act
        //    var user = new User();
        //    UserEntity act = null;
        //    //using (var db = new Access(new MainContext()))
        //    //{
        //    //    act = db.UserRepository.SingleOrDefault(x => x == user.Entity);
        //    //}

        //    //Assert
        //    Assert.IsNotNull(act);

        //    TestContext.WriteLine("act " + act.ToString());



        //}
    }
}
