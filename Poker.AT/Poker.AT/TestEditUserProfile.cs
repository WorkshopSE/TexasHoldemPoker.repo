using AT.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace AT.Tests
{
	[TestFixture]
	[Category("UCC01: Authentication & Profile")]
	class TestEditUserProfile : ProjectTests
	{
		#region Fields
		string TestUser;
		string TestUser2;
		int TestUserKey;
		int TestUser2Key;
		#endregion

		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUser=base.SignUp("asaf", "Asaf", "123456");
			TestUser2 = base.SignUp("asaf", "Asafbil", "123457");
			base.Login("Asaf", "123456",out TestUserKey);
			base.Login("Asafbil", "123457", out TestUser2Key);
		}
		[Test]
		public void TestGetProfile()
		{
			//Arrange
			string password;
			int[] avatar;

			//Act
			string actResult = base.GetProfile(TestUser, TestUserKey, out password, out avatar);

			//Assert
			Assert.AreEqual(TestUser, actResult);
			Assert.IsNotNull(password);
		}
		[Test]
		public void TestValidPasswordChange()
		{
			//Arrange
			string oldPassword = "123456";
			string password = "123457";

			//Act
			bool actResult=base.EditProfilePassword(TestUser, oldPassword, password, TestUserKey);

			//Assert
			Assert.IsTrue(actResult);
		}
		[Test]
		public void TestInValidPasswordChange()
		{
			/*string TestPassword = TestUser.Password;
			try
			{
				base.EditProfilePassword(TestUser, Int64.MinValue.ToString());
				Assert.Fail();
			}
			catch(ArgumentException e)
			{
				Assert.AreEqual(TestPassword, TestUser.Password);
			}*/
		}
		[Test]
		public void TestValidEmailChange()
		{
			//base.EditProfileEmail(TestUser, "galwa@post.bgu.ac.il");
			//Assert.AreEqual("galwa@post.bgu.ac.il", TestUser.Email);
		}

		[Test]
		public void TestUnloggedUser()
		{
			//base.Logout("Asaf", "12345");
			//string TestEmail = TestUser.Email;
			try
			{
				//base.EditProfileEmail(TestUser, "galw@apost.bgu.ac.il");
				Assert.Fail();
			}
			catch (Exception e)
			{
				//Assert.AreEqual(TestEmail, TestUser.Email);
			}
			//string TestPassword = TestUser.Password;
			try
			{
				//base.EditProfilePassword(TestUser, "12673");
				Assert.Fail();
			}
			catch (Exception e)
			{
				//Assert.AreEqual(TestPassword, TestUser.Password);
			}
		}
	}
}
