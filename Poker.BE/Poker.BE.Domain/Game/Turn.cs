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
        private Pot currentPot;
        #endregion

        #region Properties
        public Player CurrentPlayer { get { return currentPlayer; } set { currentPlayer = value; } }
        public Pot CurrentPot { get { return currentPot; } set { currentPot = value; } }
        #endregion

        #region Constructors
        public Turn(Player player, Pot pot)
        {
            this.CurrentPlayer = player;
            this.currentPot = pot;
        }
        #endregion

        #region Methods
        public void Check()
        {
            ///  Do noting?
        }

        public void Call(double amount)
        {
            if (amount <= 0)
                throw new IOException("negative call :(  Somthing isn't right...");
            this.currentPlayer.SubstractMoney(amount);
        }

        public void Fold()
        {
            this.currentPlayer.CurrentState = Player.State.ActiveFolded;
        }

        public void Bet(double amount)
        {
            if (amount <= 0)
                throw new IOException("Can't bet a negetive amount");
            this.currentPlayer.SubstractMoney(amount);
        }

        public void Raise(double amount)
        {
            if (amount <= 0)
                throw new IOException("Can't bet a negetive amount");
            this.currentPlayer.SubstractMoney(amount);
        }

        public void AllIn()
        {
            this.currentPlayer.CurrentState = Player.State.ActiveAllIn;
        }
        #endregion
    }
}