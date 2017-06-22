using System;
using System.Runtime.Serialization;

namespace Poker.BE.CrossUtility.Exceptions
{
    [Serializable]
    public class NotEnoughMoneyException : PokerException
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