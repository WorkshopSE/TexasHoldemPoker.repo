using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class GameConfig
    {

        #region Fields
        private int _maxNumberOfActivePlayers;
        private int _maxNumberOfPlayers;
        private bool _isSpactatorsAllowed;
        private int _minNumberOfPlayers;
        private double _buyInCost;
        private double _minimumBet;
        private string _name;
        private GamePreferences _preferences;
        #endregion

        #region Properties

        /// <summary>
        /// Choose between 3 game modes: “Limit Hold’em”, “No-Limit Hold’em” and “Pot Limit Hold’em”. when ‘Limit Hold’em’ is chosen the user is requested to enter a max bet limit.
        /// </summary>
        public GamePreferences Preferences
        {
            get { return _preferences; }
            set { _preferences = value; }
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
                MaxNumberOfActivePlayers = _maxNumberOfActivePlayers;
            }
        }

        /// <summary>
        /// choose the min amount of players in the room. (active + passive)
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
        /// choose the min amount of players in the room. (active + passive)
        /// </summary>
        public int MaxNumberOfPlayers
        {
            get { return _maxNumberOfPlayers; }
            set
            {
                _maxNumberOfPlayers = (value < _maxNumberOfActivePlayers) ? _maxNumberOfActivePlayers : value;
                if (!_isSpactatorsAllowed)
                {
                    _maxNumberOfActivePlayers = _maxNumberOfPlayers;
                }
            }
        }

        /// <summary>
        /// choose the max amount of players in the table (active players)
        /// </summary>
        public int MaxNumberOfActivePlayers
        {
            get { return _maxNumberOfActivePlayers; }
            set
            {
                // enforce number of active players < number of chairs.
                _maxNumberOfActivePlayers = (value > Room.NCHAIRS_IN_ROOM) ? Room.NCHAIRS_IN_ROOM : value;

                // enforce max number of players when spectators not allowed.
                _maxNumberOfPlayers =
                    (!IsSpactatorsAllowed & value > MaxNumberOfActivePlayers) ?
                    MaxNumberOfActivePlayers : value;
            }

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
        /// the room name - set by the user.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value ?? ""; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// default configuration
        /// </summary>
        public GameConfig()
        {
            BuyInCost = 60.0;
            Preferences = new GamePreferences();
            IsSpactatorsAllowed = true;
            MaxNumberOfActivePlayers = 10;
            MaxNumberOfPlayers = 15;
            MinNumberOfPlayers = 2;
            MinimumBet = 5.0;
            Name = "Unknown Room";
        }
        #endregion

        #region Methods
        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            var other = obj as GameConfig;
            if (other == null) return false;

            return
                //this.GamePreferences.Equals(other.GamePreferences) && // undone: override GamePreferences.Equals(:object)
                this.BuyInCost.Equals(other.BuyInCost) &&
                this.IsSpactatorsAllowed == other.IsSpactatorsAllowed &&
                this.MaxNumberOfActivePlayers == other.MaxNumberOfActivePlayers &&
                this.MaxNumberOfPlayers == other.MaxNumberOfPlayers &&
                this.MinimumBet == other.MinimumBet &&
                this.Name.Equals(other.Name) &&
                this.MinNumberOfPlayers == other.MinNumberOfPlayers;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}