using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
	public class GetProfileResult : IResult
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public byte[] Avatar { get; set; }
	}
}
