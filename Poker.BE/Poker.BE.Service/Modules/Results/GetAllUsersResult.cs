namespace Poker.BE.Service.Modules.Results
{
    public class GetAllUsersResult : IResult
    {
        public class UserResult
        {
            public string UserName { get; set; }
            public int Level { get; set; }
            public int[] Avatar { get; set; }
            public double GrossProfits { get; set; }
            public double GrossLosses { get; set; }
            public double CashGain { get; set; }
            public int GamesPlayed { get; set; }
        }

        public UserResult[] Users { get; set; }
    }
}