using Default;
using System;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class RemarkProxy
    {
        // Get an entire entity set.
        public static IQueryable<Status> GetStatuses()
        {
            return Client.Container.Status;
        }

        //Create a new entity
        public static void AddRemark(Remark remark)
        {
            Client.Container.AddToRemark(remark);
            Client.Container.SaveChanges();
            Client.Container = new Container(Client.BaseUri);
        }
    }
}
