using Microsoft.AspNet.SignalR;

namespace Lisa.Kiwi.WebApi.Hubs
{
    public class ReportHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}