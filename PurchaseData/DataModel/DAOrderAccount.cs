using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderAccounts
    {
        public List<Accounts> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.Accounts.ToList();
            }
        }
    }
}
