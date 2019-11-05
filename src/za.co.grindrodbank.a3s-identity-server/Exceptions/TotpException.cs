/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Runtime.Serialization;

namespace za.co.grindrodbank.a3sidentityserver.Exceptions
{
    [Serializable]
    public sealed class TotpException : Exception
    {
        private const string defaultMessage = "Invalid TOTP token.";

        public TotpException() : base(defaultMessage)
        {
        }

        public TotpException(string message) : base(!string.IsNullOrEmpty(message) ? message : defaultMessage)
        {
        }

        public TotpException(string message, Exception innerException) : base(!string.IsNullOrEmpty(message) ? message : defaultMessage, innerException)
        {
        }

        private TotpException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
