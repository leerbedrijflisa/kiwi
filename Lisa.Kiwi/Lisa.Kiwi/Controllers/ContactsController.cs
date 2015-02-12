using System;
using System.Linq;
using System.Web.Http;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class ContactsController : ApiController
    {
        public IQueryable<Contact> Get()
        {
            var contacts = _db.Contacts
                .ToList()
                .Select(contact => _modelFactory.Create(contact))
                .AsQueryable();

            return contacts;
        }

        public IHttpActionResult Get(int id)
        {
            var contact = _db.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(contact));
        }

        public IHttpActionResult Post([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contactData = _dataFactory.Create(contact);
            var reportData = _db.Reports.Find(contact.Report);

            contactData.Report = reportData;

            _db.Contacts.Add(contactData);
            _db.SaveChanges();

            contact = _modelFactory.Create(contactData);
            var url = String.Format("/contacts/{0}", contactData.Id);

            return Created(url, contact);
        }

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }
}