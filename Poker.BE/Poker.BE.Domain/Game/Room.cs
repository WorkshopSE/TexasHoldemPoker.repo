using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{

    /// <summary> Defined the room that the players are playing at the game </summary>
    /// <remarks>
    /// <author>Idan Izicovich</author>
    /// <lastModified>2017-04-25</lastModified>
    /// </remarks>
    public class Room
    {
        #region Fields
        private ICollection<Player> activeAndPassivePalyers;
        private Deck deck;
        #endregion

        #region Properties
        public ICollection<Chair> Chairs { get; }
        public Hand CurrentHand { get; private set; }
        public GamePreferences Preferences { get; set; }
        #endregion

        #region Constructors
        private Room()
        {
            activeAndPassivePalyers = new List<Player>();
            deck = new Deck();
            Chairs = new Chair[Chair.NCHAIRS_IN_ROOM];

            for(int i = 0; i < Chair.NCHAIRS_IN_ROOM; i++)
            {
                Chairs.ToArray()[i] = new Chair(i);
            }

            CurrentHand = null;

            // Note: making default preferences to the room (poker game)
            Preferences = new GamePreferences();

        }

        public Room(Player creator): this()
        {
            activeAndPassivePalyers.Add(creator);
        }

        public Room(Player creator, GamePreferences preferences): this(creator)
        {
            Preferences = preferences;
        }

        #endregion

        #region Methods
        public void StartNewHand()
        {
            CurrentHand = new Hand(deck);
        }


        public void SendMessage()
        {
            //TODO
        }
        #endregion


    }//class
}
