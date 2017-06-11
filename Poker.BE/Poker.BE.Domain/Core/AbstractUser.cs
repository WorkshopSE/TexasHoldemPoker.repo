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
		
		public bool IsConnected { get; set; }

        public int AvatarID { get; set; }
		public byte[] AvatarImage { get; set; }

		#endregion
	}
}
