using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Exceptions
{
    public class PayloadValidationException : Exception
    {
        public PayloadValidationException(string message)
            : base(message)
        { }

        public PayloadValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
