namespace Poker.BE.Service.Modules.Results
{
    public class FindRoomsByCriteriaResult : IResult
    {
        /// <summary>
        /// Array of rooms id's.
        /// </summary>
        public RoomResult[] Rooms { get; set; }

        public class RoomResult
        {
            public int RoomID { get; set; }
            public string RoomName { get; set; }
            public int LeagueID { get; set; }
        }
    }
}