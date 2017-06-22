using Poker.BE.Domain.Utility;
using Poker.BE.CrossUtility.Exceptions;
using System;

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
        public event EventHandler TurnChanged;
        #endregion

        #region Properties
        public State CurrentState { get; set; }
        public Wallet Wallet { get { return _wallet; } }
        public double WalletValue { get { return _wallet.Value; } private set { _wallet.Value = value; } }
        public Card[] PrivateCards { get; set; }
        public string Nickname { get; set; }
        public Round.Move PlayMove { get; private set; }
        public double AmountToBetOrCall { get; private set; }
        public Statistics PlayerStatistics { get; set; }
        #endregion

        #region Constructors
        public Player()
        {
            PrivateCards = new Card[NPRIVATE_CARDS];
            Nickname = "";
            CurrentState = State.Passive;
            _wallet = new Wallet();
            WalletValue = 0.0;
            PlayerStatistics = new Statistics();
            PlayMove = default(Round.Move);
        }

        public Player(string nickname) : this()
        {
            Nickname = nickname;
        }
        #endregion

        #region Methods

        public void ChoosePlayMove(string playMove, double amountToBetOrCall)
        {
            Round.Move parsedMove;
            Enum.TryParse(playMove, out parsedMove);
            PlayMove = parsedMove;
            AmountToBetOrCall = amountToBetOrCall;
        }

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

        public void AddStatistics(double amountOfMoney)
        {
            PlayerStatistics.AddHandStatistic(amountOfMoney);
        }

        public void Fold()
        {
            CurrentState = State.ActiveFolded;
        }

        public void AllIn()
        {
            CurrentState = Player.State.ActiveAllIn;
        }

        public void AddMoney(double amount)
        {
            if (amount < 0)
                throw new WrongIOException("Can't add a negative amount of money");
            _wallet.AmountOfMoney += amount;
        }

        public void SubstractMoney(double amount)
        {
            if (_wallet.AmountOfMoney < amount)
                throw new NotEnoughMoneyException("Player doesn't have enough money!");
            _wallet.AmountOfMoney -= amount;
        }

        public virtual void OnTurnChanged(EventArgs e)
        {
            //Note: syntactic sugar for checking if handler is null
            TurnChanged?.Invoke(this, e);
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

            // Note: when nickname == "" -> shallow compare.
            if (Nickname.Equals("") && other.Nickname.Equals(""))
            {
                return this == other;
            }

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
        #endregion
    }
}
