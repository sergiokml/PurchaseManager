using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class RequisitionHeader
    {
        public int GetListByStatus()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.RequisitionHeader.Where(c => c.StatusID == 2).Count();
            }
        }

        public RequisitionHeader GetById(int id)
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.RequisitionHeader.Find(id);
            }
        }
    }
}
