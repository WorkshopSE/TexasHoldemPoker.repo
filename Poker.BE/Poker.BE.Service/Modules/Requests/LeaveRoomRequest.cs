namespace Poker.BE.Service.Modules.Requests
{
    public class LeaveRoomRequest : IRequest
    {
        
        /// <summary>
        /// player hash code
        /// </summary>
        public int Player { get; set; }

        /// <summary>
        /// Room hash code - ? no need.
        /// </summary>
        public int Room { get; internal set; }
    }
}