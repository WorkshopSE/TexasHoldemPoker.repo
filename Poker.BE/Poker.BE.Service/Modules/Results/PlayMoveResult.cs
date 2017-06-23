
namespace Poker.BE.Service.Modules.Results
{
	public class PlayMoveResult : IResult
	{
        public int NextPlayer { get; set; }
        public double TotalRaise { get; set; }
        public double LastRaise { get; set; }       //this is for making sure the player doesn't raise more than last raise made

        //Note: this is the amount of money the player has invested until now.
        //      for calculating the amount of money he needs to call:
        //      TotalRaise - NextPlayerInvest = CallAmount
        public double NextPlayerInvest { get; set; }
    }
}
