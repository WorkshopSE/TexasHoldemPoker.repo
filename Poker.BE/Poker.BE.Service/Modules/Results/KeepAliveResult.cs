using System.Collections.Generic;

namespace Poker.BE.Service.Modules.Results
{
    public class KeepAliveResult : IResult
    {
        //Room's info
        public List<int> ActivePlayers { get; set; }
        public int[] TableLocationOfActivePlayers { get; set; }
        public string[] PlayersStates { get; set; } //by table location: passive means the seat is empty
        public bool IsTableFull { get; set; }

        //Hand's info
        /// <summary>
        /// this field is an array with size of 52, according to cards indexes.
        /// Each cell contains onr of the followings:
        /// 1. The player's hash code (the one that holds this card)
        /// 2. A negative number represent each of the table cards (if there are any): -1 for the first card, -2 for the second...
        /// 3. Zero, meanning this card is not in use (still in the deck)
        /// </summary>
        public int[] PlayersAndTableCards { get; set; }
        public int DealerId { get; set; }

        //Round's info
        public int CurrentPlayerID { get; set; }
        public List<double> PotsValues { get; set; }
        public List<double> PotsAmountToClaim { get; set; }
        public double[] PlayersBets { get; set; }   //by table location
        public double TotalRaise { get; set; }
        public double LastRaise { get; set; }

        //Player's info
        public double PlayerWallet { get; set; }
    }
}
