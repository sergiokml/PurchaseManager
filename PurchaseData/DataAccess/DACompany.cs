using System.Collections.Generic;
using System.Linq;

namespace PurchaseCtrl.DataBase.DataAccess
{
    public partial class Company
    {
        public List<Company> GetList()
        {
            using (var contextDB = new PurchaseCtrlEntities())
            {
                return contextDB.Companies.ToList();
            }
        }
    }
}
