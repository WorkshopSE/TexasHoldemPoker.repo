using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Exceptions
{
	[Serializable]
	public class InvalidDepositException : PokerException
	{
		public InvalidDepositException()
		{
		}

		public InvalidDepositException(string message) : base(message)
		{
		}

		public InvalidDepositException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidDepositException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
