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
    }
}
