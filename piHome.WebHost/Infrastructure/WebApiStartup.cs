using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;

namespace piHome.WebHost.Infrastructure
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // webAPI
            var webApiConfig = new HttpConfiguration();
            webApiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "piHost/{controller}/{action}"
            );

            if (Type.GetType("Mono.Runtime") != null)
            {
                webApiConfig.MessageHandlers.Add(new MonoPatchingDelegatingHandler());
            }

            webApiConfig.EnableCors(new EnableCorsAttribute("*", "*", "*"));//TODO
            webApiConfig.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            webApiConfig.DependencyResolver = new NinjectAPIDependencyResolver();
            webApiConfig.Services.Add(typeof(IExceptionLogger), new PiHomeExceptionLogger());
            webApiConfig.Services.Replace(typeof(IExceptionHandler), new PiHomeExceptionHandler());

            appBuilder.UseWebApi(webApiConfig);

            // signalR

            var serializer = (JsonSerializer)GlobalHost.DependencyResolver.GetService(typeof(JsonSerializer));
            serializer.Converters.Add(new StringEnumConverter());

            var enableDetailedErrors = Boolean.Parse(ConfigurationManager.AppSettings["SignalREnableDetailedErrors"]);
            var signalRConfig = new HubConfiguration { EnableDetailedErrors = enableDetailedErrors };
            appBuilder
                .UseCors(CorsOptions.AllowAll)
                .MapSignalR(signalRConfig);
        }
    }
}
