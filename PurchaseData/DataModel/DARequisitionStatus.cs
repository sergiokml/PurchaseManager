using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class RequisitionStatus
    {
        public List<RequisitionStatus> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.RequisitionStatus.ToList();
            }
        }
    }
}
