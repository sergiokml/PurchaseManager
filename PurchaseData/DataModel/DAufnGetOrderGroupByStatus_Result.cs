using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class ufnGetOrderGroupByStatus_Result
    {
        public List<ufnGetOrderGroupByStatus_Result> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.ufnGetOrderGroupByStatus().ToList();
            }
        }
    }
}
