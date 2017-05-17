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
            Passive
        }
        public const int NPRIVATE_CARDS = 2;
        #endregion

        #region Fields
        private Wallet _wallet = default(Wallet);
        #endregion

        #region Properties
        public State CurrentState { get; protected set; }
        public double Wallet { get { return _wallet.Value; } private set { _wallet.Value = value; } }
        public Card[] PrivateCards { get; set; }
        public string Nickname { get; set; }
        #endregion

        #region Constructors
        public Player()
        {
            PrivateCards = new Card[NPRIVATE_CARDS];
            CurrentState = State.Passive;
            _wallet = new Wallet();
            Wallet = 0.0;
        }

        public bool JoinToTable(double buyIn)
        {
            if (CurrentState != State.Passive)
            {
                return false;
            }

            // buy in to wallet
            Wallet = buyIn;

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
            return Wallet;
        }

        #endregion

    }// class
}
