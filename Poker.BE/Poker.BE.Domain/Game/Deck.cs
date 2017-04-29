using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
	/// <summary>Created Deck of Card and shuffle cards UC</summary>
	/// <remarks>
	/// <author>Gal Wainer</author>
	/// <lastModified>2017-04-26</lastModified>
	/// </remarks>
	public class Deck
    {
		#region Properties
		public List<Card> DeckOfCards { get; }
		#endregion
		// TODO: complete - set team member to do this
		#region Methods
		public Deck()
		{
			DeckOfCards = new List<Card>();
			foreach (Value value in Enum.GetValues(typeof(Value)))
				foreach (Suit suit in Enum.GetValues(typeof(Suit)))
					DeckOfCards.Add(new Card(suit, value));

		}
		public void ShuffleCards()
		{
			foreach (Card UnshuffledCard in DeckOfCards)
				UnshuffledCard.EnumerateCard();
			DeckOfCards.Sort();
		}
		#endregion
	}
}
