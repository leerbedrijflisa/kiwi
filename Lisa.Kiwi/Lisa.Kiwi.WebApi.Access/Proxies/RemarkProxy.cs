using Default;
using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class RemarkProxy : Client
    {
        // Get an entire entity set.
        public IQueryable<Remark> GetRemarks()
        {
            return Container.Remark;
        }

        //Create a new entity
        public void AddRemark(Remark remark)
        {
            Container.AddToRemark(remark);
            Container.SaveChanges();
        }
    }
}
