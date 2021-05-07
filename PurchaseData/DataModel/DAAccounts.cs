using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class Accounts
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
