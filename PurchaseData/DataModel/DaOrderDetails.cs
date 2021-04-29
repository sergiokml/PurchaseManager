using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderDetails
    {

        public List<OrderDetails> GetList(int id)
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.OrderDetails.Where(c => c.OrderHeaderID == id).ToList();
            }
        }

        public void AddDetail(OrderDetails orderDetails, OrderHeader orderHeader)
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                var pr = contextDB.OrderHeader.Find(orderHeader.OrderHeaderID);
                pr.OrderDetails.Add(orderDetails);
                contextDB.SaveChanges();
            }
        }

        public void BorrarDetail(OrderHeader orderHeader, int orderDetails, Transactions tran)
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                var detail = contextDB.OrderDetails.Find(orderDetails, orderHeader.OrderHeaderID);
                contextDB.OrderDetails.Remove(detail);
                contextDB.OrderHeader.Attach(orderHeader).Transactions.Add(tran);
                contextDB.SaveChanges();
            }
        }

    }
}
