using System.Net;
using System.Web.Http.ExceptionHandling;
using piHome.WebHost.Infrastructure.Exceptions;

namespace piHome.WebHost.Infrastructure.ExceptionHandling
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private const string GeneralErrorMessage =
            "Uuppss - R2D2 has circuit overload !!! Maybe try again later, then try to off and on :)";

        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            var invalidDataException = exception as InvalidInputException;
            if (invalidDataException != null)
            {
                context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.BadRequest, invalidDataException.Message, invalidDataException.ErrorDetails);
            }

            context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.InternalServerError, GeneralErrorMessage);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            //http://stackoverflow.com/questions/24189315/exceptions-in-asp-net-web-api-custom-exception-handler-never-reach-top-level-whe/24634485#24634485
            return true;
        }
    }
}