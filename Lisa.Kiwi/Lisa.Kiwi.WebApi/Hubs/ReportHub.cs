using Microsoft.AspNet.SignalR;

namespace Lisa.Kiwi.WebApi
{
    public class ReportHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}