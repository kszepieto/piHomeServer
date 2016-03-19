using System.Text;
using System.Web.Http.ExceptionHandling;
using piHome.Utils;

namespace piHome.WebHost.Infrastructure.ExceptionHandling
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var errMsg = new StringBuilder();
            errMsg.AppendFormat("Exception: {0}", context.Exception);
            errMsg.AppendLine().AppendLine().AppendFormat("Request: {0}", context.Request);

            if (context.ExceptionContext.ControllerContext != null)
            {
                errMsg.AppendLine().AppendLine().AppendFormat("Controller : {0}", context.ExceptionContext.ControllerContext.ControllerDescriptor.ControllerType);
            }

            if (context.ExceptionContext.ActionContext != null)
            {
                errMsg.AppendLine().AppendLine().AppendFormat("Action: {0}", context.ExceptionContext.ActionContext.ActionDescriptor.ActionName);
            }

            LogHelper.LogMessage(errMsg.ToString());
        }
    }
}
