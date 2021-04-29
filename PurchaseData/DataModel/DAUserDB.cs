using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel

{
    public partial class Users
    {
        //! login
        public List<Users> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.Users.ToList();
            }
        }


    }
}
