using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Exceptions
{
	[Serializable]
	class HandNotOverException : PokerException
	{
		public HandNotOverException()
		{
		}

		public HandNotOverException(string message) : base(message)
        {
		}

		public HandNotOverException(string message, Exception innerException) : base(message, innerException)
        {
		}

		protected HandNotOverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
		}
	}
}
