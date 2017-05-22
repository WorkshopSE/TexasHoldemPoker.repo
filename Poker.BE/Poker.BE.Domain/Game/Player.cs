using Poker.BE.Domain.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Player
    {
        #region Constants
        public enum State
        {
            ActiveUnfolded,
            ActiveFolded,
            ActiveAllIn,
            Passive
        }
        public const int NPRIVATE_CARDS = 2;
        #endregion

        #region Fields
        private Wallet _wallet = default(Wallet);
        #endregion

        #region Properties
        public State CurrentState { get; set; }
        public Wallet Wallet { get { return _wallet; } }
        public double WalletValue { get { return _wallet.Value; } private set { _wallet.Value = value; } }
        public Card[] PrivateCards { get; set; }
        public string Nickname { get; set; }
        #endregion

        #region Constructors
        public Player()
        {
            PrivateCards = new Card[NPRIVATE_CARDS];
            CurrentState = State.Passive;
            _wallet = new Wallet();
            WalletValue = 0.0;
            Nickname = "";
        }

        #endregion

        #region Methods

        public bool JoinToTable(double buyIn)
        {
            if (CurrentState != State.Passive)
            {
                return false;
            }

            // buy in to wallet
            WalletValue = buyIn;

            CurrentState = State.ActiveUnfolded;
            return true;
        }
        
        /// <summary>
        /// Make the player to leave the table, and return his remaining wallet money to the user bank
        /// </summary>
        /// <returns>the remaining wallet money</returns>
        public double StandUp()
        {
            if (CurrentState == State.Passive)
            {
                throw new PlayerModeException("Unable to stand up: Player already a spectator.");
            }

            if (CurrentState == State.ActiveUnfolded)
            {
                throw new PlayerModeException("Unable to stand up: Player needs to fold first.");
            }

            CurrentState = State.Passive;
            return WalletValue;
        }

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

            var other = obj as Player;
            return 
                other != null 
                && CurrentState == other.CurrentState
                && Nickname.Equals(other.Nickname)
                //&& this.PrivateCards.Equals(other.PrivateCards) //TODO override card.equals
                && WalletValue.Equals(other.WalletValue)
                ;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public void AddMoney(int amount)
        {
            _wallet.AmountOfMoney += amount;
        }

        public void SubstractMoney(int amount)
        {
            if (_wallet.AmountOfMoney < amount)
                throw new NotEnoughMoneyException("Player doesn't have enough money!");
            _wallet.AmountOfMoney -= amount;
        }
        #endregion
    }
}
