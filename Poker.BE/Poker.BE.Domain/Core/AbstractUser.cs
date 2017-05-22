using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Core
{
	public abstract class AbstractUser
	{

		#region Properties
		public string UserName { get; set; }
		public string Password { get; set; }
		public Bank UserBank { get; set; }
		public bool IsConnected { get; set; }
		#endregion
	}
}
