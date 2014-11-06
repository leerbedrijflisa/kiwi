using System.Web.Mvc;
using System.Web.Routing;

namespace Lisa.Kiwi.Web.Reporting
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Default", "{controller}/{action}/{guid}",
				new {controller = "Report", action = "Index", guid = UrlParameter.Optional}
				);
		}
	}
}