using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class Medidas
    {
        public List<Medidas> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Medidas.ToList();
            }
        }
    }
}
