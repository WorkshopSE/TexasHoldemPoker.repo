using System;
using System.Runtime.Serialization;

namespace Poker.BE.CrossUtility.Exceptions
{
    [Serializable]
    public class WrongIOException : PokerException
    {
        public WrongIOException()
        {
        }

        public WrongIOException(string message) : base(message)
        {
        }

        public WrongIOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongIOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}