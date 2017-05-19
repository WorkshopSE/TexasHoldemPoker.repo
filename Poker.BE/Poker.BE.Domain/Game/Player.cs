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

		#region Fields
        public Wallet PlayerWallet;
		#endregion


		public const int NPRIVATE_CARDS = 2;
        #endregion

        #region Properties
        public State CurrentState { get; set; }
        public Card[] PrivateCards { get; set; }
        #endregion

        #region Constructors
        public Player(double SumToDeposit)
        {
            PlayerWallet = new Wallet(SumToDeposit);
            PrivateCards = new Card[NPRIVATE_CARDS];
            CurrentState = State.Passive;
        }
		#endregion

		#region Methods
        public void ExitPlayer(){
            // we need to put back the Player Wallet remaining to the user Bank back.
        }

        public bool Pay (double SumToPay){
            if(PlayerWallet.CanWithdraw(SumToPay)){
                PlayerWallet.Withdraw(SumToPay);
                return true;
            }
            return false;
        }
        #endregion



	}
}
