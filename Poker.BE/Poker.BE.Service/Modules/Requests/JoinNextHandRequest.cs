namespace Poker.BE.Service.Modules.Requests
{
	public class JoinNextHandRequest : IRequest
	{
		/// <summary>
		/// player hash code
		/// </summary>
		public int Player;
		public int seatIndex;
		public double buyIn;
	}
}