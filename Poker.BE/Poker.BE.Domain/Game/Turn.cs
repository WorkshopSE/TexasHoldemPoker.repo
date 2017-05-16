using System;
using System.Collections.Generic;
using System.IO;
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
            if (amount <= 0)
                throw new IOException("Raise is lower then previous raise :(  Somthing isn't right...");
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