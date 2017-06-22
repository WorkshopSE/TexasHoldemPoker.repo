using System;
using System.Runtime.Serialization;

namespace Poker.BE.CrossUtility.Exceptions
{
    [Serializable]
    public class GameRulesException : Exception
    {
        public GameRulesException()
        {
        }

        public GameRulesException(string message) : base(message)
        {
        }

        public GameRulesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameRulesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}