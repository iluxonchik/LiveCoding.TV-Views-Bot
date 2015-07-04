using System;
using System.Runtime.Serialization;

namespace LCTViewsBot
{
    [Serializable]
    internal class RTMPUrlNotFoundException : Exception
    {
        public RTMPUrlNotFoundException()
        {
        }

        public RTMPUrlNotFoundException(string message) : base(message)
        {
        }

        public RTMPUrlNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RTMPUrlNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}