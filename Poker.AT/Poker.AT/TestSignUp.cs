using NUnit.Framework;
using AT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Tests
{
	[TestFixture]
	[Category("SignUp")]
	class TestSignUp : ProjectTests
	{
		private User TestUser;
		private User SecondTestUser;
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUser = base.SignUp("Gal", "galwa", "12345");
			SecondTestUser = base.SignUp("idan", "idanizi", "12345");
		}
		[Test]
		public void TestNewUserSignUp()
		{
			Assert.AreNotEqual(TestUser.ID, 0);
		}
		[Test]
		public void TestExsistingUserSignUp()
		{
			try
			{
				TestUser = base.SignUp("Gal", "galwa", "12345");
				Assert.Fail();
			}
			catch(ArgumentException e)
			{
				Assert.Pass();
			}
		}
		[Test]
		public void TestUniqeID()
		{
			Assert.AreNotEqual(TestUser.ID, SecondTestUser.ID);
		}
	}
}
