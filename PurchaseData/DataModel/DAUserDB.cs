using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel

{
    public partial class Users
    {
        //! login
        public List<Users> GetList()
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Users.ToList();
            }
        }

        public List<Users> GetListByPerfil(string p)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.Users.Where(c => c.ProfileID == p).ToList();
            }
        }


    }
}
