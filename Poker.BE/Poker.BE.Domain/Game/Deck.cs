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
    /// <author>Idan Izicovich</author>
    /// 
    /// </remarks>
    public class Deck
    {
        //TODO: Gal Wainer - fixme, this is not working well (shuffle cards..)
        #region Properties
        List<Card> DeckOfCards; // not initiated in a constructor - calling to null! and you should use IList or ICollection instead as BP.
        #endregion

        #region Constructors
        //TODO: for Gal Wainer?...
        #endregion


        #region Methods
        public Deck()
        {
            //foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
            //    foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            //        DeckOfCards.Add(new Card(suit, value));
            //this.ShuffleCards();

        }
        protected void ShuffleCards()
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
