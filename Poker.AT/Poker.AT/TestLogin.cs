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
			try
			{
				base.Login("Tomer12", "12345");
				Assert.Fail();
			}catch(ArgumentException e)
			{
				Assert.Pass();
			}
			
		}
		[Test]
		public void TestIncorrectPassword()
		{
			try
			{
				base.Login("Tomer123", "125");
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Assert.Pass();
			}

		}

	}
}
