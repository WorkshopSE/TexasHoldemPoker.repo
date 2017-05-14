using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Turn
    {
        #region Fields
        private Player currentPlayer;
        #endregion

        #region Constructors
        public Turn(Player player)
        {
            this.currentPlayer = player;
        }
        #endregion

        #region Methods
        public void Check()
        {
            //TODO
        }

        public void Call()
        {
            //TODO
        }

        public void Fold(Player player)
        {
            //TODO
        }

        public void Bet(Player player)
        {
            //TODO
        }

        public void Raise(Player player)
        {
            //TODO
        }

        public void AllIn(Player player)
        {
            //TODO
        }
        #endregion
    }
}
