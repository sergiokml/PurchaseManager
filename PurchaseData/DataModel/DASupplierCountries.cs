using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class SupplierCountries
    {
        public List<SupplierCountries> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.SupplierCountries.ToList();
            }
        }

    }
}
