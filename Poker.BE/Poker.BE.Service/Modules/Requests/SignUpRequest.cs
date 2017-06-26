using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
	/// <summary>
	/// sign up UC inputs
	/// </summary>
	public class SignUpRequest : IRequest
	{
		public string Password { get; set; }
		public double Deposit { get;  set; }
	}
}
