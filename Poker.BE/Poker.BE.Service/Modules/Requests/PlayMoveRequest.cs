
namespace Poker.BE.Service.Modules.Requests
{
	public class PlayMoveRequest : IRequest
	{
        public string User { get; set; }
        public int Player { get; set; }
        public string PlayMove { get; set; }
        public double AmountOfMoney { get; set; }  //this is for raise\bet - otherwise its value should be 0
    }
}
