using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class Accounts
    {
        public List<Accounts> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Accounts.ToList();
            }
        }
    }
}
