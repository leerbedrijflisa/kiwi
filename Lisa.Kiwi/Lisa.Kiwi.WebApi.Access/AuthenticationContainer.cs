using System;
using Default;
using Microsoft.OData.Client;

namespace Lisa.Kiwi.WebApi.Access
{
    class AuthenticationContainer : Container
    {
        public AuthenticationContainer(Uri serviceRoot, string token = null, string tokenType = null) : base(serviceRoot)
        {
            _token = token;
            _tokenType = tokenType;

            if (!string.IsNullOrEmpty(token))
            {
                BuildingRequest += _container_InjectToken;
            }
        }

        void _container_InjectToken(object sender, BuildingRequestEventArgs e)
        {
            e.Headers.Add("Authorization", _tokenType + " " + _token);
        }

        private string _token ;
        private string _tokenType;
    }
}
