using System;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.WebApi
{
	public class Report
	{
        public Report()
        {
            Created = DateTimeOffset.Now;
            IsVisible = true;
            CurrentStatus = Status.Open;
        }

		public int Id { get; set; }
        public bool IsVisible { get; set; }
        public DateTimeOffset Created { get; set; }
        public Status CurrentStatus { get; set; }
        
        [Required]
		public string Description { get; set; }

        [Required]
		public string Location { get; set; }

        [Required]
        public string Category { get; set; }
	}
}