using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting.Services;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using piHome.WebHost.AuthProviders;
using piHome.WebHost.Infrastructure.DI;
using piHome.WebHost.Infrastructure.ExceptionHandling;

namespace piHome.WebHost.Infrastructure
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureWebApi(app);
            ConfigureSignalR(app);
            ConfigureOAuth(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var webApiConfig = new HttpConfiguration();
            webApiConfig.MapHttpAttributeRoutes();

            webApiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "piHost/{controller}"
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

            app.UseWebApi(webApiConfig);
        }

        private void ConfigureSignalR(IAppBuilder app)
        {
            var serializer = (JsonSerializer)GlobalHost.DependencyResolver.GetService(typeof(JsonSerializer));
            serializer.Converters.Add(new StringEnumConverter());

            var enableDetailedErrors = Boolean.Parse(ConfigurationManager.AppSettings["SignalREnableDetailedErrors"]);
            var signalRConfig = new HubConfiguration { EnableDetailedErrors = enableDetailedErrors };
            app
                .UseCors(CorsOptions.AllowAll)
                .MapSignalR(signalRConfig);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var authorizationProvider = NinjectConfiguration.GetInstance().Kernel.GetService<SimpleAuthorizationServerProvider>();
            var refreshTokenProvider = NinjectConfiguration.GetInstance().Kernel.GetService<SimpleRefreshTokenProvider>();

            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(20),
                Provider = authorizationProvider,
                RefreshTokenProvider = refreshTokenProvider
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
