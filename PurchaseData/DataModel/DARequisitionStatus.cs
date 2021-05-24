using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class RequisitionStatus
    {
        public List<RequisitionStatus> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.RequisitionStatus.ToList();
            }
        }
    }
}
