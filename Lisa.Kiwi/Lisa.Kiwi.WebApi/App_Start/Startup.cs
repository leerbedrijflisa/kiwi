using System;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;


namespace Lisa.Kiwi.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Make sure the database is updated to the latest version. This effectively runs Update-Database, even when running in Azure.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KiwiContext, Configuration>());

            var config = new HttpConfiguration();
            ConfigureOAuth(app);

            // Set up Owin to use the WebAPI's config
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);

            app.Map("/signalr", map =>
            {

                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();

                map.RunSignalR(hubConfiguration);
            });

            app.UseWebApi(config);
        }

        private static void ConfigureOAuth(IAppBuilder app)
        {
            var serverOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/oauth"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(serverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}