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
                contextDB.Entry(po).Reference(c => c.OrderStatus).Load();
                contextDB.Entry(po).Collection(s => s.OrderDetails).Load();
                contextDB.Entry(po).Collection(s => s.Transactions).Load();
                //contextDB.Entry(po).Collection(s => s.Attaches).Load();
                return po;
            }
        }

    }
}
