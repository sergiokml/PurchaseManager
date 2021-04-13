using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderCompanies
    {
        public List<OrderCompanies> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.OrderCompanies.ToList();
            }
        }
    }
}
