namespace PurchaseData.DataModel
{
    public partial class OrderHeader
    {
        public OrderHeader GetById(int id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.OrderHeader.Find(id);
            }
        }

    }
}
