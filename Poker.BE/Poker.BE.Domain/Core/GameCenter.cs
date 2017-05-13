using Poker.BE.Domain.Game;
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
    public class GameCenter
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
        #endregion

        #region Fields
        private IDictionary<Player, Room> playersManager;
        private IDictionary<Room, League> roomsManager;
        private IDictionary<int, League> leaguesManager;
        private ICollection<League> leagues;
        #endregion

        #region Properties
        public ICollection<Room> Rooms { get { return roomsManager.Keys; } }
        public ICollection<Player> Players { get { return playersManager.Keys; } }
        public ICollection<League> Leagues { get { return leagues; } }
        #endregion

        #region Constructors
        public GameCenter()
        {
            playersManager = new Dictionary<Player, Room>();
            roomsManager = new Dictionary<Room, League>();
            leaguesManager = new Dictionary<int, League>();
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

        private void AddLeage(int level)
        {
            var league = new League();
            leagues.Add(league);
            leaguesManager.Add(level, league);
        }

        private void BindRoomToLeague(Room room, League league)
        {
            roomsManager.Add(room, league);
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

            // remove all occurrences of this league at the league's dictionary
            foreach (var key in leaguesManager.Where(pair => pair.Value == league).Select(pair => pair.Key))
            {
                leaguesManager.Remove(new KeyValuePair<int, League>(key, league));
            }

            // remove the league from the league collection (field)
            return leagues.Remove(league);
        }

        private void BindPlayerToRoom(Player player, Room room)
        {
            playersManager.Add(player, room);
        }
        #endregion

        #region Methods

        #region Find an Existing Room
        /// <summary>
        /// Allow the user to find an existing room according to different criteria and enter the room as a spectator.
        /// </summary>
        /// <remarks>UC004: Find an Existing Room</remarks>
        /// <returns>Collection of rooms</returns>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.tvbd8487o8xd"/>
        public ICollection<Room> FindRoomsByCriteria(int level)
        {
            // TODO
            throw new NotImplementedException();
        }

        public ICollection<Room> FindRoomsByCriteria(Player player)
        {
            // TODO
            throw new NotImplementedException();
        }

        public ICollection<Room> FindRoomsByCriteria(GamePreferences preferences)
        {
            // TODO
            throw new NotImplementedException();
        }

        public ICollection<Room> FindRoomsByCriteria(double betSize)
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

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
        public Room CreateNewRoom(int level, GameConfig config)
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allow the user to join the table after entering a room, and become an active player at the game in the next hand.
        /// </summary>
        /// <remarks>UC020: Join Next Hand</remarks>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.yy00l1jatp9d"/>
        /// <param name="room"></param>
        public void JoinNextHand(Player player)
        {
            // TODO
            throw new NotImplementedException();
        }

        public void StandUpToSpactate(Player player)
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

    }
}
