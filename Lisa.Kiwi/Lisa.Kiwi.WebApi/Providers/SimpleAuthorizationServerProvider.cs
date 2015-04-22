using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.Owin.Security.OAuth;

namespace Lisa.Kiwi.WebApi
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            if (context.GrantType != "anonymous")
            {
                return;
            }

            var db = new KiwiContext();

            var protectedToken = HttpServerUtility.UrlTokenDecode(context.Parameters.Get("token"));

            if (protectedToken == null)
            {
                return;
            }

            var anonymousToken = Encoding.UTF8.GetString(MachineKey.Unprotect(protectedToken));

            var tokenArray = anonymousToken.Split('‼');

            var reportId = Int32.Parse(tokenArray[0]);
            var time = DateTime.Parse(tokenArray[1]);

            if (tokenArray.Count() != 2 && reportId != 0 && time != new DateTime())
            {
                context.SetError("invalid_grant", "This token is invalid");
                return;
            }

            if (time < DateTime.Now)
            {
                context.SetError("invalid_grant", "This token has expired");
                return;
            }

            if (await db.Reports.FindAsync(reportId) == null)
            {
                context.SetError("invalid_grant", "No report found with that ID");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Role, "anonymous"));
            identity.AddClaim(new Claim("reportId", reportId.ToString()));
            identity.AddClaim(new Claim("is_anonymous", "true"));

            context.Validated(identity);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var repo = new AuthRepository())
            {
                var user = await repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("id", user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Role, "dashboardUser"));

                var isAdmin = repo.HasRole(user, "Administrator");
                identity.AddClaim(new Claim("is_admin", isAdmin.ToString()));

                context.Validated(identity);
            }
        }
    }
}