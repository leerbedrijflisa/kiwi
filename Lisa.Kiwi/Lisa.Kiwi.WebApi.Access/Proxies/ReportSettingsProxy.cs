﻿using Default;
using System.Data.Services.Client;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportSettingsProxy : Client
    {

        // Get an entire entity set.
        public IQueryable<ReportSettings> GetReportSettings()
        {
            return Container.ReportSettings;
        }

        //Edit entity
        public void UpdateVisibility(int ReportID, bool Visibility)
        {
            var reportSettings = Container.ReportSettings.Where(rs => rs.Report == ReportID).FirstOrDefault();
            if(reportSettings != null) 
            {
                reportSettings.Visible = Visibility;
                Container.UpdateObject(reportSettings);
                Container.SaveChangesAsync();
            }

        }
    }
}
