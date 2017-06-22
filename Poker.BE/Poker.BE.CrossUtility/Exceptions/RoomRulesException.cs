using System;
using System.Runtime.Serialization;

namespace Poker.BE.CrossUtility.Exceptions
{
    [Serializable]
    public class RoomRulesException : PokerException
    {
        public RoomRulesException()
        {
        }

        public RoomRulesException(string message) : base(message)
        {
        }

        public RoomRulesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RoomRulesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}