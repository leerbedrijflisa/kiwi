using System;
using System.Linq;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
	public sealed class StatusProxy
	{
        public StatusProxy(Uri odataUrl, string token = null, string tokenType = null)
		{
            _container = new AuthenticationContainer(odataUrl, token, tokenType);
		}

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

        private readonly AuthenticationContainer _container;
	}
}