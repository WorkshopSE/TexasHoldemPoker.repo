using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
        {
        }

        public NotEnoughMoneyException(string message) : base(message)
        {
        }

        public NotEnoughMoneyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughMoneyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}