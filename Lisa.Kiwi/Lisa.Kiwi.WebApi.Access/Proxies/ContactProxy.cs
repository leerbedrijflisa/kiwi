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

	    public async Task<Contact> AddContactWithToken(Contact contact, Guid token)
	    {
	        var client = new HttpClient
	        {
	            BaseAddress = new Uri(_odataUri + "/Contact")
	        };

	        client.DefaultRequestHeaders.Accept.Clear();
	        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

	        var mappedContact = new
	        {
	            Name = contact.Name,
	            EmailAddress = contact.EmailAddress,
	            PhoneNumber = contact.PhoneNumber,
	            StudentNumber = contact.StudentNumber,
				EditToken = token,
	            report = contact.Report,
	            securityToken = token
	        };

	        var serializedContact = JsonConvert.SerializeObject(mappedContact);

	        HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, new Uri(_odataUri + "/Contact"));
	        req.Content = new StringContent(serializedContact, Encoding.UTF8, "application/json");
	        HttpResponseMessage response = await client.SendAsync(req);

	        var requestResponse = await response.Content.ReadAsStringAsync();
	        var returnReponse = JsonConvert.DeserializeObject<Contact>(requestResponse);

	        return returnReponse;
	    }

	    private readonly AuthenticationContainer _container;
	    private Uri _odataUri;
	}
}