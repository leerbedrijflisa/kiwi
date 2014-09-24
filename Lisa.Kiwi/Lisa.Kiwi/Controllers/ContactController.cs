using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Lisa.Kiwi.Data.Models;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class ContactController : ODataController
    {
        private KiwiContext db = new KiwiContext();

        // GET odata/Contact
        [EnableQuery]
        public IQueryable<Contact> GetContact()
        {
            return db.Contact;
        }

        // GET odata/Contact(5)
        [EnableQuery]
        public SingleResult<Contact> GetContact([FromODataUri] int key)
        {
            return SingleResult.Create(db.Contact.Where(contact => contact.Id == key));
        }

        // PUT odata/Contact(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != contact.Id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contact);
        }

        // POST odata/Contact
        public async Task<IHttpActionResult> Post(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contact.Add(contact);
            await db.SaveChangesAsync();

            return Created(contact);
        }

        // PATCH odata/Contact(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Contact> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact contact = await db.Contact.FindAsync(key);
            if (contact == null)
            {
                return NotFound();
            }

            patch.Patch(contact);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contact);
        }

        // DELETE odata/Contact(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Contact contact = await db.Contact.FindAsync(key);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contact.Remove(contact);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int key)
        {
            return db.Contact.Count(e => e.Id == key) > 0;
        }
    }
}
