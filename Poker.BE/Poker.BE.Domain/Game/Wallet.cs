
namespace Poker.BE.Domain.Game
{
    public class Wallet
    {
        #region Properties
        public double AmountOfMoney { get; set; }
        #endregion

        #region Constructors
        public Wallet()
        {
            AmountOfMoney = 0.0;
        }
        public Wallet(double amount)
        {
            AmountOfMoney = amount;
        }
        #endregion

    }
}
