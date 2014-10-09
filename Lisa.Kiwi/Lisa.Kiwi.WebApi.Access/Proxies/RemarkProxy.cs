using System;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class RemarkProxy
    {
        // Get an entire entity set.
        public static IQueryable<Status> GetStatuses()
        {
            return Client.container.Status;
        }

        //Create a new entity
        public static void AddRemark(Remark remark)
        {
            Client.container.AddToRemark(remark);
            Client.container.SaveChanges();
            Client.container = new Container(Client.BaseUri);
        }
    }
}
