using System.Collections.Generic;
using System.Web.Http;

namespace Lisa.Kiwi.WebApi.Controllers
{
	[RoutePrefix("api/auth")]
	public class AuthenticationController : ApiController
	{
		[Route("test")]
		[HttpGet]
		public IEnumerable<string> Test()
		{
			return new[] {"This is", "a test!"};
		}
	}
}