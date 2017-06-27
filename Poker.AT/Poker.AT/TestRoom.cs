using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Tests
{
	[TestFixture]
	[Category("UCC03: Rooms Management")]
	class TestRoom : ProjectTests
	{
		#region fields
		string TestUser;
		int TestUserKey;
		#endregion

		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUser=base.SignUp("tomer", "Tomer123", "123456");
			base.Login("Tomer123", "123456",out TestUserKey);
		}

		[TearDown]
		public void After()
		{
			base.TearDown();
		}

		[Test]
		public void CreateARoomTest()
		{
			//Arrange
			int level = 3;
			int securityKey = TestUserKey;

			//act
			int player;
			int actResult = base.CreateARoom(level, "Tomer123", securityKey, out player);

			//Assert
			Assert.AreNotEqual(0,actResult);
			Assert.AreNotEqual(0, player);
		}
	}
}
