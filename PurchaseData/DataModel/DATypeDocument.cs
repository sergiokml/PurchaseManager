using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class TypeDocument
    {
        public List<TypeDocument> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.TypeDocument.ToList();
            }
        }
    }
}
