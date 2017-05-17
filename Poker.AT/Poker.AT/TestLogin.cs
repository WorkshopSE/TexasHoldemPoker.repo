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
	[Category("Login")]
	class TestLogin : ProjectTests
	{
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			base.SignUp("tomer", "Tomer123", "12345");
		}
		[Test]
		public void TestSuccesfulLogin()
		{
			Assert.IsTrue(base.Login("Tomer123", "12345"));
		}
		[Test]
		public void TestIncorrectUserName()
		{
			Assert.AreEqual(false, base.Login("Tomer12", "12345"));
		}
		[Test]
		public void TestIncorrectPassword()
		{
			Assert.AreEqual(false, base.Login("Tomer123", "125"));
		}

	}
}
