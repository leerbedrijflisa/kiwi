using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lisa.Kiwi.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public static string GetApiUrl()
        {
            string url = null;

#if DEBUG
            if (FiddlerAvailable())
            {
                url = WebConfigurationManager.AppSettings["WebApiFiddlerUrl"];
            }
#endif
            if (string.IsNullOrEmpty(url))
            {
                url = WebConfigurationManager.AppSettings["WebApiUrl"];
            }
            return url;


        }

        public static CloudBlobClient GetBlobStorageClient()
        {
            return CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString")).CreateCloudBlobClient();
        }

        public static CloudBlobContainer GetBlobContainer(string containerName)
        {
            var container = GetBlobStorageClient().GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            return container;
        }

        public static CloudBlobContainer GetBlobContainer(CloudBlobClient blobClient, string containerName)
        {
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            return container;
        }

        private static bool FiddlerAvailable()
        {
            return Process.GetProcesses()
                .Any(process => process.ProcessName.Contains("Fiddler"));
        }
    }
}