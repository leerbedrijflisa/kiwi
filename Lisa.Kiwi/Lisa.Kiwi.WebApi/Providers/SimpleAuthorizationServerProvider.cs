using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.Owin.Security.OAuth;

namespace Lisa.Kiwi.WebApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            if (context.GrantType == "anonymous")
            {
                var db = new KiwiContext();

                var protectedToken = HttpServerUtility.UrlTokenDecode(context.Parameters.Get("token"));

                var anonymousToken = Encoding.UTF8.GetString(MachineKey.Unprotect(protectedToken));

                

                int reportId = 0;
                DateTime time;

                var tokenArray = anonymousToken.Split('‼');

                if (tokenArray.Count() != 2 && Int32.TryParse(tokenArray[0], out reportId) && DateTime.TryParse(tokenArray[1], out time))
                {
                    context.SetError("invalid_grant", "This token is invalid");
                    return;
                }
                if (db.Reports.Find(reportId) == null)
                {
                    context.SetError("invalid_grant", "No report found with that ID");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                
                identity.AddClaim(new Claim(ClaimTypes.Role, "Anonymous"));
                identity.AddClaim(new Claim("reportId", reportId.ToString()));

                context.Validated();

            }
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
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

                var isAdmin = await repo.HasRole(user, "Administrator");
                identity.AddClaim(new Claim("is_admin", isAdmin.ToString()));

                context.Validated(identity);
            }
        }
    }
}