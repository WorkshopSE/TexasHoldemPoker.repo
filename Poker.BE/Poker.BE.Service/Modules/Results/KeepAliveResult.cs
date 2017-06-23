using System.Collections.Generic;

namespace Poker.BE.Service.Modules.Results
{
    public class KeepAliveResult : IResult
    {
        //Room's info
        public List<int> ActivePlayers { get; set; }
        public int[] TableLocationOfActivePlayers { get; set; }
        public bool IsTableFull { get; set; }

        //Hand's info
        public int[] PlayersAndTableCards { get; set; }
        public int DealerId { get; set; }


        //Round's info
        public string[] PlayersStates { get; set; } //by table location: passive means the seat is empty
        public int CurrentPlayerID { get; set; }
        public List<int> PotsValues { get; set; }
        public List<int> PotsAmountToClaim { get; set; }
        public int[] PlayersBets { get; set; }   //by table location
        public int TotalRaise { get; set; }
        public int LastRaise { get; set; }
    }
}
