using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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