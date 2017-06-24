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


        public int Level { get; set; }
        public int Player { get; set; }

        // TODO check that preferences JOSN working OK
        public GamePreferences Preferences { get; set; }

        public double BetSize { get; set; }
    }
}