using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Utility.Exceptions;

namespace Poker.BE.Domain.Game
{
    public class Player
    {
        #region Constants
        public enum State
        {
            ActiveUnfolded,
            ActiveFolded,
            Passive
        }

        public static readonly int NPRIVATE_CARDS = 2;
        #endregion

        #region Properties
        public State CurrentState { get; set; }
        public Card[] PrivateCards { get; set; }
        public Wallet Wallet { get; }
        #endregion

        #region Constructors
        public Player()
        {
            PrivateCards = new Card[NPRIVATE_CARDS];
            CurrentState = State.Passive;
            Wallet = new Wallet(0);
        }
        #endregion

        #region Methods
        public void AddMoney(int amount)
        {
            this.Wallet.amountOfMoney += amount;
        }

        public void SubstractMoney(int amount)
        {
            if (Wallet.amountOfMoney < amount)
                throw new NotEnoughMoneyException("Player doesn't have enough money!");
            this.Wallet.amountOfMoney -= amount;
        }
        #endregion
    }
}
