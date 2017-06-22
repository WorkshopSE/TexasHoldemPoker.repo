using Poker.BE.Domain.Game;

namespace Poker.BE.Service.Modules.Requests
{
	public class CreateNewRoomRequest : IRequest
	{
		public int Level { get; set; }
		public string User { get; set; }

        //Game Preferences data
        public string _name { get; set; }
        public double _buyInCost { get; set; }
        public double _minimumBet { get; set; }
        public double _antes { get; set; }
        public int _minNumberOfPlayers { get; set; }
        public int _maxNumberOfPlayers { get; set; }
        public bool _isSpactatorsAllowed { get; set; }

        public double limit { get; set; }       //this is for decorator (PotLimit/Limit game mode)
    }
}