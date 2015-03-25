using System.Web.Mvc;
using System.Web.Routing;

namespace Lisa.Kiwi.Web.Dashboard
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Login", "Login", new {controller = "Account", action = "Login"}
				);

			routes.MapRoute("Default", "{controller}/{action}/{id}",
				new {controller = "Report", action = "Index", id = UrlParameter.Optional}
				);
		}
	}
}