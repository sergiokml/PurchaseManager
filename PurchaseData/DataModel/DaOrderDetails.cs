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

        public void BorrarDetail(OrderHeader orderHeader, int orderDetails)
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                var pr = contextDB.OrderHeader.Find(orderHeader.OrderHeaderID);
                pr.OrderDetails.Remove(new OrderDetails() { DetailID = orderDetails });
                contextDB.SaveChanges();
            }
        }

    }
}
