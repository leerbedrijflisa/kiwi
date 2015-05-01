using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Lisa.Kiwi.WebApi
{
    //public class QueueConfig
    //{
    //    public CloudQueue BuildQueue()
    //    {
    //        var storageAccount = CloudStorageAccount.Parse(
    //            CloudConfigurationManager.GetSetting("StorageConnectionString"));

    //        var queueClient = storageAccount.CreateCloudQueueClient();
    //        var queue = queueClient.GetQueueReference("myqueue");

    //        queue.CreateIfNotExists();

    //        return queue;
    //    }
    //}
}