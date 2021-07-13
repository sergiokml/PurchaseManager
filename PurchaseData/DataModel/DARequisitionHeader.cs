using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class RequisitionHeader
    {
        public int GetListByStatus()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB
                    .RequisitionHeader
                    .Where(c => c.StatusID == 2)
                    .Count();
            }
        }

        public RequisitionHeader GetById(int id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                //TODO EAGER LOADING - CARGA ANSIOSA
                //return contextDB
                //    .RequisitionHeader
                //    .Include(c => c.RequisitionDetails)
                //    .Single(c => c.RequisitionHeaderID == id);

                //TODO EXPLICIT LOADING - CARGA EXPLICITA
                var po = contextDB
                     .RequisitionHeader
                     .Find(id);

                contextDB.Entry(po).Collection(s => s.RequisitionDetails).Load();
                contextDB.Entry(po).Collection(s => s.Transactions).Load();
                foreach (var item in po.RequisitionDetails)
                {
                    contextDB.Entry(item).Reference(s => s.Accounts).Load();
                }
                contextDB.Entry(po).Collection(s => s.Attaches).Load();
                return po;

            }

        }
    }
}
