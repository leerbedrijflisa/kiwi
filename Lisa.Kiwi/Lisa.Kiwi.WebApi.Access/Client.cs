using Default;
using System;

namespace Lisa.Kiwi.WebApi.Access
{
    public class Client
    {
        //Where is the OData API hosted?
        private const string BaseUrl = "http://localhost:20151/odata";

        internal static Uri BaseUri = new Uri(BaseUrl);

        // You need this to initialize the access layer
        internal Container Container = new Container(BaseUri);
    }
}
