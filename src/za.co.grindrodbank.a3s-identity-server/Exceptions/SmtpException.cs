/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
using System;
using System.Runtime.Serialization;

namespace za.co.grindrodbank.a3sidentityserver.Exceptions
{
    [Serializable]
    public sealed class SmtpException : Exception
    {
        private const string defaultMessage = "General SMTP error.";

        public SmtpException() : base(defaultMessage)
        {
        }

        public SmtpException(string message) : base(!string.IsNullOrEmpty(message) ? message : defaultMessage)
        {
        }

        public SmtpException(string message, Exception innerException) : base(!string.IsNullOrEmpty(message) ? message : defaultMessage, innerException)
        {
        }

        private SmtpException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
