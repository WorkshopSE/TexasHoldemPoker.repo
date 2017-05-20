﻿using System;
using System.Runtime.Serialization;

namespace Poker.BE.Domain.Utility.Exceptions
{
    [Serializable]
    public class WrongIOException : Exception
    {
        public WrongIOException()
        {
        }

        public WrongIOException(string message) : base(message)
        {
        }

        public WrongIOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongIOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}