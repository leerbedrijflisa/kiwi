using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lisa.Kiwi.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace Lisa.Kiwi.WebApi.Controllers
{
	[RoutePrefix("api/users")]
	public class AuthenticationController : ApiController
	{
		private readonly AuthRepository _auth = new AuthRepository();

		[Authorize(Roles = "Administrator")]
		[Route("add_user")]
		public async Task<IHttpActionResult> AddUser(AddUserModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _auth.AddUser(userModel);

			return GetErrorResult(result) ?? Ok();
		}

		[Route("test_auth")]
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
	}
}