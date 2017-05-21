using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Exceptions;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Utility.Logger;

namespace Poker.BE.Service.Services
{
    /// <summary>
    /// UCC03 Rooms Management
    /// </summary>
    /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.286w5j2ewu5c"/>
    public class RoomsService : IServices.IRoomsService
    {
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
        public IDictionary<int, Player> Players { get; set; }
        public IDictionary<int, Room> Rooms { get; set; }
        public IDictionary<int, User> Users { get; set; }
        public ILogger Logger { get; }
        #endregion

        #region Constructors
        public RoomsService()
        {
            Players = new Dictionary<int, Player>();
            Rooms = new Dictionary<int, Room>();
            Users = new Dictionary<int, User>();
            Logger = Domain.Utility.Logger.Logger.Instance;
        }
        #endregion

        #region Methods
        public CreateNewRoomResult CreateNewRoom(CreateNewRoomRequest request)
        {
            // TODO
            throw new NotImplementedException();
        }

        public EnterRoomResult EnterRoom(EnterRoomRequest request)
        {
            var result = new EnterRoomResult();

            try
            {
                Room room;
                if (!Rooms.TryGetValue(request.Room, out room))
                {
                    throw new RoomNotFoundException(string.Format("Requested room ID {0} not found", request.Room));
                }

                User user;
                if (!Users.TryGetValue(request.User, out user))
                {
                    throw new UserNotFoundException(string.Format("User ID {0} not found", request.User));
                }

                result.Player = user.EnterRoom(room).GetHashCode();
            }
            catch (RoomNotFoundException e)
            {
                // TODO
                throw e;
            }
            catch (UserNotFoundException e)
            {
                // TODO
                throw e;
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
            // TODO
            throw new NotImplementedException();
        }

        public StandUpToSpactateResult StandUpToSpactate(StandUpToSpactateRequest request)
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

    }// class
}
