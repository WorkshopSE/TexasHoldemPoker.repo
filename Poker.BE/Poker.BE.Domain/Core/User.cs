﻿using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility;
using Poker.BE.CrossUtility.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using Poker.BE.CrossUtility.Exceptions;

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
        public Statistics UserStatistics { get; private set; }
        public ICollection<Player> Players { get; set; }
        public int Level { get; set; }
        public int? SecurityKey { get; private set; }
        #endregion

        #region Constructors
        public User()
        {
            IsConnected = false;
            Password = "";
            Players = new List<Player>();
            UserBank = new Bank();
            UserName = GetHashCode().ToString();
            UserStatistics = new Statistics();
            Level = 0;
	    Avatar = null;
        }

        public User(string userName, string password, double sumToDeposit) : this()
        {
            UserName = userName;
            Password = password;
            UserBank = new Bank(sumToDeposit);
            IsConnected = true;
     	    Avatar = null;
        }
        #endregion

        #region Methods

        #region Security Methods
        /// <summary>
        /// Connect user on-line
        /// </summary>
        /// <param name="key">security random key</param>
        public void Connect(int key)
        {
            SecurityKey = key;
            IsConnected = true;
        }

        public void Disconnect()
        {
            SecurityKey = null;
            IsConnected = false;
        }

        /// <summary>
        /// tests if the user is secure by its security key
        /// </summary>
        /// <param name="key">security key sent by the actual user</param>
        /// <returns>true case this is the true key</returns>
        public bool IsSecure(int key)
        {
            return key == SecurityKey;
        }
        #endregion

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
                        room.Preferences.Name, result.First().Nickname));
                }
            }

            // entering the room
            var freshPlayer = gameCenter.EnterRoom(room);

            // logging
            logger.Log(string.Format("User {0} has player {1}", UserName, freshPlayer.GetHashCode()),
                this, "Medium");

            //registering to an event: the player got the turn
            freshPlayer.TurnChanged += ChoosePlayMove;

            //Adding the player to players collection
            Players.Add(freshPlayer);

            return freshPlayer;
        }

        public Room CreateNewRoom(int level, GamePreferences config, out Player creator)
        {
            var result = gameCenter.CreateNewRoom(level, config, out creator);
            Players.Add(creator);

            // log info
            logger.Info(string.Format("user {0} has created an new room {1}", GetHashCode(), result.GetHashCode()), this);

            return result;
        }

        public void JoinNextHand(Player player, int seatIndex, double buyIn)
        {
            if (!Players.Contains(player, new Utility.AddressComparer<Player>()))
            {
                throw new PlayerNotFoundException("the user doesn't have this player");
            }

            gameCenter.JoinNextHand(player, seatIndex, buyIn);
            UserBank.Withdraw(buyIn);
        }

        public double StandUpToSpactate(Player player)
        {
            if (!Players.Contains(player, new AddressComparer<Player>()))
            {
                throw new PlayerNotFoundException("the user doesn't have this player");
            }

            //Player is not ActiveFolded
            if (player.CurrentState != Player.State.ActiveFolded)
            {
                throw new RoomRulesException("Player is not in ActiveFolded state");
            }

            UserBank.Deposit(gameCenter.StandUpToSpactate(player));

            return UserBank.Money;
        }

        public void ExitRoom(Player player)
        {
            //Remove player from the room
            gameCenter.ExitRoom(player);

            //Add dead player's statistics to user's statistics
            UserStatistics.CombineStatistics(player.PlayerStatistics);

            //Update Level
            Level = CalculateLevel();

            Players.Remove(player);
        }
        #endregion

        #region UCC02
        public int CalculateLevel()
        {
            double winRate = UserStatistics.GrossProfits - UserStatistics.GrossLosses;

            if (winRate < 1)
                return 1;
            else if (winRate > 6000)
                return 600;

            return (int)winRate / 10;
        }
        #endregion

        #region UCC05 Statistics
        public double GetWinRateStatistics()
        {
            int gamesPlayed = UserStatistics.GamesPlayed;
            double grossProfits = UserStatistics.GrossProfits;
            double grossLosses = UserStatistics.GrossLosses;

            foreach (Player player in Players)
            {
                gamesPlayed += player.PlayerStatistics.GamesPlayed;
                grossProfits += player.PlayerStatistics.GrossProfits;
                grossLosses += player.PlayerStatistics.GrossLosses;
            }

            return (grossProfits - grossLosses) / gamesPlayed;
        }

        public double GetGrossProfitWinRateStatistics()
        {
            int gamesPlayed = UserStatistics.GamesPlayed;
            double grossProfits = UserStatistics.GrossProfits;

            foreach (Player player in Players)
            {
                gamesPlayed += player.PlayerStatistics.GamesPlayed;
                grossProfits += player.PlayerStatistics.GrossProfits;
            }

            return grossProfits / gamesPlayed;
        }
        #endregion

        #region UCC06: GamePlay
        public void ChoosePlayMove(object sender, EventArgs e)
        {
            //Note: systactic suger for checking if sender is a player type
            if ((sender is Player) && (e is PlayMoveEventArgs))
            {
                Player player = (Player)sender;
                PlayMoveEventArgs playMoveEvent = (PlayMoveEventArgs)e;

                if (!Players.Contains(player))
                    throw new PlayerNotFoundException("This player doesn't belong to this user");

                player.ChoosePlayMove(playMoveEvent.PlayMove, playMoveEvent.AmountToBetOrCall);
            }
        }
        #endregion

        #endregion

    }
}
