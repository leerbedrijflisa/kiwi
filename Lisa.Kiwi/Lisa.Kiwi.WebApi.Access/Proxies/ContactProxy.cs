using System.Linq;

//Namespace mimic door access layer template
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ContactProxy : Client
    {
        // Get an entire entity set.
        public IQueryable<Contact> GetContacts()
        {
            return Container.Contact;
        }

        //Create a new entity
        public Contact AddContact(Contact contact)
        {
            Container.AddToContact(contact);
            Container.SaveChanges();

            return contact;
        }
    }
}
