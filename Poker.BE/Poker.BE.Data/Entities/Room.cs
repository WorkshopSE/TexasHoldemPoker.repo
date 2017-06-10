using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data.Entities
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxActivePlayers { get; set; }
        public bool IsSpectatorsAllowed { get; set; }
        public double MinimumBet { get; set; }
        public double BuyInCost { get; set; }
        public virtual GamePreferences GamePreferences { get; set; }
        /// <summary>
        /// int: chair index location of 'Player'.
        /// </summary>
        public virtual IDictionary<int, Player> Players { get; set; }

    }
}
