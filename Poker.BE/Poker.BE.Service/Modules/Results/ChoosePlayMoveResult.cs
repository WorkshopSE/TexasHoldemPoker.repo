using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
    public class ChoosePlayMoveResult : IResult
    {
        //the total amount of money needed to call in order to claim the pot
        public int TotalRaise { get; set; }
    }
}
