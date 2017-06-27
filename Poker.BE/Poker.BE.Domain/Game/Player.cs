using Poker.BE.Domain.Utility;
using Poker.BE.CrossUtility.Exceptions;
using System;
using System.Threading;

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
        private Round.Move _playMove;
        #endregion

        #region Properties
        public State CurrentState { get; set; }
        public Wallet Wallet { get { return _wallet; } }
        public Card[] PrivateCards { get; set; }
        public string Nickname { get; set; }
        public double AmountToBetOrCall { get; private set; }
        public Statistics PlayerStatistics { get; set; }
        public object Lock { get; set; }

        public Round.Move PlayMove
        {
            get { return _playMove; }
            private set
            {
                _playMove = value;
                Monitor.Enter(Lock);
                Monitor.PulseAll(Lock);
                Monitor.Exit(Lock);
            }
        }
        #endregion

        #region Constructors
        public Player()
        {
            PrivateCards = new Card[NPRIVATE_CARDS];
            Nickname = "";
            CurrentState = State.Passive;
            _wallet = new Wallet();
            PlayerStatistics = new Statistics();
            _playMove = Round.Move.Null;
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
                throw new PlayerModeException("Player is not a spectator");
            }

            // buy in to wallet
            _wallet.AmountOfMoney = buyIn;

            CurrentState = State.ActiveFolded;
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

            if (CurrentState != State.ActiveFolded)
            {
                throw new PlayerModeException("Unable to stand up: Player needs to be folded.");
            }

            CurrentState = State.Passive;
            return _wallet.AmountOfMoney;
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
                && _wallet.AmountOfMoney.Equals(other._wallet.AmountOfMoney)
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
