using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderAccounts
    {
        public List<OrderAccounts> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.OrderAccounts.ToList();
            }
        }
    }
}
