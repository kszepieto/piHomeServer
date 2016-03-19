using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace piHome.WebHost.Infrastructure.ExceptionHandling
{
    public class SimpleErrorResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;
        private readonly string _errorMessage;
        private readonly IEnumerable<string> _errorDetails;

        public SimpleErrorResult(HttpRequestMessage requestMessage, HttpStatusCode statusCode, string errorMessage, IEnumerable<string> errorDetails = null)
        {
            _requestMessage = requestMessage;
            _statusCode = statusCode;
            _errorMessage = errorMessage;
            _errorDetails = errorDetails;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_requestMessage.CreateResponse(_statusCode, new ErrorVM(_errorMessage, _errorDetails)));
        }
    }
}