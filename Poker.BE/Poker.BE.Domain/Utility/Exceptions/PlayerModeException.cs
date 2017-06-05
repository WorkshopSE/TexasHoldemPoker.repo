using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class PlayerModeException : PokerException
    {
        public PlayerModeException()
        {
        }

        public PlayerModeException(string message) : base(message)
        {
        }

        public PlayerModeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayerModeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}