using System;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
    class AuthenticationContainer : Container
    {
        public AuthenticationContainer(Uri serviceRoot, string token = null) : base(serviceRoot)
        {
            if (token != null)
            {
                BuildingRequest += _container_InjectToken;
            }
        }

        void _container_InjectToken(object sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
        {
            e.Headers.Add("Token", "123456");
        }
    }
}
