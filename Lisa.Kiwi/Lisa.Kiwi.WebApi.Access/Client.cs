using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Lisa.Kiwi.WebApi.Access
{
    internal class Client
    {
        //Where is the OData API hosted?
        private const string BaseUrl = "http://localhost:20151/odata";

        internal static Uri BaseUri = new Uri(BaseUrl);

        // You need this to initialize the access layer
        internal static Container Container = new Container(BaseUri);
    }
}
