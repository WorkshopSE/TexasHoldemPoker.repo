using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Domain
{
	public class User
	{
		private string Name;
		private string UserName;
		private string Password;
		public int ID {  get;  }

		public User(string Name, string UserName, string Password)
		{
			this.Name = Name;
			this.UserName = UserName;
			this.Password = Password;
			this.ID = 0;
		}

		
	}
}
