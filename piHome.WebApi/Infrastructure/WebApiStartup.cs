using System.Web.Http;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace piHome.WebApi.Infrastructure
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "piHost/{controller}"
            );

            config.DependencyResolver = new NinjectAPIDependencyResolver();

            appBuilder.UseWebApi(config);//TODO add error handling
            appBuilder.UseStaticFiles("/WebClient");
        }
    }
}
