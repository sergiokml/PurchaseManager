using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderDelivery
    {
        public OrderDelivery GetByID(int deliveryID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderDelivery.Where(c => c.DeliveryID == deliveryID).Single();
            }
        }

        public List<OrderDelivery> GetListByID(int HeaderID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderDelivery.Where(c => c.OrderHeaderID == HeaderID).ToList();
            }
        }
    }
}
