using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
	public class EditProfileRequest : IRequest
	{
		public string oldUserName { get; set; }
		public string newUserName { get; set; }
		public string newPassword { get; set; }
		public byte[] newAvatar { get; set; }
	}
}
