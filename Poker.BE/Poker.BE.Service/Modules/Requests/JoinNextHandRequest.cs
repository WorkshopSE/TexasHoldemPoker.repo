namespace Poker.BE.Service.Modules.Requests
{
    public class JoinNextHandRequest : IRequest
    {
        /// <summary>
 		/// Player Hash Code
 		/// </summary>
 		public int Player { get; set; }
        public int SeatIndex { get; set; }
        public double BuyIn { get; set; }
    }
}