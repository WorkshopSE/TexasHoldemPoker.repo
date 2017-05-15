using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Core
{
    public class Bank : Utility.MoneyStorage
    {
        // UNDONE: make this class align with extending MoneyStorage Class.

        #region Properties
        protected double Money { get; set;}

        #endregion

        #region Methods
        public Bank (double sumToDeposit){
            Deposit (sumToDeposit);
        }

        protected bool CanWithdraw (double sum){
            return (sum <  Money) ; 
        }

        protected bool Withdraw (double sum){
            if (CanWithdraw (sum)){
                Money = Money - sum;
                return true;
            }
            return false;
        }

        protected void Deposit (double sum){
            Money = Money + sum;
        }



        #endregion

    }

    
    

}
