using System;
using System.Collections.Generic;

namespace piHome.Utils.Exceptions
{
    public class InvalidInputException : Exception
    {
        public IEnumerable<string> ErrorDetails { get; }

        public InvalidInputException(string message, IEnumerable<string> errorDetails)
            : base(message)
        {
            ErrorDetails = errorDetails;
        }
    }
}
