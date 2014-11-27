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
		public AuthenticationProxy(Uri authUri, Uri userControllerUri)
        {
            _authClient = new HttpClient
            {
                BaseAddress = authUri
            };

            _authClient.DefaultRequestHeaders.Accept.Clear();
            _authClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

			_userControllerClient = new HttpClient
			{
				BaseAddress = userControllerUri
			};
        }

	    public async Task<LoginResult> Login(string username, string password)
	    {
		    var result = new LoginResult();
		    var response = await _authClient.PostAsync("", new StringContent(
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


		public async Task<bool> GetIsAdmin(string tokenType, string token)
		{
			_userControllerClient.DefaultRequestHeaders.Add("Authorization", tokenType + " " + token);
			var response = await _userControllerClient.GetAsync("is_admin");

			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new HttpException(string.Format("Unable to retrieve data from the web API, received status code {0}!", response.StatusCode));
			}

			var content = await response.Content.ReadAsStringAsync();
			return bool.Parse(content);
		}

        private readonly HttpClient _authClient;
		private readonly HttpClient _userControllerClient;
    }
}