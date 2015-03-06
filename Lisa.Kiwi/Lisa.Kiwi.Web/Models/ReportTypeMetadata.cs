using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.App_GlobalResources.Resources;

namespace Lisa.Kiwi.Web.Models
{
	public enum ReportTypeMetadata
	{
		[Display(Name = "ReportTypeDrugs", ResourceType = typeof (DisplayNames))] Drugs,
		[Display(Name = "ReportTypeNuisance", ResourceType = typeof (DisplayNames))] Nuisance,
		[Display(Name = "ReportTypeVehicles", ResourceType = typeof (DisplayNames))] Vehicles,
		[Display(Name = "ReportTypeBurglary", ResourceType = typeof (DisplayNames))] Burglary,
		[Display(Name = "ReportTypeTheft", ResourceType = typeof (DisplayNames))] Theft,
		[Display(Name = "ReportTypeIntimidation", ResourceType = typeof (DisplayNames))] Intimidation,
		[Display(Name = "ReportTypeBullying", ResourceType = typeof (DisplayNames))] Bullying,
		[Display(Name = "ReportTypeDigital", ResourceType = typeof (DisplayNames))] Digital,
		[Display(Name = "ReportTypeFire", ResourceType = typeof (DisplayNames))] Fire
	}
}