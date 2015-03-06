using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.App_GlobalResources.Resources;

namespace Lisa.Kiwi.Web.Models
{
	public enum StatusNameMetadata
	{
		[Display(Name = "StatusOpen", ResourceType = typeof (DisplayNames))] Open,
		[Display(Name = "StatusSolved", ResourceType = typeof (DisplayNames))] Solved,
		[Display(Name = "StatusInProgress", ResourceType = typeof (DisplayNames))] InProgress,
		[Display(Name = "StatusTransferred", ResourceType = typeof (DisplayNames))] Transferred
	}
}