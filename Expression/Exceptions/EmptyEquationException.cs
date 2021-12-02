using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Exceptions
{
    [Serializable]
    class EmptyEquationException : Exception
    {
        public EmptyEquationException()
        {
        }

        public EmptyEquationException(string message) : base(message)
        {
        }

        public EmptyEquationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyEquationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
