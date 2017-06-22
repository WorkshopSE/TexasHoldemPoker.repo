using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public abstract class GamePreferences
    {
        #region Constants
        public const int MIN_NUMBER_OF_PLAYERS = 2;
        public const int MAX_NUMBER_OF_PLAYERS = 10;
        #endregion

        #region Fields
        private string _name;
        private double _buyInCost;
        private double _minimumBet;
        private double _antes;
        private int _minNumberOfPlayers;
        private int _maxNumberOfPlayers;
        private bool _isSpactatorsAllowed;
        #endregion

        #region Properties
        /// <summary>
        /// the room name - set by the user.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value ?? ""; }
        }

        /// <summary>
        /// choose the cost of joining the game.
        /// always higher than minimum bet
        /// </summary>
        public double BuyInCost
        {
            get { return _buyInCost; }

            // NOTE: adapt the set of buy in cost to the minimum bet
            set
            {
                _buyInCost = value;

                if (_buyInCost < MinimumBet)
                {
                    _minimumBet = _buyInCost;
                }
            }
        }

        /// <summary>
        /// The creator sets the big blind of the room’s table.
        /// always lower than buy in cost
        /// </summary>
        public double MinimumBet
        {
            get { return _minimumBet; }

            // Note: adapt the set of minimum bet to the buy in cost
            set
            {
                if (value > _buyInCost)
                {
                    _minimumBet = _buyInCost = value;
                }
                else
                {
                    _minimumBet = value;
                }
            }
        }

        /// <summary>
        /// The creator sets the antes of the room’s table.
        /// </summary>
        public double AntesValue
        {
            get { return _antes; }
            set { _antes = value; }
        }

        /// <summary>
        /// choose the min amount of active players in the room. (active + passive)
        /// </summary>
        public int MinNumberOfPlayers
        {
            get { return _minNumberOfPlayers; }
            set
            {
                //enforce min >= 2
                _minNumberOfPlayers = (value < 2) ? 2 : value;
            }
        }

        /// <summary>
        /// choose the min amount of active players in the room. (active + passive)
        /// </summary>
        public int MaxNumberOfPlayers
        {
            get { return _maxNumberOfPlayers; }
            set
            {
                _maxNumberOfPlayers = (value < MIN_NUMBER_OF_PLAYERS) ? MIN_NUMBER_OF_PLAYERS :
                                                                        ((value > MAX_NUMBER_OF_PLAYERS) ? MAX_NUMBER_OF_PLAYERS : value);
            }
        }

        /// <summary>
        /// choose whether spectating a game is allowed or not.
        /// </summary>
        public bool IsSpactatorsAllowed
        {
            get { return _isSpactatorsAllowed; }
            set
            {
                _isSpactatorsAllowed = value;

                // refresh the depended properties
                MaxNumberOfPlayers = _maxNumberOfPlayers;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// default configuration
        /// </summary>
        public GamePreferences()
        {
            Name = "Unknown Room";
            BuyInCost = 100.0;
            MinimumBet = 10.0;
            AntesValue = 0;
            MaxNumberOfPlayers = 10;
            MinNumberOfPlayers = 2;
            IsSpactatorsAllowed = true;

        }
        #endregion

        #region Methods
        public abstract void CheckBuyIn(double amountOfMoney);
        public abstract void CheckPlayers(int numOfPlayers);
        public abstract void CheckRaise(double raiseAmount);
        #endregion
    }
}
