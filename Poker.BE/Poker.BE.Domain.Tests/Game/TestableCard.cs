using System;
using Poker.BE.Domain.Game;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Tests.Game
{
	class TestableCard : Card
	{
		public TestableCard(Suit suit, Value num) : base(suit, num)
		{
		}
		public void SetEnumerable(int num)
		{
			this.ShuffledIndex = num;
		}
		public Suit ExposeCardSuit()
		{
			return this.CardSuit;
		}
		public Value ExposeCardValue()
		{
			return this.CardNumber;
		}
		public int ExposeShuffledIndex()
		{
			return this.ShuffledIndex;
		}
	}
}
