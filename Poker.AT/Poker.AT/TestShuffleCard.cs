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
	[Category("Shuffle Cards")]
	class TestShuffleCard : ProjectTests
	{
		private Deck TestDeck;
		[SetUp]
		public new void Setup()
		{
			base.Setup();
			TestDeck = new Deck();
		}
		[Test]
		public void TestShuffleCards()
		{
			Assert.IsTrue(base.ShuffleCards(TestDeck));
		}
	}
}
