using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Tests
{
	[TestFixture]
	[Category("UCC01: Authentication & Profile")]
	class TestAuthentication : ProjectTests
	{
		#region field
		private string TestUser;
		private string SecondTestUser;
		#endregion

		[SetUp]
		public new void Setup()
		{
			base.Setup();
			base.SignUp("tomer", "Tomer123", "123456");
			SecondTestUser = base.SignUp("idan", "idanizi", "12345");
		}

		[TearDown]
		public void After()
		{
			base.TearDown();
		}
		[Test]
		public void TestNewUserSignUp()
		{
			//Arrange
			string name = "Gal";
			string userName = "galwa";
			string password = "123456";

			//Act
			string actResult = base.SignUp(name, userName, password);

			//Assert
			Assert.AreEqual(userName, actResult);
		}
		[Test]
		public void TestExsistingUserSignUp()
		{
			//Arrange
			string name = "Gal";
			string userName = "gwainer";
			string password = "789456";
			base.SignUp(name, userName, password);

			//Act
			string actResult=base.SignUp(name, userName, password);

			//Assert
			Assert.IsNull(actResult);
		}
		[Test]
		public void TestUniqeID()
		{
			//Arrange
			string name = "idan";
			string userName = "idanizi";
			string password = "784456";
			string name2 = "idan";
			string userName2 = "idan";
			string password2 = "756456";

			//Act
			string actResult = base.SignUp(name, userName, password);
			string actResult2 = base.SignUp(name2, userName2, password2);

			//Assert
			Assert.AreNotEqual(actResult, actResult2);
		}

		[Test]
		public void TestSuccesfulLogin()
		{
			//Arrange
			string name = "Gal";
			string userName = "gwainer1";
			string password = "7894566";
			int securityKey;
			base.SignUp(name, userName, password);

			//Act
			bool actResult = base.Login(userName, password,out securityKey);

			//Assert
			Assert.IsTrue(actResult);
			Assert.AreNotEqual(0, securityKey);
		}
		[Test]
		public void TestLoginIncorrectUserName()
		{
			//Arrange
			string name = "asaf";
			string userName = "asaf";
			string wrongUserName = "asa";
			string password = "7894456";
			int securityKey;
			base.SignUp(name, userName, password);

			//Act
			bool actResult = base.Login(wrongUserName, password, out securityKey);

			//Assert
			Assert.IsFalse(actResult);
			Assert.AreEqual(0, securityKey);
		}
		[Test]
		public void TestLoginWrongPassword()
		{
			//Arrange
			string name = "yossi";
			string userName = "yossi";
			string password = "999999";
			string wrongPassword = "888888";
			int securityKey;
			base.SignUp(name, userName, password);

			//Act
			bool actResult = base.Login(userName, wrongPassword, out securityKey);

			//Assert
			Assert.IsFalse(actResult);
			Assert.AreEqual(0, securityKey);
		}
		
		public void TestSuccesfulLogout()
		{
			//Assert.IsTrue(base.Logout("Tomer123", ));
		}
	}
}
