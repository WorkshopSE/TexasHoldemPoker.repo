using System;
using System.Collections.Generic;
using Poker.BE.Domain.Game;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Tests.Game
{
	class TestableDeck : Deck
	{
		public List<Card> ExposeCardDeck()
		{
			return this.DeckOfCards;
		}
		public List<Card> TestableShuffleCards()
		{
			ShuffleCards();
			return this.DeckOfCards;
		}
	}
}
