using Poker.BE.Domain.Game;

namespace Poker.BE.Service.Modules.Requests
{
    public class FindRoomsByCriteriaRequest : IRequest
    {
        /// <summary>
        /// this array sets the criteria the user would like to use to search.
        /// </summary>
        /// <remarks>
        ///     Author:
        ///         Idan Izicovich
        ///         - for more help on this service, ask me!
        /// </remarks>
        public string[] Criterias { get; set; }

        /* Optional Criteria(s) for Search */
        public const string BET_SIZE = "bet size";
        public const string LEVEL = "level";
        public const string PREFERENCES = "preferences";
        public const string PLAYER = "player";
        public const string ANTES_VALUE = "ante";
        public const string MAX_NUMBER_OF_PLAYERS = "max number of players";
        public const string BUY_IN_COST = "buy in cost";
        public const string NAME = "name";
        public const string MIN_BET = "minimum bet";
        public const string MIN_NUMBER_OF_PLAYERS = "min number of players";

        public int Level { get; set; }
        public int Player { get; set; }

        // TODO PotLimit : double
        public double BetSize { get; set; }
        public double MinimumBuyIn { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public int CurrentNumberOfPlayers { get; set; }
        public double Antes { get; set; }
        public double BuyInCost { get; set; }
        public string Name { get; set; }
        public double MinimumBet { get; set; }
        public int MinNumberOfPlayers { get; set; }
    }
}