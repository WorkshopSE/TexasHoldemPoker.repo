using Poker.BE.Domain.Utility;

namespace Poker.BE.Service.Modules.Results
{
    public class LeaveRoomResult : IResult
    {
        public Statistics UserStatistics { get; set; }
    }
}