namespace Poker.BE.Service.Modules.Requests
{
    public class JoinNextHandRequest : IRequest
    {
        /// <summary>
 		/// Player Hash Code
 		/// </summary>
 		public int Player;
        public int seatIndex;
        public double buyIn;
    }
}