using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Tests
{
	[TestFixture]
	[Category("Login")]
	class TestLogout : ProjectTests
	{
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			base.SignUp("tomer", "Tomer123", "123456");
			base.Login("Tomer123", "123456");
		}

		[TearDown]
		public void After()
		{
			base.TearDown();
		}
		[Test]
		public void TestSuccesfulLogout()
		{
			Assert.IsTrue(base.Logout("Tomer123", "123456"));
		}
	}
}
