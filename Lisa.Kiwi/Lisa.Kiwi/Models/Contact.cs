using System;

namespace Lisa.Kiwi.WebApi
{
	public class Contact
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public int? StudentNumber { get; set; }
        public int Report { get; set; }
		public Guid EditToken { get; set; }
	}
}