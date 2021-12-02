using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Exceptions
{
    [Serializable]
    class InvalidIdentificatorException : Exception
    {
        public InvalidIdentificatorException()
        {
        }

        public InvalidIdentificatorException(string message) : base(message)
        {
        }

        public InvalidIdentificatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidIdentificatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
