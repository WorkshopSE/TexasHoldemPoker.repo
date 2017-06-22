using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.CrossUtility.Exceptions
{
	[Serializable]
	public class InvalidPasswordException : PokerException
	{
		
		public InvalidPasswordException()
		{
		}

		public InvalidPasswordException(string message) : base(message)
		{
		}

		public InvalidPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
