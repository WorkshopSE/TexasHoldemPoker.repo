using Poker.BE.Domain.Game;

namespace Poker.BE.Service.Modules.Requests
{
	public class CreateNewRoomRequest : IRequest
	{
		public int Level { get; set; }

        //Game Preferences data
        public string Name { get; set; }
        public double BuyInCost { get; set; }
        public double MinimumBet { get; set; }
        public double Antes { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public bool IsSpactatorsAllowed { get; set; }

        public double Limit { get; set; }       //this is for decorator (PotLimit/Limit game mode)
    }
}