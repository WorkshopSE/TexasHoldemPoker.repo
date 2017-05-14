using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class GameConfig
    {

        #region Properties
        /// <summary>
        /// Choose between 3 game modes: “Limit Hold’em”, “No-Limit Hold’em” and “Pot Limit Hold’em”. when ‘Limit Hold’em’ is chosen the user is requested to enter a max bet limit.
        /// </summary>
        public GamePreferences GamePrefrences { get; set; }
        /// <summary>
        /// choose whether spectating a game is allowed or not.
        /// </summary>
        public bool IsSpactatorsAllowd { get; set; }
        /// <summary>
        /// choose the min amount of players in the table.
        /// </summary>
        public int MinNumberOfPlayers { get; set; }
        /// <summary>
        /// choose the min amount of players in the table.
        /// </summary>
        public int MaxNumberOfPlayers { get; set; }
        /// <summary>
        /// choose the cost of joining the game.
        /// </summary>
        public double BuyInCost { get; set;}
        /// <summary>
        /// The creator sets the big blind of the room’s table.
        /// </summary>
        public double MinimumBet { get; set; }

        #endregion
    }
}