using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class Attaches
    {
        public Attaches GetByID(int attachID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Attaches.Find(attachID);
            }
        }
        public List<Attaches> GetListByID(int id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                //return contextDB.Attaches.Where(c =>c.AttachID == id).ToList();
                return contextDB.RequisitionHeader.Find(id).Attaches.ToList();
            }
        }
    }
}
