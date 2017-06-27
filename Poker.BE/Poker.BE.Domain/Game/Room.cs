using System;
using System.Collections.Generic;
using System.Linq;
using Poker.BE.CrossUtility.Exceptions;
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
        #region Constants
        public const int NCHAIRS_IN_ROOM = 10;
        #endregion

        #region Fields
        private ICollection<Player> activeAndPassivePlayers;
        private Chair[] chairs;
        private int dealerIndex = 0;
        private object _lock;
        #endregion

        #region Properties

        //Return all players (active+passive) in the room
        public ICollection<Player> Players { get { return activeAndPassivePlayers; } }
        public ICollection<Player> ActivePlayers
        {
            get
            {
                var result = from player in activeAndPassivePlayers
                             where player.CurrentState != Player.State.Passive
                             select player;

                return result.ToList();
            }
        }
        public ICollection<Player> PassivePlayers
        {
            get
            {
                var result = from player in activeAndPassivePlayers
                             where player.CurrentState == Player.State.Passive
                             select player;

                return result.ToList();
            }
        }
        public Dictionary<int, Player> ActivePlayersByID { get; private set; }

        public ICollection<Chair> Chairs { get { return chairs; } }
        public IDictionary<Chair, Player> TableLocationOfActivePlayers { get; private set; }

        public Hand CurrentHand { get; private set; }
        public GamePreferences Preferences { get; }
        public bool IsTableFull
        {
            get
            {
                return ActivePlayers.Count == Preferences.MaxNumberOfPlayers;
            }
        }
        public Task GameThread { get; set; }
        #endregion

        #region Constructors
        private Room()
        {
            activeAndPassivePlayers = new List<Player>();
            ActivePlayersByID = new Dictionary<int, Player>();

            chairs = new Chair[NCHAIRS_IN_ROOM];

            for (int i = 0; i < NCHAIRS_IN_ROOM; i++)
            {
                chairs[i] = new Chair(i);
            }

            CurrentHand = null;
            TableLocationOfActivePlayers = new Dictionary<Chair, Player>();

            // Note: default configuration
            Preferences = new NoLimitHoldem();

            _lock = this;

            GameThread = new Task(() => StartNewHand());
            GameThread.Start();

        }

        /// <summary>
        /// UC003 Create a new room 
        /// </summary>
        /// <param name="creator">enter the room as a passive player.</param>
        /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.tzy1eb1jifgr"/>
        public Room(Player creator) : this()
        {
            activeAndPassivePlayers.Add(creator);
        }

        /// <summary>
        /// UC003 Create a new room 
        /// </summary>
        /// <param name="creator">enter the room as a passive player.</param>
        /// <param name="preferences">limit / no limit / pot limit </param>
        /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.tzy1eb1jifgr"/>
        public Room(Player creator, GamePreferences preferences) : this(creator)
        {
            Preferences = preferences;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Converting passive player to active player.
        /// precondition: the player must be a passive player at the room.
        /// postcondition: the player is active player at the room.
        /// </summary>
        /// <param name="player">a passive player at the room</param>
        public void JoinPlayerToTable(Player player, double buyIn)
        {
            if (player.CurrentState != Player.State.Passive)
            {
                throw new PlayerModeException("Player is not a spectator");
            }

            ActivePlayersByID.Add(player.GetHashCode(), player);

            player.JoinToTable(buyIn);

            if (ActivePlayers.Count >= Preferences.MinNumberOfPlayers)
            {
                Monitor.PulseAll(this);
            }
        }

        public void RemovePlayer(Player player)
        {
            activeAndPassivePlayers.Remove(player);
        }

        /// <summary>
        /// Method as a destructor - delete all players and other resources from the room.
        /// </summary>
        /// <remarks>
        /// this function is used by gameCenter do delete the room.
        /// All players and other resources of room need to be deleted.
        /// </remarks>
        public void ClearAll()
        {
            this.activeAndPassivePlayers.Clear();
            this.CurrentHand = null;
        }

        public Player CreatePlayer()
        {
            var result = new Player() { Lock = _lock };
            activeAndPassivePlayers.Add(result);
            return result;
        }

        /// <summary>
        /// UC014 Start (Deal) a New Hand
        /// </summary>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.3z6a7b6nlnjj"/>
        public void StartNewHand()
        {
            while (true)
            {

                Monitor.Enter(this);
                while (ActivePlayers.Count < Preferences.MinNumberOfPlayers)
                {
                    Monitor.Wait(this);
                    //throw new NotEnoughPlayersException("Its should be at least 2 active players to start new hand!");
                }
                Monitor.Exit(this);

                if (this.CurrentHand != null && this.CurrentHand.Active)
                {
                    throw new NotEnoughPlayersException("The previous hand hasnt ended");
                }

                Player dealer = ActivePlayers.ElementAt(dealerIndex);
                CurrentHand = new Hand(dealer, ActivePlayers, Preferences);

                CurrentHand.PlayHand(_lock);
                EndCurrentHand();

                /* Note: only the first player joining the room start 
                 * the new thread of the hand 
                 */

            }
        }

        public void EndCurrentHand()
        {
            CurrentHand.EndHand();
            foreach (Player player in ActivePlayers)
            {
                player.AddStatistics(CurrentHand.PlayersBets[player]);
            }

            dealerIndex++;
        }

        public bool TakeChair(Player player, int index)
        {
            try
            {
                var result = PassivePlayers.Where(item => item == player);
                if (result.Count() != 1 || !chairs[index].Take())
                {
                    return false;
                }

                // register player location at the table.
                TableLocationOfActivePlayers.Add(chairs[index], player);

                return true;
            }
            catch (IndexOutOfRangeException)
            {
                throw new RoomRulesException("chair index does not exists");
            }
        }

        public double LeaveChair(Player player)
        {
            if (TableLocationOfActivePlayers.Values.Contains(player))
            {
                foreach (var chair in TableLocationOfActivePlayers.Keys)
                {
                    if (TableLocationOfActivePlayers[chair] == player)
                    {
                        TableLocationOfActivePlayers.Remove(chair);
                        ActivePlayersByID.Remove(player.GetHashCode());
                        chair.Release();
                        return player.StandUp();
                    }
                }
            }
            throw new PlayerNotFoundException("Can't find player at the table");
        }

        public void SendMessage()
        {
            //TODO: 'UC006: Send Message to Room’s Chat' - for the ones that doing that
        }
        #endregion

    }
}