namespace PurchaseData.DataModel
{
    public partial class Suppliers
    {
        public Suppliers GetByID(string id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Suppliers.Find(id);
            }
        }
    }
}
