using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<User>> Get()
        {
            var identityUsers = await _auth.GetUsers();
            var users = new List<User>();

            foreach (var identityUser in identityUsers)
            {
                var user = new User
                {
                    Id = identityUser.Id,
                    UserName = identityUser.UserName,
                    Role = await _auth.GetRole(identityUser)
                };

                users.Add(user);
            }

            return users;
        }
        
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

        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Patch(string id, [FromBody] JToken json)
        {
            if (json.Value<string>("password") == null)
            {
                return BadRequest();
            }

            await _auth.UpdatePassword(id, json.Value<string>("password"));

            return Ok();
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

        [Route("time")]
        [HttpGet]
        public IHttpActionResult Time()
        {
            var timeZoneInfoCorrected = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            var timeZoneInfo = TimeZoneInfo.Utc;

            var originalTime = DateTime.Now;
            var correctedTime = DateTime.Now.AddHours(timeZoneInfoCorrected.GetUtcOffset(DateTime.UtcNow).Hours);
            var isDSTOriginal = timeZoneInfo.IsDaylightSavingTime(originalTime);
            var isDSTCorrected = timeZoneInfoCorrected.IsDaylightSavingTime(correctedTime);


            return Ok(new { originalTime, correctedTime, isDSTOriginal, isDSTCorrected });
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

        [HttpGet]
        [Route("ping")]
        [Authorize]
        public IHttpActionResult Ping()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        private readonly AuthRepository _auth = new AuthRepository();
    }
}