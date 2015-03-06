using System;
using System.Linq;
using System.Threading.Tasks;
using Lisa.Kiwi.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.WebApi
{
	public class AuthRepository : IDisposable
	{
		private readonly KiwiContext _ctx;
		private readonly UserManager<IdentityUser> _userManager;

		public AuthRepository()
		{
			_ctx = new KiwiContext();
			_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
		}

		public void Dispose()
		{
			_ctx.Dispose();
			_userManager.Dispose();
		}

		public async Task<IdentityResult> AddUser(AddUserModel userModel)
		{
			var user = new IdentityUser
			{
				UserName = userModel.UserName
			};

			var result = await _userManager.CreateAsync(user, userModel.Password);

			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password)
		{
			var user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public async Task<bool> HasRole(IdentityUser user, string roleName)
		{
			var role = _ctx.Roles.First(r => r.Name == roleName);
			return user.Roles.Any(r => r.RoleId == role.Id);
		}
	}
}