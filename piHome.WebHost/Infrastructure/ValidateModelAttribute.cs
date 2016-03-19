using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using piHome.WebHost.Infrastructure.ExceptionHandling;

namespace piHome.WebHost.Infrastructure
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var errors = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorVM("Validation error", errors));
            }
        }

        /// <summary>
        /// To prevent filter from executing twice on same call. Problem solved by:
        /// http://stackoverflow.com/questions/18485479/webapi-filter-is-calling-twice?rq=1
        /// </summary>
        public override bool AllowMultiple
        {
            get { return false; }
        }
    }
}
