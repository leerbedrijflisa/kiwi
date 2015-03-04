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
        public string Category { get; set; }
        public bool IsVisible { get; set; }
        public DateTimeOffset Created { get; set; }
        public Status CurrentStatus { get; set; }
		public string Description { get; set; }

        #region FirstAid
        public bool? UnConscious { get; set; }
        #endregion

        #region Drugs

        #endregion



        public Contact Contact { get; set; }
	    public Vehicle Vehicle { get; set; }
	}
}