using System;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class StatusProxy
    {
        // You need this to initialize the access layer
        private static readonly Container container = new Container(new Uri(ClientConfig.BaseUrl));

        // Get an entire entity set.
        public static IQueryable<Status> GetStatuses()
        {
            return container.Status;
        }

        //Create a new entity
        public static void AddStatus(Status status)
        {
            container.AddToStatus(status);
            var serviceResponse = container.SaveChanges();
        }
    }
}
