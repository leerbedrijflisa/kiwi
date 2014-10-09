using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class StatusProxy
    {
        // Get an entire entity set.
        public static IQueryable<Status> GetStatuses()
        {
            return Client.container.Status;
        }

        //Create a new entity
        public static void AddStatus(Status status)
        {
            //Client.container.Status.Context.AddObject("Status", status); //CreateStatus(status);
            Client.container.AddToStatus(status);
            Client.container.SaveChanges();
            Client.container = new Container(Client.BaseUri);          
        }
    }
}
