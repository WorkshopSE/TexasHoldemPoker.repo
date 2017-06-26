namespace Poker.BE.Service.Modules.Results
{
    public class EnterRoomResult : IResult
    {
        /// <summary>
        /// player hash code (session id)
        /// </summary>
        public int? Player { get; set; }
        public int RoomID { get; set; }

        //Game Preferences data
        public string Name { get; set; }
        public double BuyInCost { get; set; }
        public double MinimumBet { get; set; }
        public double Antes { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public bool IsSpactatorsAllowed { get; set; }

        public double Limit { get; set; }       //this is for decorator (PotLimit/Limit game mode)


        public EnterRoomResult() : base()
        {
            Player = null;
            ErrorMessage = "";
            Success = true;
        }
    }
}