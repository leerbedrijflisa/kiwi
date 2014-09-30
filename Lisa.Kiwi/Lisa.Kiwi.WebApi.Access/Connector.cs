using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lisa.Kiwi.WebApi.Access
{
    public class Connector
    {
        public static HttpClient Client;

        public static void Main() {
            RunAsync().Wait();
        }

        public static async Task RunAsync() {

            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:20151/");
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public void GetData()
        {
            HttpResponseMessage response = Client.GetAsync("odata/Report").Result;

            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ToString();

            }
            else
            {
                var test = string.Format("Error Code" + 
                response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
