using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Exceptions;
using Poker.BE.Domain.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Core
{
    public class User : AbstractUser
    {
        #region Fields
        /* singletons */
        private GameCenter gameCenter = GameCenter.Instance;
        private ILogger logger = Logger.Instance;
        /* ---------- */

        #endregion

        #region Properties

        public ICollection<Player> Players { get; set; }
        #endregion

        #region Constructors
        public User()
        {
            IsConnected = false;
            Password = "";
            Players = new List<Player>();
            UserBank = new Bank();
            UserName = GetHashCode().ToString();
        }

        public User(string userName, string password, double sumToDeposit) : this()
        {
            UserName = userName;
            Password = password;
            UserBank = new Bank(sumToDeposit);
            IsConnected = true;
        }
        #endregion

        #region Methods

        // TODO: for Ariel - what is this 2 methods?
        public void Connect()
        {
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        #region UCC03 Rooms Management Methods
        /// <summary>
        /// Allow the user to find an existing room according to different criteria and enter the room as a spectator.
        /// </summary>
        /// <remarks>UC004: Find an Existing Room</remarks>
        /// <returns>Collection of rooms</returns>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.tvbd8487o8xd"/>
        public ICollection<Room> FindRoomsByCriteria(int level = -1, Player player = null, GamePreferences preferences = null, double betSize = -1.0)
        {
            return gameCenter.FindRoomsByCriteria(level, player, preferences, betSize);
        }

        /// <summary>
        /// Allow the user to enter a room from a list as a spectator.
        /// </summary>
        /// <param name="room">to enter</param>
        /// <returns>
        /// Session ID of the new player for this user, for the user to store as a cookie
        /// </returns>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.tzy1eb1jifgr"/>
        public Player EnterRoom(Room room)
        {
            // check that the user doesn't have already a player in the room
            if (Players.Count > 0 && room.Players.Count > 0)
            {
                var result = from player in room.Players
                             where Players.Contains(player, new Utility.AddressComparer<Player>())
                             select player;

                if (result.Count() > 0)
                {
                    throw new RoomRulesException(
                        string.Format("the user already play this room: {0} with player nickname: {1}",
                        room.Name, result.First().Nickname));
                }
            }

            // entering the room
            var freshPlayer = gameCenter.EnterRoom(room);

            // logging
            logger.Log(string.Format("User {0} has player {1}", UserName, freshPlayer.GetHashCode()),
                this, "Medium");

            Players.Add(freshPlayer);

            return freshPlayer;
        }

        public Room CreateNewRoom(int level, GameConfig config, out Player creator)
        {
            var result = gameCenter.CreateNewRoom(level, config, out creator);
            Players.Add(creator);

            // log info
            logger.Info(string.Format("user {0} has created an new room {1}", GetHashCode(), result.GetHashCode()), this);

            return result;
        }

        public void JoinNextHand(Player player, int seatIndex, double buyIn)
        {
            if(!Players.Contains(player, new Utility.AddressComparer<Player>()))
            {
                throw new PlayerNotFoundException("the user doesn't have this player");
            }
            
            gameCenter.JoinNextHand(player, seatIndex, buyIn);
        }

        public double StandUpToSpactate(Player player)
        {
            if (!Players.Contains(player, new Utility.AddressComparer<Player>()))
            {
                throw new PlayerNotFoundException("the user doesn't have this player");
            }

            UserBank.Money = gameCenter.StandUpToSpactate(player);
            
            return UserBank.Money;
        }
        #endregion


        #endregion

    }
}
