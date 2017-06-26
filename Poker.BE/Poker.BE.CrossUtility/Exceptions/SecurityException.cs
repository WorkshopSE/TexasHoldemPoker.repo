using System;
using System.Runtime.Serialization;

namespace Poker.BE.CrossUtility.Exceptions
{
    [Serializable]
    public class SecurityException : PokerException
    {
        public SecurityException()
        {
        }

        public SecurityException(string message) : base(message)
        {
        }

        public SecurityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}