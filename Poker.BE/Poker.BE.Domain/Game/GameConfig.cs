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
        public bool IsSpactatorsAllowed
        {
            get { return IsSpactatorsAllowed; }
            set
            {
                // when spectator not allowed, enforce it on max number of players
                if (!value & MaxNumberOfPlayers > MaxNumberOfActivePlayers)
                {
                    MaxNumberOfPlayers = MaxNumberOfActivePlayers;
                }
            }
        }

        /// <summary>
        /// choose the min amount of players in the room. (active + passive)
        /// </summary>
        public int MinNumberOfPlayers
        {
            get { return MinNumberOfPlayers; }
            // enforce min >= 2
            set { MinNumberOfPlayers = (value < 2) ? 2 : value; }
        }

        /// <summary>
        /// choose the min amount of players in the room. (active + passive)
        /// </summary>
        public int MaxNumberOfPlayers
        {
            get { return MaxNumberOfPlayers; }
            set
            {
                // enforce max number of players when spectators not allowed.
                MaxNumberOfPlayers =
                    (!IsSpactatorsAllowed & value > MaxNumberOfActivePlayers) ?
                    MaxNumberOfActivePlayers : value;
            }
        }

        /// <summary>
        /// choose the max amount of players in the table (active players)
        /// </summary>
        public int MaxNumberOfActivePlayers { get; set; }

        /// <summary>
        /// choose the cost of joining the game.
        /// always higher than minimum bet
        /// </summary>
        public double BuyInCost
        {
            get { return BuyInCost; }

            // NOTE: adapt the set of buy in cost to the minimum bet
            set
            {
                if (value < MinimumBet)
                {
                    MinimumBet = BuyInCost = value;
                }
                else
                {
                    BuyInCost = value;
                }
            }
        }

        /// <summary>
        /// The creator sets the big blind of the room’s table.
        /// always lower than buy in cost
        /// </summary>
        public double MinimumBet
        {
            get { return MinimumBet; }

            // Note: adapt the set of minimum bet to the buy in cost
            set
            {
                if (value > BuyInCost)
                {
                    MinimumBet = BuyInCost = value;
                }
                else
                {
                    MinimumBet = value;
                }
            }
        }

        /// <summary>
        /// the room name - set by the user.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// default configuration
        /// </summary>
        public GameConfig()
        {
            BuyInCost = 50.0;
            GamePrefrences = new GamePreferences();
            IsSpactatorsAllowed = true;
            MaxNumberOfActivePlayers = 10;
            MaxNumberOfPlayers = 15;
            MinNumberOfPlayers = 2;
            MinimumBet = 5.0;
            Name = "Unknown Room";
        }
        #endregion

    }
}