using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;

namespace Lisa.Kiwi.Web
{
    internal static class ImageHelpers
    {
        /// <summary>
        /// Checks if image is smaller than given dimensions.
        /// Returns false if the image is larger than given dimensions.
        /// </summary>
        public static bool IsSmallerThanDimensions(Stream imageStream, int maxHeight, int maxWidth)
        {
            using (var img = Image.FromStream(imageStream))
            {
                if (img.Width <= maxWidth && img.Height <= maxHeight) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if image is larger than given dimensions.
        /// Returns false if the image is smaller than given dimensions.
        /// </summary>
        public static bool IsLargerThanDimensions(Stream imageStream, int minsize)
        {
            using (var img = Image.FromStream(imageStream))
            {
                if (img.Width >= minsize || img.Height >= minsize) return true;
            }
            return false;
        }

        /// <summary>
        /// Resizes image to fit inside a rectangle of given size. Retains aspect ratio.
        /// </summary>
        public static Stream ResizeImage(Stream img, int maxSize)
        {
            var rawStream = img;
            using (rawStream)
            {
                rawStream.Position = 0;

                // Create bitmap decoder
                var decoder = BitmapDecoder.Create(
                rawStream,
                BitmapCreateOptions.PreservePixelFormat,
                BitmapCacheOption.None);

                // Decode single bitmap frame
                var frame = decoder.Frames[0];

                // Make sure the image doesn't get upscaled
                // if (frame.Height < maxSize && frame.Width < maxSize) return img;

                Double xRatio = frame.Width / maxSize;
                Double yRatio = frame.Height / maxSize;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(frame.Width / ratio);
                int nny = (int)Math.Floor(frame.Height / ratio);

                // Resize the bitmap frame
                var resizedFrame = new TransformedBitmap(
                    frame,
                    new ScaleTransform(
                        nnx / frame.Width * 96 / frame.DpiX,
                        nny / frame.Height * 96 / frame.DpiY,
                        0, 0));

                // Re-encode the bitmap to stream
                var stream = new MemoryStream();
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(resizedFrame));
                encoder.Save(stream);

                stream.Position = 0;
                return stream;
            }
        }

        /// <summary>
        /// Returns the Url of the given File object
        /// </summary>
        public static string GetImageUrl(WebApi.Models.File file)
        {
            _container = _blobClient.GetContainerReference(file.Container);
            _container.CreateIfNotExists();
            _container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            return _container.GetBlockBlobReference(file.Key).Uri.ToString();
        }

        public static string GetImageUrl(WebApi.Models.File file, string size)
        {
            var key = file.Key.Split('.').First();
            var extension = file.Key.Split('.').Last();
            var keyResized = string.Format("{0}_{1}.{2}", key, size.Split('/').First(), extension);

            _container = _blobClient.GetContainerReference(file.Container);
            _container.CreateIfNotExists();
            _container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            if (_container.GetBlockBlobReference(keyResized)
                .Exists())
            {
                return _container.GetBlockBlobReference(keyResized).Uri.ToString();
            }

            return _container.GetBlockBlobReference(file.Key).Uri.ToString();
        }

        public static class ImageSizes
        {
            public static string Thumbnail { get { return "thumb/150"; } }
            public static string InPage { get { return "inpage/800"; } }
            public static string FullSize { get { return "fullsize/1024"; } }
            public static List<string> List { get { return _list; } }

            private static List<string> _list = new List<string>
        {
            Thumbnail,
            InPage,
            FullSize
        };
        }

        private static readonly CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        private static readonly CloudBlobClient _blobClient = _storageAccount.CreateCloudBlobClient();
        private static CloudBlobContainer _container;
    }
}
