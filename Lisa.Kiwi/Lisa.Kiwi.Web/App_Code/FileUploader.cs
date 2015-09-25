using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Lisa.Kiwi.Web
{
    public class FileUploader
    {
        public FileUploader(HttpPostedFileBase file, string container)
        {
            _file = file;
            _containerName = container;
            _extension = Path.GetExtension(file.FileName);
            _contentType = file.ContentType;
            _container = MvcApplication.GetBlobContainer(container);
            _key = string.Format("File-{0}{1}", Guid.NewGuid(), _extension);
        }

        public FileUploader(HttpPostedFileBase file, WebApi.File fileEntity)
        {
            _file = file;
            _containerName = fileEntity.Container;
            _extension = Path.GetExtension(_file.FileName);
            _contentType = fileEntity.ContentType;
            _container = MvcApplication.GetBlobContainer(_containerName);
            _key = fileEntity.Key;
        }

        public WebApi.File GetFileEntity()
        {
            // Create new database object to reference the blob from
            var fileEntity = new WebApi.File
            {
                Name = _file.FileName.Replace(_extension ?? "", ""),
                ContentLength = _file.ContentLength,
                ContentType = _contentType,
                Key = _key,
                Container = _containerName
            };

            return fileEntity;
        }

        public bool IsSize(int maxSize)
        {
            return _file.ContentLength <= maxSize;
        }

        public bool IsSmallerThanDimensions(Stream imageStream, int maxHeight, int maxWidth)
        {
            using (var img = Image.FromStream(imageStream))
            {
                if (img.Width <= maxWidth && img.Height <= maxHeight) return true;
            }
            return false;
        }

        public async Task UploadFile()
        {
            using (var fileStream = _file.InputStream)
            {
                if (_contentType.Contains("image")) 
                {
                    await UploadImagesResized(fileStream);
                }
                await UploadStreamToStorage(fileStream, _contentType, _key);
            }
        }

        private async Task UploadImagesResized(Stream imageStream)
        {
            foreach (var s in ImageHelpers.ImageSizes.List)
            {
                // Only upload additional files if the image is larger than the target size
                if (ImageHelpers.IsLargerThanDimensions(imageStream, Convert.ToInt32(s.Split('/').Last())))
                {
                    var keyGuid = _key.Split('.').First();
                    var keyExtension = _key.Split('.').Last();
                    var keySizeIdentifier = s.Split('/').First();
                    var maxPixelSize = int.Parse(s.Split('/').Last());

                    // Format key with size identifier included
                    var key = string.Format("{0}_{1}.{2}", keyGuid, keySizeIdentifier, keyExtension);

                    // Create a new copy of the momerystream. Copying ensures there won't be any stream position issues
                    imageStream.Position = 0;
                    var stream = new MemoryStream();
                    imageStream.CopyTo(stream);
                    stream.Position = 0;

                    // Resize and upload image
                    var resizedstream = ImageHelpers.ResizeImage(stream, maxPixelSize);
                    await UploadStreamToStorage(resizedstream, _contentType, key);
                }
            }   
        }

        private async Task UploadStreamToStorage(Stream stream, string contentType, string key)
        {
            // Set the file stream postion to the start of the stream
            _file.InputStream.Position = 0;

            // Upload original file
            var block = _container.GetBlockBlobReference(key);
            block.Properties.ContentType = contentType;
            await block.UploadFromStreamAsync(stream);
        }

        private string _key;
        private string _extension;
        private string _contentType;
        private string _containerName;
        private HttpPostedFileBase _file;
        private CloudBlobContainer _container;
    }
}
