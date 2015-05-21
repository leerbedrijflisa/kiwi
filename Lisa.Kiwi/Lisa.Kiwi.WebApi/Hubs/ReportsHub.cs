using System.Net;
using Microsoft.AspNet.SignalR;

namespace Lisa.Kiwi.WebApi
{
    public class ReportsHub : Hub
    {
        public HttpStatusCode Authorize(string token)
        {
            Groups.Add(Clients.Caller, "Authorized");
            return HttpStatusCode.Unauthorized;
        }
    }
}