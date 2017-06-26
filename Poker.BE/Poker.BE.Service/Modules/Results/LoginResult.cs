using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
	public class LoginResult : IResult
	{
		/// <summary>
		/// User Name 
		/// </summary>
		public string UserName { get; set; }

        /// <summary>
        /// random generated security key - for the front-end to keep
        /// </summary>
        public int SecurityKey { get; set; }

        public double UserBank { get; set; }
        public int Level { get; set; }
    }
}
