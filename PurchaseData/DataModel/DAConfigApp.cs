using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class ConfigApp
    {
        public ConfigApp GetUnique()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.ConfigApp.Single();
            }
        }
    }
}
