using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Wallet
    {
		#region Properties
		public double Money { get; set; }

		#endregion

		#region Constructors
		public Wallet(double sumToDeposit)
		{
			Deposit(sumToDeposit);
		}
		#endregion

		#region Methods
		public bool CanWithdraw(double sum)
		{
			return (sum < Money);
		}

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


	}
}
