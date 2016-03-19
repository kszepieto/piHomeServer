using System.Collections.Generic;

namespace piHome.WebHost.Infrastructure.ExceptionHandling
{
    public class ErrorVM
    {
        public string Message { get; }
        public IEnumerable<string> Details { get; }

        public ErrorVM(string message, IEnumerable<string> details = null)
        {
            Message = message;
            Details = details;
        }
    }
}