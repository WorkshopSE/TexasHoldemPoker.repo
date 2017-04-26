using Poker.BE.Domain.Utility.Exceptions;
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
        private ICollection<Player> activeAndPassivePlayers;
        private Deck deck;
        #endregion

        #region Properties
        // TODO: do we need ID for the Room? if so, what type should it be? 'long?' means nullable long.
        //public long? ID { get; }
        public ICollection<Chair> Chairs { get; }
        public Hand CurrentHand { get; private set; }
        public GamePreferences Preferences { get; set; }
        private ICollection<Player> ActivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.ActiveUnfolded | player.CurrentState == Player.State.ActiveFolded))
                    .ToList();

                /* idan:
                 * this is another cool way to filter on a collection (ICollection). just leaving it here...
                 * */
                //(from player in activeAndPassivePlayers
                //where
                // (player.CurrentState == Player.State.ActiveFolded || player.CurrentState == Player.State.ActiveUnfolded)
                //select player).ToList();
            }
        }
        private ICollection<Player> PassivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.ActiveUnfolded | player.CurrentState == Player.State.ActiveFolded))
                    .ToList();
            }
        }
        #endregion

        #region Constructors
        private Room()
        {
            activeAndPassivePlayers = new List<Player>();
            deck = new Deck();
            Chairs = new Chair[Chair.NCHAIRS_IN_ROOM];

            for (int i = 0; i < Chair.NCHAIRS_IN_ROOM; i++)
            {
                Chairs.ToArray()[i] = new Chair(i);
            }

            CurrentHand = null;

            // Note: making default preferences to the room (poker game)
            Preferences = new GamePreferences();

        }

        public Room(Player creator) : this()
        {
            activeAndPassivePlayers.Add(creator);
        }

        public Room(Player creator, GamePreferences preferences) : this(creator)
        {
            Preferences = preferences;
        }

        #endregion

        #region Methods
        public void StartNewHand()
        {
            if (ActivePlayers.Count < Hand.MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new NotEnoughPlayersException();
            }
            CurrentHand = new Hand(deck, ActivePlayers);
        }


        public void SendMessage()
        {
            //TODO: 'UC006: Send Message to Room’s Chat' - for the ones that doing that
        }
        #endregion


    }//class
}
