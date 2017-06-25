using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
	public class EditProfileRequest : IRequest
	{
		public string UserName { get; set; }
        public string NewUserName { get; set; }
		public string NewPassword { get; set; }
		public byte[] NewAvatar { get; set; }
	}
}
