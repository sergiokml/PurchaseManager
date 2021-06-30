using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class OrderNotes
    {
        public OrderNotes GetByID(int noteID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderNotes.Where(c => c.OrderNoteID == noteID).Single();
            }
        }

        public List<OrderNotes> GetListByID(int HeaderID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderNotes.Where(c => c.OrderHeaderID == HeaderID).ToList();
            }
        }
    }
}
