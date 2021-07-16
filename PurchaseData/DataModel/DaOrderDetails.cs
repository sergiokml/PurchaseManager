namespace PurchaseData.DataModel
{
    public partial class OrderDetails
    {
        //public List<OrderDetails> GetListByID(int HeaderID)
        //{
        //    using (var contextDB = new PurchaseManagerEntities())
        //    {
        //        var lista = contextDB.OrderDetails.Where(c => c.HeaderID == HeaderID).ToList();
        //        foreach (var item in lista)
        //        {
        //            contextDB.Entry(item).Reference(c => c.Accounts).Load();
        //            contextDB.Entry(item).Reference(c => c.Medidas).Load();
        //        }
        //        return lista;
        //    }
        //}

        //public OrderDetails GetByID(int DetailID)
        //{
        //    using (var contextDB = new PurchaseManagerEntities())
        //    {
        //        return contextDB.OrderDetails.Where(c => c.DetailID == DetailID).Single();
        //    }
        //}

        //public void AddDetail(OrderDetails orderDetails, OrderHeader orderHeader)
        //{
        //    //todo ESTO POR QUE NO ESTA EN EL PERFILPR??? 
        //    using (var contextDB = new PurchaseManagerContext())
        //    {
        //        var pr = contextDB.OrderHeader.Find(orderHeader.OrderHeaderID);
        //        pr.OrderDetails.Add(orderDetails);
        //        contextDB.SaveChanges();
        //    }
        //}

        //public void BorrarDetail(OrderHeader orderHeader, int orderDetails, Transactions tran)
        //{
        //    using (var contextDB = new PurchaseManagerContext())
        //    {
        //        var detail = contextDB.OrderDetails.Find(orderDetails, orderHeader.OrderHeaderID);
        //        contextDB.OrderDetails.Remove(detail);
        //        contextDB.OrderHeader.Attach(orderHeader).Transactions.Add(tran);
        //        contextDB.SaveChanges();
        //    }
        //}

    }
}
