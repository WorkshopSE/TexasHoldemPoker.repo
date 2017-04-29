using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    /// <summary> Defined the hand that the active players are playing poker at the room </summary>
    /// 
	/// <remarks>
	/// <author>Gal Wainer</author>
    /// <lastModified>2017-04-25</lastModified>
    /// 
    /// comments by:
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
			foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
				foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
					DeckOfCards.Add(new Card(suit, value));

		}
		public void ShuffleCards()
		{
			foreach (Card UnshuffledCard in DeckOfCards)
				UnshuffledCard.EnumerateCard();
			DeckOfCards.Sort();
        }

        internal Card PullCard()
        {
            throw new NotImplementedException();
        }

        internal ICollection<Card> PullCards(int v)
        {
            throw new NotImplementedException();
		}
		#endregion
	}
}
