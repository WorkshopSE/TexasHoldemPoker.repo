using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class LevelNotFoundException : PokerException
    {
        public LevelNotFoundException()
        {
        }

        public LevelNotFoundException(string message) : base(message)
        {
        }

        public LevelNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LevelNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}