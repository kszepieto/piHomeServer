using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Converters;
using Owin;

namespace piHome.WebHost.Infrastructure
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var webApiConfig = new HttpConfiguration();
            webApiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "piHost/{controller}/{action}"
            );
            
            webApiConfig.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            webApiConfig.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            webApiConfig.DependencyResolver = new NinjectAPIDependencyResolver();
            appBuilder.UseWebApi(webApiConfig);

            var enableDetailedErrors = false;
#if DEBUG
            enableDetailedErrors = true;
#endif

            var signalRConfig = new HubConfiguration { EnableDetailedErrors = enableDetailedErrors };
            appBuilder
                .UseCors(CorsOptions.AllowAll)
                .MapSignalR(signalRConfig);
        }
    }
}
