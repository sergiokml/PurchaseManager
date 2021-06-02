using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderHitos
    {
        public OrderHitos GetByID(int hitoID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderHitos.Where(c => c.HitoID == hitoID).Single();
            }
        }
    }
}
