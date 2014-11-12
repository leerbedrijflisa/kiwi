using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.Data.Models
{
	public class KiwiAuthContext : IdentityDbContext<IdentityUser>
	{
		public KiwiAuthContext()
            : base("KiwiAuthContext")
        {
        }
	}
}
