using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Lisa.Kiwi.WebApi
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(AddUserModel userModel)
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
        public IHttpActionResult IsAdmin()
        {
            var user = (ClaimsIdentity) User.Identity;
            return Ok(user.Claims.Any(c => c.Type == "is_admin" && bool.Parse(c.Value)));
        }

        [Authorize]
        [Route("is_anonymous")]
        [HttpGet]
        public IHttpActionResult IsAnonymous()
        {
            var user = (ClaimsIdentity) User.Identity;

            return Ok(user.Claims.Any(c => c.Type == "is_anonymous" && Boolean.Parse(c.Value)));
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

        private readonly AuthRepository _auth = new AuthRepository();
    }
}