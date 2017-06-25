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
	[Category("UCC01: Authentication & Profile")]
	class TestLogin : ProjectTests
	{
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			base.SignUp("tomer", "Tomer123", "123456");
		}

		[TearDown]
		public void After()
		{
			base.TearDown();
		}

		[Test]
		public void TestSuccesfulLogin()
		{
			Assert.IsTrue(base.Login("Tomer123", "123456"));
		}
		[Test]
		public void TestIncorrectUserName()
		{
			Assert.IsFalse(base.Login("Tomer12", "123456"));
		}
		[Test]
		public void TestIvalidPassword()
		{
			Assert.IsFalse(base.Login("Tomer123", "182759"));
		}

	}
}
