﻿using System;
using System.Collections.Generic;
using System.Linq;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Core;
using Poker.BE.CrossUtility.Loggers;
using Poker.BE.Domain.Security;
using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Service.Modules.Caches;

namespace Poker.BE.Service.Services
{
    /// <summary>
    /// UCC03 Rooms Management
    /// </summary>
    /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.286w5j2ewu5c"/>
    public class RoomsService : IServices.IRoomsService
    {
        #region Fields
        private CommonCache _cache;
        #endregion

        #region Properties
        /// <summary>
        /// Map for the player (user session) ID -> player at the given session.
        /// using the player.GetHashCode() for generating this ID.
        /// </summary>
        /// <remarks>
        /// session ID is the ID we give for a screen the user opens.
        /// this is a need because the user can play several screen at once.
        /// thus, to play with different players at the same time.
        /// 
        /// for now - the user cannot play as several players, at the same room.
        ///    - this option is blocked.
        /// </remarks>
        public IDictionary<int, Player> Players { get { return _cache.Players; } }
        public IDictionary<int, Room> Rooms { get { return _cache.Rooms; } }
        public IDictionary<string, User> Users { get { return _cache.Users; } }
        public UserManager UserManager { get { return _cache.UserManager; } }
        public GameCenter GameCenter { get { return _cache.GameCenter; } }
        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
        #endregion

        #region Constructors
        public RoomsService()
        {
            _cache = CommonCache.Instance;
        }
        #endregion

        #region Private Functions

        #endregion

        #region Methods
        /// <summary>
        /// for testing
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        public CreateNewRoomResult CreateNewRoom(CreateNewRoomRequest request)
        {
            var result = new CreateNewRoomResult();
            User user;
            while (!Users.TryGetValue(request.User, out user))
            {
                if (_cache.Refresh())
                {
                    continue;
                }

                throw new UserNotFoundException("User was not found, can't create room");
            }

            try
            {
                Player creator;
                Room room = user.CreateNewRoom(request.Level, new NoLimitHoldem(), out creator);
                Rooms.Add(room.GetHashCode(), room);
                Players.Add(creator.GetHashCode(), creator);
                result.Player = creator.GetHashCode();
                result.Room = room.GetHashCode();
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public EnterRoomResult EnterRoom(EnterRoomRequest request)
        {
            var result = new EnterRoomResult();

            try
            {
                Room room;
                while (!Rooms.TryGetValue(request.Room, out room))
                {
                    if (_cache.Refresh())
                    {
                        continue;
                    }

                    throw new RoomNotFoundException(string.Format("Requested room ID {0} not found", request.Room));
                }

                User user;
                while (!Users.TryGetValue(request.User, out user))
                {
                    if (_cache.Refresh())
                    {
                        continue;
                    }

                    throw new UserNotFoundException(string.Format("User ID {0} not found", request.User));
                }

                result.Player = user.EnterRoom(room).GetHashCode();
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, "At " + GetType().Name, e.Source);
            }

            return result;
        }

        public JoinNextHandResult JoinNextHand(JoinNextHandRequest request)
        {
            var result = new JoinNextHandResult();

            try
            {

                Player player;
                while (!Players.TryGetValue(request.Player, out player))
                {
                    if (_cache.Refresh())
                    {
                        continue;
                    }

                    throw new PlayerNotFoundException(string.Format("Cannot find player id: {0}, please exit and re-enter the room.", request.Player));
                }

                User user;
                while (!Users.TryGetValue(request.User, out user))
                {
                    if (_cache.Refresh()) { continue; }

                    throw new UserNotFoundException(string.Format("Cannot find user name: {0}, please login to the server again.", request.User));
                }

                user.JoinNextHand(player, request.seatIndex, request.buyIn);
                result.UserBank = user.UserBank.Money;

                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, "At " + GetType().Name, e.Source);
            }
            return result;
        }

        public StandUpToSpactateResult StandUpToSpactate(StandUpToSpactateRequest request)
        {
            var result = new StandUpToSpactateResult();

            try
            {
                var user = default(User);
                while (!Users.TryGetValue(request.User, out user))
                {
                    if (_cache.Refresh()) continue;

                    throw new UserNotFoundException(string.Format("cannot find user name: {0}, please login again.", request.User));
                }

                Player player = null;
                while (!Players.TryGetValue(request.Player, out player))
                {
                    if (_cache.Refresh()) continue;

                    throw new PlayerNotFoundException(string.Format("cannot find player id: {0}, please re-enter the room", request.Player));
                }

                result.RemainingMoney = user.StandUpToSpactate(player);
                result.UserBankMoney = user.UserBank.Money;

                //TODO - idan - check JSON of user statistics
                result.UserStatistics = user.UserStatistics;
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, "At " + GetType().Name, e.Source);
            }

            return result;
        }

        public LeaveRoomResult LeaveRoom(LeaveRoomRequest request)
        {
            var result = new LeaveRoomResult();

            try
            {
                var user = _cache.RefreshAndGet(
                    Users,
                    request.User,
                    new UserNotFoundException(string.Format("cannot find user name: {0}, please login again.", request.User))
                    );

                var player = _cache.RefreshAndGet(
                    Players,
                    request.Player,
                    new PlayerNotFoundException(string.Format("player id: {0} not found, please re-enter the room.", request.Player))
                    );

                user.ExitRoom(player);
                result.Success = true;
                result.UserStatistics = user.UserStatistics;

                // update cache on removal
                Players.Remove(player.GetHashCode());
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, "At " + GetType().Name, e.Source);
            }

            return result;
        }

        public FindRoomsByCriteriaResult FindRoomsByCriteria(FindRoomsByCriteriaRequest request)
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                // UNDONE - Idan
                var rooms = GameCenter.FindRoomsByCriteria();
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, "At " + GetType().Name, e.Source);
            }

            return result;
        }

        public FindRoomsByCriteriaResult GetAllRooms()
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                //Rooms.ToList().ForEach(room 
                GameCenter.
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, "At " + GetType().Name, e.Source);
            }

            return result;
        }


        #endregion

    }// class
}
