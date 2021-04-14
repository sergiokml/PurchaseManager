using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class SupplierCountries
    {
        public List<SupplierCountries> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.SupplierCountries.ToList();
            }
        }

    }
}
