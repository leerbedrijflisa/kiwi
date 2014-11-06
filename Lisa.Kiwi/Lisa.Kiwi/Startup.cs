using System.Data.Entity;
using System.Web.Http;
using Lisa.Kiwi.Data;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Lisa.Kiwi.WebApi.Startup))]

namespace Lisa.Kiwi.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
			var config = new HttpConfiguration();

			WebApiConfig.Register(config);
			Database.SetInitializer<KiwiContext>(null);

			// Set up Owin to use the WebAPI's config
	        app.UseWebApi(config);
        }
    }
}