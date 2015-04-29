using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Lisa.Common.Access;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class AuthenticationProxy
    {
        public AuthenticationProxy(string baseUrl, string resourceUrl)
        {
            _resourceUrl = resourceUrl;
            _client = new HttpClient {BaseAddress = new Uri(baseUrl)};

        }

        // TODO: replace the two functions underneath with a function with which you can request
        // the claims of the current user.

        public async Task<bool> GetIsAdmin(string tokenType, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", String.Format("{0} {1}", tokenType, token));

            var response = await _client.GetAsync("is_admin");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebApiException("Unexpected statuscode", response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            return Boolean.Parse(content);
        }

        public async Task<bool> GetIsAnonymous(string tokenType, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", String.Join(" ", tokenType, token));

            var response = await _client.GetAsync("/api/users/is_anonymous");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebApiException("Unexpected statuscode", response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();

            return Boolean.Parse(content);
        }

        public async Task<Token> Login(string userName, string password)
        {
            var requestContent = String.Format(
                "grant_type=password&username={0}&password={1}",
                HttpUtility.UrlEncode(userName),
                HttpUtility.UrlEncode(password));

            return await Login(requestContent);
        }

        public async Task<Token> LoginAnonymous(string anonymousToken)
        {
            var requestContent = String.Format(
                "grant_type=anonymous&token={0}",
                HttpUtility.UrlEncode(anonymousToken));

            return await Login(requestContent);
        }

        private async Task<Token> Login(string requestContent)
        {
            var response = await _client.PostAsync(_resourceUrl, new StringContent(requestContent));
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return null;

                case HttpStatusCode.OK:
                    var resultContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var authInfo = JObject.Parse(resultContent);
                        return new Token()
                        {
                            Value = authInfo.SelectToken("access_token").ToString(),
                            Type = authInfo.SelectToken("token_type").ToString(),
                            ExpiresIn = authInfo.SelectToken("expires_in").Value<int>()
                        };
                    }
                    catch (Exception e)
                    {
                        throw new WebApiException("The response body was malformed. Either the JSON is incorrect or a token property is missing.", response.StatusCode, e);
                    }

                default:
                    throw new WebApiException("Unexpected HTTP status code. Only 200 OK and 400 Bad Request are acceptable.", response.StatusCode);
            }
        }

        private readonly HttpClient _client;
        private readonly string _resourceUrl;
    }
}