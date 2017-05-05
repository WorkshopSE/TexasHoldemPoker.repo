using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AT.Domain
{
	public class User
	{
		private string Name;
		private string UserName;
		public string Password { get; }
		public string Email { get; }
		public int ID {  get;  }
		public Image Avatar { get; set; }

		public User(string Name, string UserName, string Password)
		{
			this.Name = Name;
			this.UserName = UserName;
			this.Password = Password;
			this.ID = 0;
		}

		
	}
}
