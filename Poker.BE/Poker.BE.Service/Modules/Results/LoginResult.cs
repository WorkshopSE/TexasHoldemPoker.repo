using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
	public class LoginResult : IResult
	{
		/// <summary>
		/// username 
		/// </summary>
		public string User { get; set; }
        public int Level { get; set; }
        public double UserBank { get; set; }
	}
}
