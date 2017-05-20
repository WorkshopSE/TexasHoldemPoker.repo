using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Exceptions
{
	[Serializable]
	class IncorrectPasswordException : PokerException
	{
		public IncorrectPasswordException()
		{
		}

		public IncorrectPasswordException(string message) : base(message)
		{
		}

		public IncorrectPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
