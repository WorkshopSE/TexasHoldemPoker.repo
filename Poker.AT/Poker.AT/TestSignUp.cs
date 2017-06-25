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
		private string TestUser;
		private string SecondTestUser;
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUser = base.SignUp("Gal", "galwa", "123456");
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
			Assert.AreEqual("galwa", TestUser);
		}
		[Test]
		public void TestExsistingUserSignUp()
		{
			Assert.IsNull(base.SignUp("Gal", "galwa", "123456"));
		}
		[Test]
		public void TestUniqeID()
		{
			Assert.AreNotEqual(TestUser, SecondTestUser);
		}
	}
}
