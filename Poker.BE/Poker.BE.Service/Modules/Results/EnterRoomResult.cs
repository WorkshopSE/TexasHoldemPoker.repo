namespace Poker.BE.Service.Modules.Results
{
    public class EnterRoomResult : IResult
    {
        /// <summary>
        /// player hash code (session id)
        /// </summary>
        public int? Player { get; set; }
        public int RoomID { get; set; }

        public EnterRoomResult() : base()
        {
            Player = null;
            ErrorMessage = "";
            Success = true;
        }
    }
}