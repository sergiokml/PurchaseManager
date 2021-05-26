namespace PurchaseData.DataModel
{
    public partial class Attaches
    {
        public Attaches GetByID(int attachID)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Attaches.Find(attachID);
            }
        }
    }
}
