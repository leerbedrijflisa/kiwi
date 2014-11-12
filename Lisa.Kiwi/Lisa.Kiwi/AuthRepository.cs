using System;
using System.Threading.Tasks;
using Lisa.Kiwi.Data.Models;
using Lisa.Kiwi.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.WebApi
{
	public class AuthRepository : IDisposable
	{
		private readonly KiwiAuthContext _ctx;
		private readonly UserManager<IdentityUser> _userManager;

		public AuthRepository()
		{
			_ctx = new KiwiAuthContext();
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
	}
}