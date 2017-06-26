using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
    public class GetStatisticsResult : IResult
    {
        public double WinRateStatistics { get; set; }
        public double GrossProfitWinRateStatistics { get; set; }
    }
}
