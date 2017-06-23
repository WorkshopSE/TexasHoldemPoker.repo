using System;
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
        public UserManager UserManager { get { return UserManager.Instance; } }
        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
        #endregion

        #region Constructors
        public RoomsService()
        {
            _cache = CommonCache.Instance;
        }
        #endregion

        #region Methods
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

                // UNDONE - Idan - continue from here.
                user.JoinNextHand(player, request.seatIndex, request.buyIn);
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
            // TODO
            throw new NotImplementedException();
        }
        public void Clear()
        {
            _cache.Clear();
        }
        #endregion

    }// class
}
