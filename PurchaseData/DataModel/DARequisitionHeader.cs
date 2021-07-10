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


                //    //contextDB.Entry(po).Reference(s => s.RequisitionDetails).Load(); // EN ESTE CASO NO ES REFERENCE!
                contextDB.Entry(po).Collection(s => s.RequisitionDetails).Load(); // ESTE CASO ES COLLECTION!
                return po;

            }

        }
    }
}
