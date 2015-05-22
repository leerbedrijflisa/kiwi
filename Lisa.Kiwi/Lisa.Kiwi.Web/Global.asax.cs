using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lisa.Kiwi.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public static string GetApiUrl()
        {
            string url = null;

#if DEBUG
            if (FiddlerAvailable())
            {
                url = WebConfigurationManager.AppSettings["WebApiFiddlerUrl"];
            }
#endif
            if (string.IsNullOrEmpty(url))
            {
                url = WebConfigurationManager.AppSettings["WebApiUrl"];
            }
            return url;


        }

        private static bool FiddlerAvailable()
        {
            return Process.GetProcesses()
                .Any(process => process.ProcessName.Contains("Fiddler"));
        }
    }
}