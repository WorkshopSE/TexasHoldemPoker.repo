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
        #region Constants
        public const int NCHAIRS_IN_ROOM = 10;
        #endregion

        #region Fields
        private ICollection<Player> activeAndPassivePlayers;
        private Deck deck;
        private Chair[] chairs;
        #endregion

        #region Properties
        // TODO: do we need ID for the Room? if so, what type should it be? 'long?' means nullable long.
        //public long? ID { get; }

        public string Name { get; set; }
        public ICollection<Chair> Chairs { get { return chairs; } }
        public Hand CurrentHand { get; private set; }
        public GamePreferences Preferences { get; set; }
        public ICollection<Player> ActivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.ActiveUnfolded | player.CurrentState == Player.State.ActiveFolded))
                    .ToList();
            }
        }
        public ICollection<Player> PassivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.ActiveUnfolded | player.CurrentState == Player.State.ActiveFolded))
                    .ToList();
            }
        }
        public ICollection<Player> Players { get { return activeAndPassivePlayers; } }
        public bool IsSpactatorsAllowd { get; }
        public int MaxNumberOfPlayers { get; private set; }
        public int MinNumberOfPlayers { get; private set; }
        public int MaxNumberOfActivePlayers
        {
            get { return MaxNumberOfActivePlayers; }
            private set
            {
                // enforce number of active players < number of chairs.
                MaxNumberOfActivePlayers = (value > NCHAIRS_IN_ROOM) ? NCHAIRS_IN_ROOM : value;
            }
        }
        public double MinimumBet { get; private set; }
        public bool IsTableFull
        {
            get
            {
                return ActivePlayers.Count == MaxNumberOfActivePlayers;
            }
        }

        #endregion

        #region Constructors
        private Room()
        {
            activeAndPassivePlayers = new List<Player>();
            deck = new Deck();
            chairs = new Chair[NCHAIRS_IN_ROOM];

            for (int i = 0; i < NCHAIRS_IN_ROOM; i++)
            {
                Chairs.ToArray()[i] = new Chair(i);
            }

            CurrentHand = null;

            // Note: making default preferences to the room (poker game)
            Preferences = new GamePreferences();

            Name = "Unknown Room";

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

        public Room(Player creator, GameConfig config) : this(creator, config.GamePrefrences)
        {
            IsSpactatorsAllowd = config.IsSpactatorsAllowed;
            MaxNumberOfPlayers = config.MaxNumberOfPlayers;
            MinNumberOfPlayers = config.MinNumberOfPlayers;
            MinimumBet = config.MinimumBet;
            Name = config.Name;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Converting passive player to active player.
        /// precondition: the player must be a passive player at the room.
        /// postcondition: the player is active player at the room.
        /// </summary>
        /// <param name="player">a passive player at the room</param>
        public bool JoinPlayerToTable(Player player, double buyIn)
        {
            if (player.CurrentState != Player.State.Passive)
            {
                return false;
            }

            return player.JoinToTable(buyIn);
        }

        public void RemovePlayer(Player player)
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method as a destructor - delete all players and other resources from the room.
        /// </summary>
        public void ClearAll()
        {
            //TODO: this function used be gameCenter do delete the room. all players and other resources of room need to be deleted.
            throw new NotImplementedException();
        }

        public Player CreatePlayer()
        {
            var result = new Player();
            activeAndPassivePlayers.Add(result);
            return result;
        }
		/// <summary>
		/// Allow the system to deal new cards to the players at the table.
		/// </summary>
		/// <returns></returns>
		/// <remarks>UC014: Deal A New Hand</remarks>
		public void StartNewHand()
        {
			/* Checking Preconditions */

			//min amount of players
			if(ActivePlayers.Count()<MinNumberOfPlayers)
			{
				throw new NotEnoughPlayersException("not enough players to start hand");
			}

			//last hand ended 

			if(CurrentHand!=null || !CurrentHand.handEnded)
			{
				throw new HandNotOverException("previous hand hasnt ended");
			}
			//Deal Cards
			foreach(Player Player in ActivePlayers)
			{
				Player.PrivateCards[0] = deck.PullCard();
				Player.PrivateCards[1] = deck.PullCard();
			}
			//Place Bids
			CurrentHand.PlaceBlinds();
			deck.ShuffleCards();
			CurrentHand = new Hand(deck, ActivePlayers);
        }
		public void EndCurrentHand()
		{
			//TODO UC31
			CurrentHand.handEnded = true;
		}
        public bool TakeChair(Player player, int index)
        {
            // TODO
            throw new NotImplementedException();
        }

        public bool LeaveChair(Player player)
        {
            // TODO
            throw new NotImplementedException();
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
