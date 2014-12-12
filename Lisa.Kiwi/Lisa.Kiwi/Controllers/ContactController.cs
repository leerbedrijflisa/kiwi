using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Lisa.Kiwi.Data;
using Lisa.Kiwi.WebApi.Models;

namespace Lisa.Kiwi.WebApi.Controllers
{
	[Authorize]
	public class ContactController : ODataController
	{
		private readonly KiwiContext db = new KiwiContext();

		// GET odata/Contact
		[EnableQuery]
		public IQueryable<Contact> GetContact()
		{
            var result = from c in db.Contacts
                         select new Contact
                         {
                             Id = c.Id,
                             EmailAddress = c.EmailAddress,
                             Name = c.Name,
                             PhoneNumber = c.PhoneNumber,
                             Report = c.Report.Id,
                         };
			return result;
		}

		// GET odata/Contact(5)
		[EnableQuery]
		public SingleResult<Contact> GetContact([FromODataUri] int key)
		{
            var result = from c in db.Contacts
                         where c.Id == key
                         select new Contact
                         {
                             Id = c.Id,
                             EmailAddress = c.EmailAddress,
                             Name = c.Name,
                             PhoneNumber = c.PhoneNumber,
                             Report = c.Report.Id,
                         };
            return new SingleResult<Contact>(result);
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
                var newContact = new Data.Contact
                {
                    Id = contact.Id,
                    EmailAddress = contact.EmailAddress,
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    Report = db.Reports.Where(r => r.Id == contact.Report).FirstOrDefault()
                };

				db.Contacts.Add(newContact);

				await db.SaveChangesAsync();
			}
			catch (Exception)
			{
				if (!ContactExists(key))
				{
					return NotFound();
				}
				throw;
			}

			return Updated(contact);
		}

		// POST odata/Contact
		[AllowAnonymous]
		public async Task<IHttpActionResult> Post(Contact contact)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			var report = db.Reports.FirstOrDefault(r => r.Id == contact.Report);

			// Only allow assigning contacts to reports without a matching token if the user is an admin
			if ((report == null || report.EditToken != contact.EditToken) &&
				!(User == null || !User.IsInRole("Administrator")))
			{
				return Unauthorized();
			}

            var newContact = new Data.Contact
            {
                Id = contact.Id,
                EmailAddress = contact.EmailAddress,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Report = report
            };

			db.Contacts.Add(newContact);
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

            var contact = await db.Contacts.FindAsync(key);

            if (contact == null)
			{
				return NotFound();
			}

            var report = db.Reports.Where(r => r.Id == contact.Report.Id).FirstOrDefault();
            var newContact = new Data.Contact
            {
                Id = contact.Id,
                EmailAddress = contact.Name,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Report = report
            };

            //patch.Patch(newContact);

			try
			{

				db.Contacts.Add(newContact);
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ContactExists(key))
				{
					return NotFound();
				}
				throw;
			}

			return Updated(contact);
		}

		// DELETE odata/Contact(5)
		public async Task<IHttpActionResult> Delete([FromODataUri] int key)
		{
			Data.Contact contact = await db.Contacts.FindAsync(key);
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