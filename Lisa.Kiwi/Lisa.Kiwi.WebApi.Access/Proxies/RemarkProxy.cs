using System;
using System.Linq;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
	public class RemarkProxy
	{
        public RemarkProxy(Uri odataUrl, string token = null, string tokenType = null)
		{
            _container = new AuthenticationContainer(odataUrl, token, tokenType);
        }

		// Get an entire entity set.
		public IQueryable<Remark> GetRemarks()
		{
			return _container.Remark;
		}

		//Create a new entity
		public void AddRemark(Remark remark)
		{
			_container.AddToRemark(remark);
			_container.SaveChanges();
		}

        private readonly AuthenticationContainer _container;
	}
}