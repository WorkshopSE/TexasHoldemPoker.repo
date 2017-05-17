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
                throw new IOException("Last raise is lower then previous raise :(  Somthing isn't right...");
            this.currentPlayer.SubstractMoney(amount);
        }

        public void Fold()
        {
            this.currentPlayer.CurrentState = Player.State.ActiveFolded;
        }

        public void Bet(int amount)
        {
            if (amount <= 0)
                throw new IOException("Can't bet a negetive amount");
            this.currentPlayer.SubstractMoney(amount);
        }

        public void Raise(int amount)
        {
            if (amount <= 0)
                throw new IOException("Can't bet a negetive amount");
            this.currentPlayer.SubstractMoney(amount);
        }

        public void AllIn()
        {
            this.currentPlayer.CurrentState = Player.State.ActiveAllIn;
            //TODO - add money to pot
            this.currentPlayer.Wallet.amountOfMoney = 0;
        }
        #endregion
    }
}