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
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace piHome.WebHost.Infrastructure
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            ConfigureWebApi(app);
            ConfigureSignalR(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(new ValidateModelAttribute());

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "piHost/{controller}"
            //);

            if (Type.GetType("Mono.Runtime") != null)
            {
                config.MessageHandlers.Add(new MonoPatchingDelegatingHandler());
            }

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));//TODO
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.DependencyResolver = new NinjectAPIDependencyResolver();

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureSignalR(IAppBuilder app)
        {
            GlobalHost.HubPipeline.RequireAuthentication();

            var serializer = (JsonSerializer)GlobalHost.DependencyResolver.GetService(typeof(JsonSerializer));
            serializer.Converters.Add(new StringEnumConverter());

            var enableDetailedErrors = Boolean.Parse(ConfigurationManager.AppSettings["SignalREnableDetailedErrors"]);
            var signalRConfig = new HubConfiguration { EnableDetailedErrors = enableDetailedErrors };
            app.MapSignalR(signalRConfig);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var authorizationProvider = NinjectConfiguration.GetInstance().Kernel.GetService<SimpleAuthorizationServerProvider>();
            var refreshTokenProvider = NinjectConfiguration.GetInstance().Kernel.GetService<SimpleRefreshTokenProvider>();

            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                Provider = authorizationProvider,
                RefreshTokenProvider = refreshTokenProvider
            };
            
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
