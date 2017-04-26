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
        #endregion

        #region Constructors
        public Hand(Deck deck, ICollection<Player> players)
        {
            this.deck = deck;
            this.activePlayers = players;
            this.pot = new Pot();
        }
        #endregion
    }
}
