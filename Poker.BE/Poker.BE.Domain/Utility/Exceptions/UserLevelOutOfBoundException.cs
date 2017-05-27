using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Exceptions
{
	[Serializable]
	class UserLevelOutOfBoundException : PokerException
	{
		public UserLevelOutOfBoundException()
		{
		}

		public UserLevelOutOfBoundException(string message) : base(message)
        {
		}

		public UserLevelOutOfBoundException(string message, Exception innerException) : base(message, innerException)
        {
		}

		protected UserLevelOutOfBoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
		}
	}
}
