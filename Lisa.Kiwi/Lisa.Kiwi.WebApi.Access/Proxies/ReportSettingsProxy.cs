using Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportSettingsProxy
    {

        // Get an entire entity set.
        public static IQueryable<ReportSettings> GetReportSettings()
        {
            return Client.Container.ReportSettings;
        }

        //Edit entity
        public static void AddSettings(ReportSettings reportSettings)
        {
            //Client.container.Status.Context.AddObject("Status", status); //CreateStatus(status);
            Client.Container.UpdateObject(reportSettings);
            //Client.Container.AddToReportSettings(reportSettings);
            Client.Container.SaveChanges();
            Client.Container = new Container(Client.BaseUri);
        }
    }
}
