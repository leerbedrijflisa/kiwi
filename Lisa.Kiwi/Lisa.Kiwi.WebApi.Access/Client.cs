using System;

namespace Lisa.Kiwi.WebApi.Access
{
    public static class Client
    {
        //Where is the OData API hosted?
		public const string BaseUrl = "http://localhost:20151/odata";

        public static readonly Uri BaseUri = new Uri(BaseUrl);
	}
}
