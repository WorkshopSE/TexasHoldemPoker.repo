
namespace Poker.BE.Domain.Core
{
    public class Bank : Utility.MoneyStorage
    {
        // UNDONE: make this class align with extending MoneyStorage Class.

        #region Properties
        public double Money { get; set; }
        #endregion

        #region Constructors
        public Bank()
        {
            Money = 0.0;
        }

        public Bank(double sumToDeposit)
        {
            // TODO: Ariel / Tomer - test sumToDeposit > 0
            Deposit(sumToDeposit);
        }
        #endregion

        #region Methods
        public bool Withdraw(double sum)
        {
            if (CanWithdraw(sum))
            {
                Money = Money - sum;
                return true;
            }
            return false;
        }

        public void Deposit(double sum)
        {
            Money = Money + sum;
        }
        #endregion

        #region Protected Functions
        protected bool CanWithdraw(double sum)
        {
            return (sum < Money);
        }
        #endregion

    }




}
