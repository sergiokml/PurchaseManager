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
                //contextDB.Entry(po).Reference(c => c.OrderStatus).Load(); // Status viene desde las vistas
                contextDB.Entry(po).Collection(s => s.OrderDetails).Load();
                foreach (var item in po.OrderDetails)
                {
                    contextDB.Entry(item).Reference(s => s.Medidas).Load();
                }
                contextDB.Entry(po).Collection(s => s.Transactions).Load();
                contextDB.Entry(po).Collection(s => s.Attaches).Load(); //TODO ESTO ESTABA COMENTADO... ¿POR QUÉ?
                contextDB.Entry(po).Collection(s => s.OrderHitos).Load();

                return po;
            }
        }

    }
}
