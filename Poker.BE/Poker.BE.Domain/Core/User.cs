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
        public User(string userName, string password, double sumToDeposit)
        {
            UserName = userName;
            Password = password;
            UserBank = new Bank(sumToDeposit);
            IsConnected = true;
            Players = new List<Player>();
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


        #region Find an Existing Room
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
                             where Players.Contains(player)
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

            Players.Add(freshPlayer.GetHashCode(), freshPlayer);

            return freshPlayer;
        }

        public bool CreateNewRoom(int level, GameConfig config)
        {
            // TODO
            throw new NotImplementedException();
        }

        public bool JoinNextHand(int sessionID, int seatIndex, double buyIn)
        {
            // TODO
            throw new NotImplementedException();
        }

        public bool StandUpToSpactate(int sessionID)
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

    }
}
