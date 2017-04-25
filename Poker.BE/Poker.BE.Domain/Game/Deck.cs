using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Deck
    {
		#region Properties
		Dictionary<int, Card> DeckOfCards;
		#endregion
		// TODO: complete - set team member to do this
		#region Methods
		public Deck()
		{
			foreach (Value value in Enum.GetValues(typeof(Value)))
				foreach (Suit suit in Enum.GetValues(typeof(Suit)))
					DeckOfCards.Add(1, new Card(suit, value));

		}
		protected void ShuffleCards()
		{

		}
		#endregion
	}
}
