using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Access
{
    public class AuthenticationProxy
    {
        public async Task<string> Login()
        {
            object Account = new
            {
                Username = "Kees",
                Password = "Kees123"
            };

            try
            {
               HttpResponseMessage response = await _client.PostAsync("", new StringContent(JsonConvert.SerializeObject(Account), Encoding.UTF8, "application/json"));

                try
                {
                    string token = response.Content.ToString();

                    return token;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("WebApi did not response to login request.");
                }
            } 
            catch (Exception)
            {
                throw new Exception("Failed to login. Please check your credentials.");
            }
        }

        private AuthenticationProxy(Uri odataUrl)
        {
            _client = new HttpClient
            {
                BaseAddress = odataUrl
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _client;
    }
}