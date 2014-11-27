using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Lisa.Kiwi.Web.Dashboard;

[assembly: PreApplicationStartMethod(typeof(PreApplicationStart), "Start")]

namespace Lisa.Kiwi.Web.Dashboard
{
	public class PreApplicationStart
	{
		public static void Start()
		{
			Roles.Enabled = true;
		}
	}

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}