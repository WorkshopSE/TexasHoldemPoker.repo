using Poker.BE.Domain.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
        private Chair[] chairs;
        #endregion

        #region Properties
        // TODO: Question: do we need ID for the Room? if so, what type should it be? 'long?' means nullable long.
        //public long? ID { get; }

        public ICollection<Chair> Chairs { get { return chairs; } }
        public Hand CurrentHand { get; private set; }
        public GamePreferences Preferences { get; set; }
        private ICollection<Player> ActivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.ActiveUnfolded | player.CurrentState == Player.State.ActiveFolded))
                    .ToList();
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

            // Initiate chairs in the room
            chairs = new Chair[Chair.NCHAIRS_IN_ROOM];

            for (int i = 0; i < Chair.NCHAIRS_IN_ROOM; i++)
            {
                chairs[i] = new Chair(i);
            }

            CurrentHand = null;

            // Note: making default preferences to the room (poker game)
            Preferences = new GamePreferences();

        }

        public Room(Player creator) : this()
        {
            // Note: the creator is passive palyer at creation.
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
            CurrentHand = new Hand(deck, ActivePlayers);
        }

        public void SendMessage()
        {
            //TODO: 'UC006: Send Message to Room’s Chat' - for the ones that doing that
        }
        #endregion

        #region Private Functions
        private void TakeAChair(int index)
        {
            chairs[index].Take();
        }

        private void ReleaseAChair(int index)
        {
            chairs[index].Release();
        }
        #endregion


    }//class
}
