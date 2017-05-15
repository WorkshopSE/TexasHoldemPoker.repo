using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class RoomRulesException : Exception
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