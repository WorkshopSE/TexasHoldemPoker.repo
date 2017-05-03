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
			Deck TestDeck = new Deck();
			ShuffleCards(TestDeck);
		}
		[Test]
		public void TestShuffleCards()
		{
			CollectionAssert.AreNotEqual(ShuffleCards(TestDeck), ShuffleCards(TestDeck));
		}
	}
}
