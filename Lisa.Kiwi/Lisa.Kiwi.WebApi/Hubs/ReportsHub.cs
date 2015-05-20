using Microsoft.AspNet.SignalR;

namespace Lisa.Kiwi.WebApi.Hubs
{
    [Authorize]
    public class ReportsHub : Hub
    {
        public void UpdateReport(Report report)
        {
            Clients.All.updateReport(report);
        }
    }
}