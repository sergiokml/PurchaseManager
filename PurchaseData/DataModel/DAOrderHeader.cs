using System.Data.Entity;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderHeader
    {
        public OrderHeader GetById(int id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderHeader.Include(c => c.OrderStatus).FirstOrDefault(x => x.OrderHeaderID == id);
            }
        }

    }
}
