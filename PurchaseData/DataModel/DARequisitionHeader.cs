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
    }
}
