using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class NotPlayersTurnException : PokerException
    {
        public NotPlayersTurnException()
        {
        }

        public NotPlayersTurnException(string message) : base(message)
        {
        }

        public NotPlayersTurnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotPlayersTurnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}