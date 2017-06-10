using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data.Entities
{
    public class LeagueEntity
    {
        public int ID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
    }
}
