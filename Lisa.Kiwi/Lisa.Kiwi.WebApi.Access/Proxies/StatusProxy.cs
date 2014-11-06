using System;
using System.Linq;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
	public sealed class StatusProxy
	{
		private readonly Container _container;

		public StatusProxy(Uri odataUrl)
		{
			_container = new Container(odataUrl);
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
	}
}