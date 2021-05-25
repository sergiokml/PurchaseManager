using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class Currencies
    {
        public List<Currencies> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Currencies.ToList();
            }
        }
    }
}
