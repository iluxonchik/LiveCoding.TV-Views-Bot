using System;
using System.Runtime.Serialization;

namespace LCTViewsBot
{
    [Serializable]
    internal class RTMPAddressNotFoundException : Exception
    {
        public RTMPAddressNotFoundException()
        {
        }

        public RTMPAddressNotFoundException(string message) : base(message)
        {
        }

        public RTMPAddressNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RTMPAddressNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}