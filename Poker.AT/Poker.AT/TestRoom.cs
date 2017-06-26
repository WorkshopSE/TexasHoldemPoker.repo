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
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			base.SignUp("tomer", "Tomer123", "123456");
			base.Login("Tomer123", "123456",out int key);
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
			int securityKey = 1;

			//act
			int player;
			int actresult = base.CreateARoom(level, "Tomer123", securityKey, out player);

			//Assert
			Assert.AreNotEqual(0,actresult);
			Assert.AreNotEqual(0, player);
		}
	}
}
