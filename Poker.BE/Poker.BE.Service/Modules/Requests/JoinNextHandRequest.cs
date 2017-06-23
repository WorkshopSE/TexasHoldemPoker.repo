namespace Poker.BE.Service.Modules.Requests
{
    public class JoinNextHandRequest : IRequest
    {
        /// <summary>
 		/// Player Hash Code
 		/// </summary>
 		public int Player { get; set; }
        /// <summary>
        /// User name unique identifier
        /// </summary>
        public string User { get; set; }

        public int seatIndex { get; set; }
        public double buyIn { get; set; }
    }
}