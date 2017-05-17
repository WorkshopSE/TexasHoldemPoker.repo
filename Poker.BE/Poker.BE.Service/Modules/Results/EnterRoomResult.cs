namespace Poker.BE.Service.Modules.Results
{
    public class EnterRoomResult : IResult
    {
        /// <summary>
        /// player hash code (session id)
        /// </summary>
        public int player { get; set; }
    }
}