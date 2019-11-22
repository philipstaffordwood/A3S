using System;
using System.Runtime.Serialization;

namespace za.co.grindrodbank.a3s.Exceptions
{
    [Serializable]
    public sealed class ArchivingException : Exception
    {
        private const string defaultMessage = "Archive processing error.";

        public ArchivingException() : base(defaultMessage)
        {
        }

        public ArchivingException(string message) : base(!string.IsNullOrEmpty(message) ? message : defaultMessage)
        {
        }

        public ArchivingException(string message, Exception innerException) : base(!string.IsNullOrEmpty(message) ? message : defaultMessage, innerException)
        {
        }

        private ArchivingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
