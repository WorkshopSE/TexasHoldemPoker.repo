using Poker.BE.Domain.Core;
using Poker.BE.Domain.Utility;

namespace Poker.BE.Service.Modules.Results
{
    public class StandUpToSpactateResult : IResult
    {
        public double RemainingMoney { get; set; }
        public double UserBankMoney { get; set; }
        public Statistics UserStatistics { get; set; }
    }
}