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
		private int TestUserID;
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUserID = base.SignUp("Gal", "galwa", "12345");
		}
		[Test]
		public void TestNewUserSignUp()
		{
			Assert.AreNotEqual(TestUserID, -1);
		}
		[Test]
		public void TestExsistingUserSignUp()
		{
			try
			{
				int TestUserID = base.SignUp("Gal", "galwa", "12345");
				Assert.Fail();
			}
			catch(ArgumentException e)
			{
				Assert.Pass();
			}
		}
	}
}
