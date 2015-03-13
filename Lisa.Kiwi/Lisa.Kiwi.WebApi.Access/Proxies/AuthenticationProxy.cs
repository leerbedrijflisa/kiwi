using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class AuthenticationProxy
    {
        public AuthenticationProxy(string baseUrl, string resourceUrl)
        {
            _baseUrl = baseUrl;
            _resourceUrl = resourceUrl;
        }

        public async Task<LoginResult> Login(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var result = new LoginResult();
                client.BaseAddress = new Uri(_baseUrl);

                var response = await client.PostAsync(_resourceUrl,
                    new StringContent(String.Format("grant_type=password&username={0}&password={1}", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(password))));

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    result.Status = LoginStatus.ConnectionError;
                    return result;
                }
                
                var content = await response.Content.ReadAsStringAsync();
                var authInfo = JObject.Parse(content);
                var token = authInfo.SelectToken("access_token");

                if (token == null)
                {
                    result.Status = LoginStatus.UserPassMismatch;
                    return result;
                }

                result.Token = token.ToString();
                result.TokenType = authInfo.SelectToken("token_type").ToString();
                result.TokenExpiresIn = authInfo.SelectToken("expires_in").Value<int>();

                return result;
            }
        }

        public async Task<bool> GetIsAdmin(string tokenType, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorize", String.Format("{0} {1}", tokenType, token));

                var response = await client.GetAsync("is_admin");

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpException();
                }

                var content = await response.Content.ReadAsStringAsync();
                return Boolean.Parse(content);
            }
        }

        public async Task<LoginResult> LoginAnonymous(string anonymousToken)
        {
            using (var client = new HttpClient())
            {
                var result = new LoginResult();
                client.BaseAddress = new Uri(_baseUrl);

                var response = await client.PostAsync(_resourceUrl, new StringContent(String.Format("grant_type=anonymous&token={0}", anonymousToken)));
                
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    result.Status = LoginStatus.ConnectionError;
                    return result;
                }

                var content = await response.Content.ReadAsStringAsync();
                var authInfo = JObject.Parse(content);
                var token = authInfo.SelectToken("access_token");

                if (token == null)
                {
                    result.Status = LoginStatus.UserPassMismatch;
                    return result;
                }

                result.Token = token.ToString();
                result.TokenType = authInfo.SelectToken("token_type").ToString();
                result.TokenExpiresIn = authInfo.SelectToken("expires_in").Value<int>();

                return result;
            }
        }

        private readonly string _baseUrl;
        private readonly string _resourceUrl;
    }
}