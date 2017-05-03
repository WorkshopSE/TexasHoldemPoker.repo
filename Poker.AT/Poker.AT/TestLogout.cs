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
			base.SignUp("tomer", "Tomer123", "12345");
			base.Login("Tomer123", "12345");
		}
		[Test]
		public void TestSuccesfulLogout()
		{
			Assert.IsTrue(base.Logout("Tomer123", "12345"));
		}
	}
}
