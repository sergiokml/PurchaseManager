namespace PurchaseData.DataModel
{
    public partial class Suppliers
    {
        public Suppliers GetList(string id)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Suppliers.Find(id);
            }
        }
    }
}
