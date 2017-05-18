namespace Poker.BE.Service.Modules.Results
{
    public class EnterRoomResult : IResult
    {
        /// <summary>
        /// player hash code (session id)
        /// </summary>
        public int? Player { get; set; }

        public EnterRoomResult()
        {
            Player = null;
            ErrorMessage = "";
            Success = true;
        }
    }
}