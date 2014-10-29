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
        public void AddSettings(ReportSettings reportSettings)
        {
            Container.UpdateObject(reportSettings);
            Container.SaveChanges();
        }
    }
}
