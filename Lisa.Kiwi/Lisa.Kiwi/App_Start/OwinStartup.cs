using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Lisa.Kiwi.WebApi.OwinStartup))]

namespace Lisa.Kiwi.WebApi
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}