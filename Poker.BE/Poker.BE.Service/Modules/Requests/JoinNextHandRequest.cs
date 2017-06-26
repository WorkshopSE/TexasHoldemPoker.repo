namespace Poker.BE.Service.Modules.Requests
{
    public class JoinNextHandRequest : IRequest
    {
        /// <summary>
 		/// Player Hash Code
 		/// </summary>
 		public int Player { get; set; }
        public int seatIndex { get; set; }
        public double buyIn { get; set; }
    }
}