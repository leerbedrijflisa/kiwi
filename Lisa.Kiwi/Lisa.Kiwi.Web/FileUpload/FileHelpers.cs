using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public partial class FileHelpers
    {
        /// <summary>
        /// Returns the Url of the given File object
        /// </summary>
        public static string GetFileUrl(WebApi.File file)
        {
            _container = MvcApplication.GetBlobContainer(file.Container);

            return _container.GetBlockBlobReference(file.Key).Uri.ToString();
        }

        public static bool IsSize(HttpPostedFileBase file, int maxSize)
        {
            return file.ContentLength <= maxSize;
        }

        public static bool IsMimes(HttpPostedFileBase file, string[] mimes)
        {
            if (file != null && mimes.Any())
            {
                return mimes.All(mime => file.ContentType.Contains(mime));
            }

            return true;
        }

        private static CloudBlobContainer _container;
    }

    
}
