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
		string TestUser;
		
		Image TestUserImage;
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUser=base.SignUp("asaf", "Asaf", "12345");
			base.Login("Asaf", "12345");
			TestUserImage = Image.FromFile("donaldtramp");
			
		}
		[Test]
		public void TestValidPasswordChange()
		{
			//base.EditProfilePassword(TestUser, "12894");
			//Assert.AreEqual("12894", TestUser.Password);
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
		public void TestAvatarChange()
		{
			MemoryStream ms = new MemoryStream();
			TestUserImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
			string Expected = Convert.ToBase64String(ms.ToArray());
			base.EditProfileAvatar(TestUserImage).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
			string Real = Convert.ToBase64String(ms.ToArray());
			Assert.AreEqual(Expected, Real);
		}
		[Test]
		public void TestUnloggedUser()
		{
			base.Logout("Asaf", "12345");
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
