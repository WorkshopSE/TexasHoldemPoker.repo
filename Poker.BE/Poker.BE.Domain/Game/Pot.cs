using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Pot
    {

		#region Fields
		private double PotValue;
		#endregion


		#region Properties
		public Pot PartialPot { get; set; }
		#endregion

		#region Constructors
        public Pot(){
            PotValue = 0;
        }
		#endregion

		#region Methods
        public void AddValue (double SumToAdd){
            PotValue = PotValue + SumToAdd;
        }

		public double GetValue()
		{
            return PotValue;
		}


		#endregion

	}
}
