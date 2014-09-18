using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web.Reporting.Models
{
    public class OriginalReport
    {
        public OriginalReport() {
            Created = DateTime.Now;
        }

        public int Id { get; set; }
        [Required, Display(Name="Title"), StringLength(150, ErrorMessage="De titel mag niet langer zijn dan 150 karakters.")]
        public string Title { get; set; }
        [Required, Display(Name = "Locatie")]
        public string Location { get; set; }
        [Required, StringLength(19)]
        public string GuId { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }
        
        public DateTime Created {get;set;}
        [Required]
        public DateTime Time { get; set; }

        public enum Type
        {
            Drugs, Brand, verlast, Voertuigen, Inbraak, Diefstal, Intimidatie, Pesten, Internet
        }
        public enum Priority
        {
            Laag,
            Normaal,
            Hoog
        }
       
    }
}