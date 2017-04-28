using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Tests.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
	/// <summary>Card Test</summary >
	/// <remarks>
	/// <author>Gal Wainer</author>
	/// <lastModified>2017-04-27</lastModified>
	/// </remarks>
	[TestClass()]
	public class CardTests
	{
		[TestMethod()]
		public void CardTest()
		{
			TestableCard TestCard = new TestableCard(Suit.Clubs, Value.Ace);
			Assert.AreEqual(Suit.Clubs, TestCard.ExposeCardSuit());
			Assert.AreEqual(Value.Ace, TestCard.ExposeCardValue());
		}

		[TestMethod()]
		public void EnumerateCardTest()
		{
			TestableCard TestCard = new TestableCard(Suit.Diamonds, Value.Eight);
			Assert.AreEqual(0, TestCard.ExposeShuffledIndex());
			TestCard.EnumerateCard();
			Assert.AreNotEqual(0, TestCard.ExposeShuffledIndex());
		}

		[TestMethod()]
		public void CompareToTest()
		{
			TestableCard TestCard1 = new TestableCard(Suit.Diamonds, Value.Eight);
			TestableCard TestCard2 = new TestableCard(Suit.Clubs, Value.Ace);
			TestableCard TestCard3 = new TestableCard(Suit.Hearts, Value.Jack);
			TestCard1.SetEnumerable(1);
			TestCard2.SetEnumerable(1);
			TestCard3.SetEnumerable(2);
			Assert.AreEqual(0,TestCard1.CompareTo(TestCard2));
			Assert.AreEqual(-1, TestCard1.CompareTo(TestCard3));
			Assert.AreEqual(1, TestCard3.CompareTo(TestCard1));
		}
	}
}