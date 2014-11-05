﻿using System;
using Default;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class RemarkProxy
	{
		private readonly Container _container;

		public RemarkProxy(Uri odataUrl)
	    {
			_container = new Container(odataUrl);
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
    }
}
