using System;
using System.Collections.Generic;

namespace piHome.WebHost.Infrastructure.Exceptions
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
