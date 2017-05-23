using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Exceptions
{
    public abstract class PokerException : Exception
    {
        public PokerException()
        {
        }

        public PokerException(string message) : base(message)
        {
        }

        public PokerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PokerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
