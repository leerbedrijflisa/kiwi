using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
	public class AuthenticationProxy
    {
	    public async Task<LoginResult> Login(string username, string password)
	    {
		    var result = new LoginResult();
		    var response = await _client.PostAsync("", new StringContent(
					"grant_type=password&username=" + HttpUtility.UrlEncode(username) +
					"&password=" + HttpUtility.UrlEncode(password)));

		    if (response.StatusCode != HttpStatusCode.OK)
			{
				result.Status = LoginStatus.ConnectionError;
				return result;
			}

		    var content = await response.Content.ReadAsStringAsync();
			var authInfo = JObject.Parse(content);
		    var authTokenJobj = authInfo.SelectToken("access_token");

		    if (authTokenJobj == null)
		    {
			    result.Status = LoginStatus.UserPassMismatch;
			    return result;
		    }

			result.Token = authTokenJobj.ToString();
			result.TokenType = authInfo.SelectToken("token_type").ToString();
			result.TokenExpiresIn = authInfo.SelectToken("expires_in").ToString();

		    return result;
	    }

	    public AuthenticationProxy(Uri authUri)
        {
            _client = new HttpClient
            {
                BaseAddress = authUri
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        private readonly HttpClient _client;
    }
}