using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Exceptions
{
    [Serializable]
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException()
        {
        }

        public InvalidTokenException(string message) 
            : base(message)
        {
        }

        public InvalidTokenException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail.
        protected InvalidTokenException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
