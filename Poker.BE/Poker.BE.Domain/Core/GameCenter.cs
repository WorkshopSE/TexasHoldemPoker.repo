using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Core
{
	/// <summary>
	/// Responsible for the connection of the user with the 'Game' package.
	/// </summary>
	/// <remarks>
	/// <author>Idan Izicovich</author>
	/// </remarks>
	public sealed class GameCenter
	{
		#region Constants
		public enum Move
		{
			Check,
			Fold,
			Bet,
			Raise,
			Call,
			AllIn
		}

		/// <summary>
		/// clears all game-center resources
		/// </summary>
		public void ClearAll()
		{
			playersManager.Clear();
			roomsManager.Clear();
			leagues.Clear();
		}
		bool ROOM_NAME_UNAVAILABLE = false;
		bool ROOM_NAME_AVAILABLE = true;
		#endregion

		#region Fields
		private IDictionary<Player, Room> playersManager;
		private IDictionary<Room, League> roomsManager;
		private ICollection<League> leagues;
		#endregion

		#region Properties
		public ICollection<Room> Rooms { get { return roomsManager.Keys; } }
		public ICollection<Player> Players { get { return playersManager.Keys; } }
		public ICollection<League> Leagues { get { return leagues; } }
		#endregion

		#region Constructors

		#region Singleton
		// for singleton implementation at C#
		static GameCenter() { }
		private static readonly GameCenter _instance = new GameCenter();
		public static GameCenter Instance { get { return _instance; } }
		#endregion

		// all constructors needs to be private for being a singleton
		private GameCenter()
		{
			playersManager = new Dictionary<Player, Room>();
			roomsManager = new Dictionary<Room, League>();
			leagues = new List<League>();
		}


		#endregion

		#region Private Functions
		/// <summary>
		/// When a user enters a room, allow the system to create a player to represent the user in the game.
		/// </summary>
		/// <remarks>UC022: Create Player</remarks>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.my2n008zu6pu"/>
		/// <param name="room">the player enters to it</param>
		/// <returns>a new player</returns>
		private Player CreatePlayer(Room room)
		{
			return room.CreatePlayer();
		}

		private League AddLeage(int minLevel = League.MIN_LEVEL, int maxLevel = League.MAX_LEVEL)
		{
			var league = new League() { MaxLevel = maxLevel, MinLevel = minLevel };
			leagues.Add(league);
			return league;
		}

		private bool RemovePlayer(Player player)
		{
			playersManager[player].RemovePlayer(player);
			return playersManager.Remove(player);
		}

		private bool RemoveRoom(Room room)
		{
			// Note: aggressive approach - all the players kicked out
			/* TODO: try/catch for gentle approach?
             * example - if there are players at the room, this removal will fail.
             * */
			if (room.Players.Count > 0)
			{
				foreach (var player in room.Players)
				{
					RemovePlayer(player);
				}

				room.ClearAll();
			}

			// remove the room from the league
			roomsManager[room].RemoveRoom(room);

			// remove the room from the room's dictionary.
			return roomsManager.Remove(room);
		}

		private bool RemoveLeague(League league)
		{
			if (league.Rooms.Count > 0)
			{
				foreach (var room in league.Rooms)
				{
					RemoveRoom(room);
				}

				// call clear() of current room, to clean all his resources.
				league.Rooms.Clear();
			}

			// remove the league from the league collection (field)
			return leagues.Remove(league);
		}

		private void BindPlayerToRoom(Player player, Room room)
		{
			playersManager.Add(player, room);
		}

		private void BindRoomToLeague(Room room, League league)
		{
			roomsManager.Add(room, league);
		}

		private bool SetLeagueLevelRange(int minlevel, int maxLevel, League league)
		{
			if (!leagues.Contains(league)) return false;

			league.MaxLevel = maxLevel;
			league.MinLevel = minlevel;

			return true;
		}

		/// <summary>
		/// look up for leagues that their level rang contains the requested level
		/// </summary>
		/// <param name="level">the user level</param>
		/// <returns>a collection of leagues</returns>
		private ICollection<League> GetAllLeaguesAtLevel(int level)
		{
			var result = new List<League>();

			foreach (var league in leagues)
			{
				if (league.MinLevel < level & league.MaxLevel > level)
				{
					result.Add(league);
				}
			}

			if (result.Count == 0)
			{
				result.Add(AddLeage());
			}

			return result;
		}

		private League FindLeagueToFill(ICollection<League> leaguesPartialGroup)
		{
			// TODO: pick a league to insert the new created room, by the relevant requirements.

			// HACK: this is a stub return
			return
				(from league in leaguesPartialGroup
				 where !league.IsFull
				 select league).First();
		}

		private bool IsRoomNameAvailable(String name)
		{
			foreach(KeyValuePair<Room,League> roomPair in roomsManager)
			{
				if (name == roomPair.Key.Name)
				{
					return ROOM_NAME_UNAVAILABLE;
				}
			}
			return ROOM_NAME_AVAILABLE;
		}
		#endregion

		#region Methods

		/// <summary>
		/// Allow the user to find an existing room according to different criteria and enter the room as a spectator.
		/// </summary>
		/// <remarks>UC004: Find an Existing Room</remarks>
		/// <returns>Collection of rooms</returns>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.tvbd8487o8xd"/>
		public ICollection<Room> FindRoomsByCriteria(int level = -1, Player player = null, GamePreferences preferences = null, double betSize = -1.0)
		{
			var result = new List<Room>();

			if (level > 0)
			{
				var collections = (from league in leagues
								   where league.MinLevel <= level & league.MaxLevel >= level
								   select league.Rooms);

				var flat =
					collections.Aggregate(new List<Room>(), (acc, x) => acc.Concat(x).ToList())
					.Distinct(new Utility.AddressComparer<Room>());
				result.AddRange(flat);
			}

			if (player != null)
			{
				Room room = null;
				if (!playersManager.TryGetValue(player, out room))
				{
					throw new RoomNotFoundException("room not found for player: " + player.GetHashCode().ToString());
				}

				if (result.Count == 0)
				{
					result.Add(room);
				}
				else
				{
					result = (from item in result where item == room select item).ToList();
					if (result.Count != 1)
					{
						throw new RoomNotFoundException("mismatch of searching room. only 1 occurrence didn't returned");
					}
				}
			}

			if (preferences != null)
			{
				// undone - idan - continue from here - after game preferences will be implemented.
			}

			if (betSize > 0)
			{
				result.AddRange(
					from room in Rooms
					where room.MinimumBet == betSize
					select room
					);
			}

			if (result.Count == 0)
			{
				throw new RoomNotFoundException(
					"no rooms are find by the criteria: "
					+ (level > -1 ? "level: " + level : "")
					+ (player != null ? "player id: " + player.GetHashCode() : "")
					+ (preferences != null ? "game preferences: " + preferences.GetType().Name : "")
					+ (betSize > -1.0 ? "bet size: " + betSize : "")
					);
			}

			return result;
		}

		/// <summary>
		/// Allow the user to enter a room from a list as a spectator.
		/// </summary>
		/// <param name="room">enter to</param>
		/// <returns>a new created player for the user to hold</returns>
		/// <remarks>UC021: Enter a Room</remarks>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.tzy1eb1jifgr"/>
		public Player EnterRoom(Room room)
		{
			var player = CreatePlayer(room);

			// for the gameCenter player's dictionary (players manager)
			BindPlayerToRoom(player, room);

			return player;
		}

		/// <summary>
		/// Allow the user to create a room with a table inside it for Texas Hold’em Poker game that other user can join & play.
		/// </summary>
		/// <remarks>UC003: Create a New Room</remarks>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.eqjp0wvvpmjg"/>
		/// <param name="level">user level</param>
		/// <returns>the new created room</returns>
		public Room CreateNewRoom(int level, GameConfig config, out Player creator)
		{
			creator = new Player();

			// creating the room and adding the creator as a passive player to it.
			var room = new Room(creator, config);
			BindPlayerToRoom(creator, room);

			League league = FindLeagueToFill(GetAllLeaguesAtLevel(level));

			if (league == null)
				throw new LevelNotFoundException("Requested level: " + level);

			league.Rooms.Add(room);
			BindRoomToLeague(room, league);

			return room;
		}

		/// <summary>
		/// Allow the user to join the table after entering a room, and become an active player at the game in the next hand.
		/// </summary>
		/// <remarks>UC020: Join Next Hand</remarks>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.yy00l1jatp9d"/>
		/// <param name="player"></param>
		public void JoinNextHand(Player player, int seatIndex, double buyIn)
		{
			/* Checking Preconditions */

			// get the room of the player belongs to
			Room room = null;
			if (!playersManager.TryGetValue(player, out room))
			{
				throw new RoomNotFoundException("Try joining next hand for a player with room that can't be found");
			}

			// check the user is a spectator
			if (player.CurrentState != Player.State.Passive)
			{
				throw new PlayerModeException("Player " + player.Nickname + " is trying to join a hand, but he's not a spectator!");
			}

			// check if table is full
			if (room.IsTableFull)
			{
				throw new RoomRulesException("The Table is full.");
			}

			// the chosen seat is not taken
			if (!room.TakeChair(player, seatIndex))
			{
				throw new RoomRulesException("The seat is already taken, please try again.");
			}

			// the user has enough money to buy in
			if (buyIn < room.BuyInCost)
			{
				throw new NotEnoughMoneyException("Buy in amount is less then the minimum to join the table.");
			}

			/* Joining the player to the next hand */
			room.JoinPlayerToTable(player, buyIn);
		}

		/// <summary>
		/// Allow the player to be a spectator over the table at the room, after playing (get up from the table)
		/// </summary>
		/// <returns>remaining money from the wallet to transfer back to the user bank</returns>
		/// <remarks>UC013: Stand Up To Spectate</remarks>
		/// <param name="player"></param>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.kl4k8p3d5mt6"/>
		public double StandUpToSpactate(Player player)
		{
			/* Checking Preconditions */

			// the player is sitting at the table
			Room room = null;
			if (!playersManager.TryGetValue(player, out room))
			{
				throw new RoomNotFoundException("Unable to stand up - The player is not at the room");
			}

			// it's the player's turn

			try
			{
				if (room.CurrentHand.CurrentRound.CurrentTurn.CurrentPlayer != player)
				{
					throw new NotPlayersTurnException("Unable to stand up");
				}
			}
			catch (NullReferenceException)
			{
				if (player.CurrentState == Player.State.Passive)
				{
					throw new RoomRulesException("Player is already a spectator");
				}
			}

			/* Action - Make the player to stand up */

			room.LeaveChair(player);
			return player.StandUp();
		}
		#endregion

	}
}
