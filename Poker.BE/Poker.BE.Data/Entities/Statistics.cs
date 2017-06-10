using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data.Entities
{
    public class Statistics
    {
        public int GamesPlayed { get; set; }
        public double GrossProfits { get; set; }
        public double GrossLosses { get; set; }
    }
}
