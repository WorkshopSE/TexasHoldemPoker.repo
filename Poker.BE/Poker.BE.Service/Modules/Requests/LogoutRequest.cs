﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
	public class LogoutRequest : IRequest
	{
		/// <summary>
		/// requested user hash code
		/// </summary>
		public int User { get; set; }
	}
}
