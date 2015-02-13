using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IHttpActionResult> Get(int id)
        {
            var contact = await _db.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(contact));
        }

        public async Task<IHttpActionResult> Post([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contactData = _dataFactory.Create(contact);
            var reportData = await _db.Reports.FindAsync(contact.Report);

            contactData.Report = reportData;

            _db.Contacts.Add(contactData);
            await _db.SaveChangesAsync();

            contact = _modelFactory.Create(contactData);
            var url = Url.Route("DefaultApi", new { controller = "contacts", id = contactData.Id });

            return Created(url, contact);
        }

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }
}