using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Exceptions
{
    public class PayloadReadException : Exception
    {
        public PayloadReadException(string message)
            : base(message)
        { }

        public PayloadReadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
