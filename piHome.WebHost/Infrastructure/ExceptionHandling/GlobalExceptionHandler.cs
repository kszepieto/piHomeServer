using System.Collections.Generic;
using System.Net;
using System.Web.Http.ExceptionHandling;
using piHome.Utils.Exceptions;

namespace piHome.WebHost.Infrastructure.ExceptionHandling
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private const string GeneralErrorMessage =
            "Uuppss - R2D2 has circuit overload !!! Try to off and on :), if this doesn't help You are DOOMED";

        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            var invalidDataException = exception as InvalidInputException;
            if (invalidDataException != null)
            {
                context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.BadRequest, invalidDataException.Message, invalidDataException.ErrorDetails);
            }

            var businesRuleViolationException = exception as BusinesRuleViolationException;
            if (businesRuleViolationException != null)
            {
                context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.BadRequest, "Businnes rules violation", businesRuleViolationException.Errors);
            }

            var entityNotFoundException = exception as EntityNotFoundException;
            if(entityNotFoundException != null)
            {
                var message = $"Entity id: {entityNotFoundException.ID}, type: {entityNotFoundException.ObjecType.Name} cannot be found";
                context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.NotFound, message);
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