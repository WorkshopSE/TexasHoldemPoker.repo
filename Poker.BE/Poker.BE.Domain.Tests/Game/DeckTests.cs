using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
	[TestClass]
	public class DeckTests
	{
		[TestMethod]
		public void DeckTest()
		{
			Deck TestDeck = new Deck();
			Assert.IsNotNull(TestDeck);
		}

		[TestMethod]
		public void ShuffleCardsTest()
		{
			Deck TestDeck = new Deck();
			for (int i = 1; i < 52; i++)
			{
				Assert.AreNotEqual(1, TestDeck.DeckOfCards[i - 1].CompareTo(TestDeck.DeckOfCards[i]));
			}
		}
	}
}