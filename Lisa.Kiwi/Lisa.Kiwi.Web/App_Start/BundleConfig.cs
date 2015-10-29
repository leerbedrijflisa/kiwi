using System.Web.Optimization;

namespace Lisa.Kiwi.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Scripts/bug-report.js")
                .Include("~/Scripts/messageReciever.js")
                .Include("~/Scripts/header-resizer.js")
                .Include("~/Scripts/headerCategory.js"));

            bundles.Add(new ScriptBundle("~/bundles/frameScripts")
                .Include("~/Scripts/messageSender.js")
                .Include("~/Scripts/headerCategory.js"));

            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/Content/reporter.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}