using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class SupplierBanks
    {
        public List<SupplierBanks> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.SupplierBanks.ToList();
            }
        }

    }
}
