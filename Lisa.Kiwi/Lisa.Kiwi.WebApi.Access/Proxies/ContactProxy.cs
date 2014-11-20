using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Access
{
	public sealed class ContactProxy
	{
        public ContactProxy(Uri odataUrl, string token = null, string tokenType = null)
		{
            _container = new AuthenticationContainer(odataUrl, token, tokenType);
            _odataUri = odataUrl;
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
	    
	    private readonly AuthenticationContainer _container;
	    private Uri _odataUri;
	}
}