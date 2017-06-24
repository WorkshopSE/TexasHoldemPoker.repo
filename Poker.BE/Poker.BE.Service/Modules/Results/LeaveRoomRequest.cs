namespace Poker.BE.Service.Modules.Results
{
    public class LeaveRoomRequest
    {
        /// <summary>
        /// user name unique identifier
        /// </summary>
        public string User { get; set; }
        
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