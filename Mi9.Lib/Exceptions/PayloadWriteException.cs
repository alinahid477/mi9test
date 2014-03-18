using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Exceptions
{
    public class PayloadWriteException : Exception
    {
        public PayloadWriteException(string message)
            : base(message)
        { }

        public PayloadWriteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
