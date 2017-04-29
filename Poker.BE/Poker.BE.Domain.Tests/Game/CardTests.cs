using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	[TestClass]
	public class CardTests
	{
		[TestMethod]
		public void CardTest()
		{
			Card TestCard = new Card(Suit.Clubs, Value.Ace);
			Assert.IsNotNull(TestCard);
		}

		[TestMethod]
		public void EnumerateCardTest()
		{
			Card TestCard = new Card(Suit.Diamonds, Value.Eight);
			Assert.AreEqual(0, TestCard.ShuffledIndex);
			TestCard.EnumerateCard();
			Assert.AreNotEqual(0, TestCard.ShuffledIndex);
		}

		[TestMethod]
		public void CompareToTest()
		{
/*			Card TestCard2 = new Card(Suit.Clubs, Value.Ace);
			Card TestCard3 = new Card(Suit.Hearts, Value.Jack);
			TestCard1.SetEnumerable(1);
			TestCard2.SetEnumerable(1);
			TestCard3.SetEnumerable(2);
			Assert.AreEqual(0,TestCard1.CompareTo(TestCard2));
			Assert.AreEqual(-1, TestCard1.CompareTo(TestCard3));
			Assert.AreEqual(1, TestCard3.CompareTo(TestCard1));*/
		}
	}
}