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

        public Player CurrentPlayer { get { return currentPlayer; } set { currentPlayer = value; } }
        #endregion

        #region Constructors
        public Turn(Player player)
        {
            this.CurrentPlayer = player;
        }
        #endregion

        #region Methods
        public void Check()
        {
            ///  Do noting?
        }

        public void Call(int amount)
        {
            this.currentPlayer.SubstractMoney(amount);
        }

        public void Fold()
        {
            currentPlayer.CurrentState = Player.State.ActiveFolded;
        }

        public void Bet()
        {
            //TODO
        }

        public void Raise()
        {
            //TODO
        }

        public void AllIn()
        {
            //TODO
        }
        #endregion
    }
}