using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace piHome.WebHost.Infrastructure
{
    public class PiHomeExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                ErrMsg = "Uuppss - R2D2 has circuit overload !!! Maybe try again later, then try to off and on :)"
            };
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            //http://stackoverflow.com/questions/24189315/exceptions-in-asp-net-web-api-custom-exception-handler-never-reach-top-level-whe/24634485#24634485
            return true;
        }
    }

    class TextPlainErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }
        public string ErrMsg { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ErrMsg);
            return Task.FromResult(response);
        }
    }
}