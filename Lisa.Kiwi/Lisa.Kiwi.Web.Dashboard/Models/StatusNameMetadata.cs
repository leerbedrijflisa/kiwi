using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
	public enum StatusNameMetadata
	{
		[Display(Name = "StatusOpen", ResourceType = typeof (DisplayNames))] Open,
		[Display(Name = "StatusSolved", ResourceType = typeof (DisplayNames))] Solved,
		[Display(Name = "StatusInProgress", ResourceType = typeof (DisplayNames))] InProgress,
		[Display(Name = "StatusTransferred", ResourceType = typeof (DisplayNames))] Transferred
	}
}