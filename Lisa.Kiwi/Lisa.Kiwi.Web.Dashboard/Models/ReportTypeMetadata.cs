using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
    public enum ReportTypeMetadata
    {
        [Display(Name = "ReportTypeDrugs", ResourceType = typeof(Resources.DisplayNames))]
        Drugs,
        [Display(Name = "ReportTypeNuisance", ResourceType = typeof(Resources.DisplayNames))]
        Nuisance,
        [Display(Name = "ReportTypeVehicles", ResourceType = typeof(Resources.DisplayNames))]
        Vehicles,
        [Display(Name = "ReportTypeBurglary", ResourceType = typeof(Resources.DisplayNames))]
        Burglary,
        [Display(Name = "ReportTypeTheft", ResourceType = typeof(Resources.DisplayNames))]
        Theft,
        [Display(Name = "ReportTypeIntimidation", ResourceType = typeof(Resources.DisplayNames))]
        Intimidation,
        [Display(Name = "ReportTypeBullying", ResourceType = typeof(Resources.DisplayNames))]
        Bullying,
        [Display(Name = "ReportTypeDigital", ResourceType = typeof(Resources.DisplayNames))]
        Digital,
        [Display(Name = "ReportTypeFire", ResourceType = typeof(Resources.DisplayNames))]
        Fire
    }
}