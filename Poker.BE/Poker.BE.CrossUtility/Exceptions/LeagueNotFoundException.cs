using System;
using System.Runtime.Serialization;

namespace Poker.BE.CrossUtility.Exceptions
{
    [Serializable]
    public class LeagueNotFoundException : PokerException
    {
        public LeagueNotFoundException()
        {
        }

        public LeagueNotFoundException(string message) : base(message)
        {
        }

        public LeagueNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LeagueNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}