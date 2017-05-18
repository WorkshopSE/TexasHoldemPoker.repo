using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    /// <summary> Defined the hand that the active players are playing poker at the room </summary>
    /// <remarks>
    /// <author>Idan Izicovich</author>
    /// <lastModified>2017-04-25</lastModified>
    /// </remarks>
    public class Hand
    {

        #region Constants
        public static readonly int MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START;
        #endregion

        #region Fields
        private Deck deck;
        private ICollection<Player> activePlayers;
        private Pot pot;
        #endregion

        #region Properties
		public bool handEnded { get; set;}
        #endregion

        #region Constructors
        public Hand(Deck deck, ICollection<Player> players)
        {
            if(players.Count < MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new Utility.Exceptions.NotEnoughPlayersException();
            }
            this.deck = deck;
            this.activePlayers = players;
            this.pot = new Pot();
			handEnded = false;
        }

        public Round CurrentRound { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Each player receive 2 face-down cards
        /// </summary>
        public void DealCards()
        {
            foreach (Player player in activePlayers)
            {
                for (int i = 0; i < player.PrivateCards.Length; i++)
                    player.PrivateCards[i] = deck.PullCard();
            }
        }

        public void PlaceBlinds()
        {
            PlaceSmallBlind();
            PlaceBigBlind();
            PlaceAnts();
        }

        /// <summary>
        /// If needed:
        /// All of the active players are forced to pay some blind
        /// payment to the pot, regardless to the regular blinds.
        /// </summary>
        private void PlaceAnts()
        {
            //TODO
            throw new NotImplementedException();
        }

        private void PlaceBigBlind()
        {
            //TODO
            throw new NotImplementedException();
        }

        private void PlaceSmallBlind()
        {
            //TODO
            throw new NotImplementedException();
        }
        #endregion
    }
}
