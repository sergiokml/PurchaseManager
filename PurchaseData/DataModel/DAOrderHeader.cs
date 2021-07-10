namespace PurchaseData.DataModel
{
    public partial class OrderHeader
    {
        public OrderHeader GetById(int id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                //return contextDB
                //    .OrderHeader
                //    .Include(c => c.OrderStatus)
                //    .Single(x => x.OrderHeaderID == id);
                var po = contextDB
                    .OrderHeader
                    .Find(id);
                // TODO ESTE CASO ES REFERENCIS, PORQUE ES 1 OBJETO, NO VARIOS.
                contextDB.Entry(po).Reference(c => c.OrderStatus).Load();
                return po;
            }
        }

    }
}
