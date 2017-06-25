namespace Poker.BE.Service.Modules.Results
{
    public class LeaguesResult : IResult
    {
        public class League
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int MinLevel { get; set; }
            public int MaxLevel { get; set; }
            public bool IsFull { get; set; }
        }

        public League[] Leagues { get; set; }
        public int RelevantID { get; set; }
    }
}