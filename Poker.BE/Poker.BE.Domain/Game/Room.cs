using Poker.BE.Domain.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Poker.BE.Domain.Utility;

namespace Poker.BE.Domain.Game
{

    /// <summary> Defined the room that the players are playing at the game </summary>
    /// <remarks>
    /// <author>Idan Izicovich</author>
    /// <author>Tomer Amdur</author>
    /// <lastModified>2017-04-29</lastModified>
    /// </remarks>
    public class Room
    {
        #region Fields
        private ICollection<Player> activeAndPassivePlayers;
        private StatisticsManager statisticManager;
        private Deck deck;
        #endregion

        #region Properties
        // TODO: do we need ID for the Room? if so, what type should it be? 'long?' means nullable long.
        //public long? ID { get; }

        public ICollection<Chair> Chairs { get; private set; }
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
            TakeAChair(0);
            activeAndPassivePlayers.Add(creator);
        }

        private void TakeAChair(int index)
        {
            // TODO - idan - fix null pointer exception
            Chairs.ElementAt(index).Take();
        }

        private void ReleaseAChair(int index)
        {
            Chairs.ElementAt(index).Release();
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

        // TODO: Take a chair, leave a chair - will chairsSemaphore.WaitOne() or Release()

        public void SendMessage()
        {
            //TODO: 'UC006: Send Message to Room’s Chat' - for the ones that doing that
        }
        
        public void StartRecording()
        {
            //TODO: UC016: Store & Retrieve Games Information
        }

        public void UpdateUserStatistics(int userId, bool won, int amount)
        {
            statisticManager.UpdateUserStatistics(userId, won, amount);
        }
        #endregion


    }//class
}
