using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
    public enum StatusNameMetadata
    {
        [Display(Name = "StatusOpen", ResourceType = typeof(Resources.DisplayNames))]
        Open,
        [Display(Name = "StatusSolved", ResourceType = typeof(Resources.DisplayNames))]
        Solved,
        [Display(Name = "StatusInProgress", ResourceType = typeof(Resources.DisplayNames))]
        InProgress,
        [Display(Name = "StatusTransferred", ResourceType = typeof(Resources.DisplayNames))]
        Transferred
    }
}