using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Exceptions
{
	[Serializable]
	public class UserNameTakenException : PokerException
	{
		public UserNameTakenException()
		{
		}

		public UserNameTakenException(string message) : base(message)
        {
		}

		public UserNameTakenException(string message, Exception innerException) : base(message, innerException)
        {
		}

		protected UserNameTakenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
		}
	}
}
