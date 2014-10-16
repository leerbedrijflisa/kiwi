using Default;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class StatusProxy
    {
        // Get an entire entity set.
        public static IQueryable<Status> GetStatuses()
        {
            return Client.Container.Status;
        }

        //Create a new entity
        public static void AddStatus(Status status)
        {
            //Client.container.Status.Context.AddObject("Status", status); //CreateStatus(status);
            Client.Container.AddToStatus(status);
            Client.Container.SaveChanges();
            Client.Container = new Container(Client.BaseUri);          
        }
    }
}
