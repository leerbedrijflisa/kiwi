using System.Linq;
using Default;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi.Access
{
    public sealed class ContactProxy
	{
		private readonly Container _container = new Container(Client.BaseUri);

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
    }
}
