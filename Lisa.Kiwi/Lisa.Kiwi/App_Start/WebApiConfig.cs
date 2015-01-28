using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Enable Web API Attribute routing (used in non-REST controllers)
			config.MapHttpAttributeRoutes();

			ODataModelBuilder builder = new ODataConventionModelBuilder();

			builder.EntitySet<Report>("Report");
			builder.EntitySet<Contact>("Contact");
			builder.EntitySet<Remark>("Remark");
			builder.EntitySet<Status>("Status");
            builder.EntitySet<Vehicle>("Vehicles");

			config.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());
		}
	}
}