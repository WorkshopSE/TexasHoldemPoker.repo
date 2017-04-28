using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Tests.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
	[TestClass()]
	public class DeckTests
	{
		[TestMethod()]
		public void DeckTest()
		{
			TestableDeck TestDeck = new TestableDeck();
			Assert.AreEqual(52, TestDeck.ExposeCardDeck().Count);
		}

		[TestMethod()]
		public void ShuffleCardsTest()
		{
			TestableDeck TestDeck = new TestableDeck();
			for (int i = 1; i < 52; i++)
			{
				Assert.AreNotEqual(1, TestDeck.ExposeCardDeck()[i - 1].CompareTo(TestDeck.ExposeCardDeck()[i]));
			}
		}
	}
}