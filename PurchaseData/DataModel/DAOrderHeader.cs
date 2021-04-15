namespace PurchaseData.DataModel
{
    public partial class OrderHeader
    {
        public OrderHeader GetById(int id)
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.OrderHeader.Find(id);
            }
        }

    }
}
