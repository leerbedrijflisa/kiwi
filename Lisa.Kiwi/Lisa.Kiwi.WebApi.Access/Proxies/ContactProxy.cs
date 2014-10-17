using System.Linq;
using Default;

//Namespace mimic door access layer template
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ContactProxy
    {
        // Get an entire entity set.
        public static IQueryable<Contact> GetContacts()
        {
            return Client.Container.Contact;
        }

        //Create a new entity
        public static void AddContact(Contact contact)
        {
            Client.Container.AddToContact(contact);
            Client.Container.SaveChanges();
            Client.Container = new Container(Client.BaseUri);
        }
    }
}
