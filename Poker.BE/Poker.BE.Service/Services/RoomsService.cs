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

namespace Poker.BE.Service.Services
{
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
        #endregion


        public CreateNewRoomResult CreateNewRoom(CreateNewRoomRequest request)
        {
            // TODO
            throw new NotImplementedException();
        }

        public EnterRoomResult EnterRoom(EnterRoomRequest request)
        {
            var result = default(EnterRoomResult);

            if (!Rooms.TryGetValue(request.Room, out Room room))
            {
                throw new RoomNotFoundException(string.Format("Requested room ID {0} not found", request.Room));
            }

            if(!Users.TryGetValue(request.User, out User user))
            {
                throw new UserNotFoundException();
            }

            result = new EnterRoomResult()
            {
                player = request.User
            };


            // UNDONE - idan - continue from here 18/5

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
    }
}
