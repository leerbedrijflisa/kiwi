using System;
using System.Linq;
using Default;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi.Access
{
    public sealed class ContactProxy
	{
		public ContactProxy(Uri odataUrl)
	    {
			_container = new Container(odataUrl);
	    }

        // Get an entire entity set.
        public IQueryable<Contact> GetContacts()
        {
            return _container.Contact;
        }

        //Create a new entity
        public Contact AddContact(Contact contact)
        {
			_container.AddToContact(contact);
			_container.SaveChanges();

            return contact;
        }

        private readonly Container _container;
    }
}
