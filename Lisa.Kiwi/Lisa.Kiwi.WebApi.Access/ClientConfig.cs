using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Lisa.Kiwi.WebApi.Access
{
    class ClientConfig
    {
        //Where is the OData API hosted?
        public const string BaseUrl = "http://localhost:20151/odata";

        public HttpClient BuildClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
