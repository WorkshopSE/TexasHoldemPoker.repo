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
		string TestUser2;
		int TestUser2Key;
		int TestUserKey;
		#endregion

		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestUser = base.SignUp("tomer", "Tomer123", "123456");
			TestUser2 = base.SignUp("asaf", "Asafbil", "123457");
			base.Login("Asafbil", "123457", out TestUser2Key);
			base.Login("Tomer123", "123456", out TestUserKey);
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
			int? player;
			int actResult = base.CreateARoom(level, TestUser, securityKey, out player);

			//Assert
			Assert.AreNotEqual(0, actResult);
			Assert.AreNotEqual(0, player);
		}
		public void EnterARoomTest()
		{
			//Arrange
			int level = 4;
			int securityKey = TestUserKey;
			int? player;
			int room = base.CreateARoom(level, TestUser, securityKey, out player);

			//act
			int actResult = base.EnterRoom(room, TestUser2, TestUser2Key, out player);

			//Assert
			Assert.AreNotEqual(0, actResult);
			Assert.AreNotEqual(0, player);
		}

		[Test]
		public void JoinNextHandTest()
		{
			//Arrange
			int level = 4;
			int securityKey = TestUserKey;
			int? player;
			double wallet;
			int room = base.CreateARoom(level, TestUser, securityKey, out player);

			//Act
			double actResult = base.JoinNextHand(TestUser, securityKey, player, 1, 100, out wallet);
			
			//Assert
			Assert.AreNotEqual(0, actResult);
			Assert.AreNotEqual(0, wallet);
		}

		[Test]
		public void StandUpToSpectateTest()
		{
			//Arrange
			int level = 4;
			int securityKey = TestUserKey;
			int? player;
			double wallet;
			int room = base.CreateARoom(level, TestUser, securityKey, out player);
			base.JoinNextHand(TestUser, securityKey, player, 1, 100, out wallet);

			//Act
			bool actResult = base.StandUpToSpectate(TestUser, securityKey, player);

			//Assert
			Assert.AreNotEqual(0, actResult);
			Assert.AreNotEqual(0, wallet);
		}
	}
}
