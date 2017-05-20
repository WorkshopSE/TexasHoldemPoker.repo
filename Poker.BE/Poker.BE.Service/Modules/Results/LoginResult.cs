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
		/// user hash code 
		/// </summary>
		public int User { get; set; }
	}
}
