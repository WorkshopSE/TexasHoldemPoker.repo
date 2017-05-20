using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
	public class SignUpRequest : IRequest
	{
		/// <summary>
		/// requesting user hash code
		/// </summary>
		public string UserName { get; set; }
		public string Password { get; set; }
		public double Deposit { get;  set; }
	}
}
