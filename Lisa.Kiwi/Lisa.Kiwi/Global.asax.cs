using System.Data.Entity;
using System.Web;
using System.Web.Http;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			Database.SetInitializer<KiwiContext>(null);
		}
	}
}