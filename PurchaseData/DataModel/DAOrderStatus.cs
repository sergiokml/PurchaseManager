using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderStatus
    {
        public List<OrderStatus> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderStatus.ToList();
            }
        }
    }
}
