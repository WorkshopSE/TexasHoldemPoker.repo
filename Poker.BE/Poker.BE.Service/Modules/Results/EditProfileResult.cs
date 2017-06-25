using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
	public class EditProfileResult : IResult
	{
		public string newUserName { get; set; }
		public string newPassword { get; set; }
		public int[] newAvatar { get; set; }
	}
}
