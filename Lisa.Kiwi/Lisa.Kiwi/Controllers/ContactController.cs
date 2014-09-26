using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Lisa.Kiwi.Data.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class ContactController : ODataController
    {
        private KiwiContext db = new KiwiContext();
        private readonly CloudQueue _queue = new QueueConfig().BuildQueue();

        // GET odata/Contact
        [EnableQuery]
        public IQueryable<Contact> GetContact()
        {
            return db.Contacts;
        }

        // GET odata/Contact(5)
        [EnableQuery]
        public SingleResult<Contact> GetContact([FromODataUri] int key)
        {
            return SingleResult.Create(db.Contacts.Where(contact => contact.Id == key));
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

            try
            {
                await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(contact)));
            }
            catch (Exception)
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

            await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(contact)));

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

            Contact contact = await db.Contacts.FindAsync(key);
            if (contact == null)
            {
                return NotFound();
            }

            patch.Patch(contact);

            try
            {
                await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(contact)));
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
            Contact contact = await db.Contacts.FindAsync(key);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
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
            return db.Contacts.Count(e => e.Id == key) > 0;
        }
    }
}
