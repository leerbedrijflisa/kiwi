using System.Linq;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
    public sealed class StatusProxy
    {
		private readonly Container _container = new Container(Client.BaseUri);

        // Get an entire entity set.
        public IQueryable<Status> GetStatuses()
        {
			return _container.Status;
        }

        //Create a new entity
        public void AddStatus(Status status)
        {
			_container.AddToStatus(status);
			_container.SaveChanges();           
        }
    }
}
