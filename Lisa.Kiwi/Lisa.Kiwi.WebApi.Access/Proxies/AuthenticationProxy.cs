using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lisa.Kiwi.WebApi.Access
{
    public class AuthenticationProxy
    {
        public async Task<string> Login(string username, string password)
        {
            try
            {
               HttpResponseMessage response = await _client.PostAsync("", new StringContent("grant_type=password&username=" + username + "&password=" + password));

                try
                {
                    string token = await response.Content.ReadAsStringAsync();

                    return token;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("WebApi did not respond to login request.");
                }
            } 
            catch (Exception)
            {
                throw new Exception("Failed to login. Please check your credentials.");
            }
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