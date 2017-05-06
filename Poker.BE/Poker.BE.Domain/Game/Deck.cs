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
        #region Constants
        public const int NCARDS = checked(Card.NSUIT * Card.NVALUE);
        #endregion

        #region Fields
        private Card[] cards;
        #endregion

        #region Constructors
        public Deck()
        {
            cards = GetFullDeck();
        }
        #endregion

        #region Private Functions
        private Card[] GetFullDeck()
        {
            Card[] result = new Card[NCARDS];
            int index = 0;

            for (int i = 0; i < Card.NSUIT; i++)
            {
                for (int j = 0; j < Card.NVALUE; j++)
                {
                    result[index] = new Card(Card.Suit., Card.Value[j]);
                    index++;
                }
            }

            return result;
        }
        #endregion

        #region Methods
        public void ShuffleCards()
        {

        }

        public Card PullCard()
        {
            throw new NotImplementedException();
        }

        public ICollection<Card> PullCards(int v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
