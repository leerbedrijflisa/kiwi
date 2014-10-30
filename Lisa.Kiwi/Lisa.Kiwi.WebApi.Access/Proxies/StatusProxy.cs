using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class StatusProxy : Client
    {
        // Get an entire entity set.
        public IQueryable<Status> GetStatuses()
        {
            return Container.Status;
        }

        //Create a new entity
        public void AddStatus(Status status)
        {
            Container.AddToStatus(status);
            Container.SaveChanges();           
        }
    }
}
