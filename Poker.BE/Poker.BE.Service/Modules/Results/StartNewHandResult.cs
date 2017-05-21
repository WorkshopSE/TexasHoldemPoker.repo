using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
    public class StartNewHandResult : IResult
    {
        //Hand hash code
        public int Hand { get; set; }
    }
}
