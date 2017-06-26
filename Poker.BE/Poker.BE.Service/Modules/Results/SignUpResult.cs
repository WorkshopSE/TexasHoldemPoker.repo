using Poker.BE.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
	public class SignUpResult : IResult
	{
		/// <summary>
		/// user name
		/// </summary>
		public string UserName { get; set; }
	}
}
