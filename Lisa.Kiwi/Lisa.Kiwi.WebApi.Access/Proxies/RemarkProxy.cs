using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class RemarkProxy : Client
    {
        // Get an entire entity set.
        public IQueryable<Status> GetStatuses()
        {
            return Container.Status;
        }

        //Create a new entity
        public void AddRemark(Remark remark)
        {
            Container.AddToRemark(remark);
            Container.SaveChanges();
        }
    }
}
