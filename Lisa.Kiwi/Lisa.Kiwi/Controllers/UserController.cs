using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Lisa.Kiwi.Data;
using Lisa.Kiwi.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace Lisa.Kiwi.WebApi.Controllers
{
	[RoutePrefix("api/users")]
	public class UserController : ApiController
	{
		private readonly AuthRepository _auth = new AuthRepository();

		[Authorize(Roles = "Administrator")]
		[Route("add_user")]
		[HttpPost]
		public async Task<IHttpActionResult> AddUser(AddUserModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _auth.AddUser(userModel);

			return GetErrorResult(result) ?? Ok();
		}

		[Authorize]
		[Route("is_admin")]
		[HttpGet]
		public async Task<IHttpActionResult> IsAdmin()
		{
			var user = (ClaimsIdentity)User.Identity;
			return Ok(user.Claims.Any(c => c.Type == "is_admin" && bool.Parse(c.Value)));
		}
		
		[Authorize]
		[Route("test_auth")]
		[HttpGet]
		public IHttpActionResult TestAuth()
		{
			return Ok("Success!");
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (result.Succeeded)
			{
				return null;
			}

			if (result.Errors != null)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error);
				}
			}

			if (ModelState.IsValid)
			{
				// No ModelState errors are available to send, so just return an empty BadRequest.
				return BadRequest();
			}

			return BadRequest(ModelState);
		}

		private readonly KiwiContext _db = new KiwiContext();
	}
}