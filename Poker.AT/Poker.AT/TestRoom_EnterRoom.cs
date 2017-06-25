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
	class TestRoom_EnterRoom : ProjectTests
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
		public
	}
}
