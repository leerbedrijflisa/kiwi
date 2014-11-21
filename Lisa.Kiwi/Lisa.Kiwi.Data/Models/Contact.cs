using System;

namespace Lisa.Kiwi.Data
{
	public class Contact
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public int? StudentNumber { get; set; }
		public virtual Report Report { get; set; }
		public Guid EditToken { get; set; }
	}
}