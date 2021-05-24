using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class RequisitionDetails
    {
        public List<RequisitionDetails> GetListByID(int id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                var lista = contextDB.RequisitionDetails.Where(c => c.HeaderID == id).ToList();
                foreach (var item in lista)
                {
                    contextDB.Entry(item).Reference(c => c.Accounts).Load();
                }
                return lista;
            }
        }

    }
}
