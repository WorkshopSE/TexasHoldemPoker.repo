using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Utility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public class Turn
    {
        #region Fields
        private Player currentPlayer;
        #endregion

        #region Properties
        /// <summary>
        /// The set of this property raising an event
        /// </summary>
        public Player CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                currentPlayer = value;
                currentPlayer.OnTurnChanged(EventArgs.Empty);
            }
        }
        #endregion

        #region Constructors
        public Turn(Player player)
        {
            this.CurrentPlayer = player;
        }
        #endregion

        #region Methods
        #endregion
    }
}