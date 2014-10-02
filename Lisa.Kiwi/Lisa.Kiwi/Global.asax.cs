using System.Data.Entity;
using System.Web;
using Lisa.Kiwi.Data.Models;
using System.Web.Http;

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
