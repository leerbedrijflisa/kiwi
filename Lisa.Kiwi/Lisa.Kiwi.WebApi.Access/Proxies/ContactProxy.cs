using System;
using System.Linq;
using Default;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi.Access
{
	public sealed class ContactProxy
	{
        public ContactProxy(Uri odataUrl, string token = null, string tokenType = null)
		{
            _container = new AuthenticationContainer(odataUrl, token, tokenType);
        }

		// Get an entire entity set.
		public IQueryable<Contact> GetContacts()
		{
			return _container.Contact;
		}

		//Create a new entity
		public Contact AddContact(Contact contact, Guid securityToken = new Guid())
		{
			_container.AddToContact(contact);
			_container.SaveChanges();

			return contact;
		}

        private readonly AuthenticationContainer _container;
	}
}