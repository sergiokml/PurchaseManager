namespace PurchaseData.DataModel
{
    public partial class RequisitionDetails
    {
        //public List<RequisitionDetails> GetListByID(int id)
        //{
        //    using (var contextDB = new PurchaseManagerEntities())
        //    {
        //        var lista = contextDB.RequisitionDetails.Where(c => c.HeaderID == id).ToList();
        //        foreach (var item in lista)
        //        {
        //            contextDB.Entry(item).Reference(c => c.Accounts).Load();
        //            contextDB.Entry(item).Reference(c => c.Medidas).Load();
        //        }
        //        return lista;
        //    }
        //}
        //public RequisitionDetails GetByID(int DetailID)
        //{
        //    using (var contextDB = new PurchaseManagerEntities())
        //    {
        //        return contextDB.RequisitionDetails.Where(c => c.DetailID == DetailID).Single();
        //    }
        //}

    }
}
