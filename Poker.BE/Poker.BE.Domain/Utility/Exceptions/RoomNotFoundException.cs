using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class RoomNotFoundException : PokerException
    {
        public RoomNotFoundException()
        {
        }

        public RoomNotFoundException(string message) : base(message)
        {
        }

        public RoomNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RoomNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}