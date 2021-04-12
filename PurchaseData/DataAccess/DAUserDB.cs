using System.Collections.Generic;
using System.Linq;

namespace PurchaseCtrl.DataBase.DataAccess
{
    public partial class UserDB
    {
        public List<UserDB> GetList()
        {
            using (var contextDB = new en())
            {
                return contextDB.UserDBs.ToList();
            }
        }
        public UserDB GetUserDB(string rut)
        {
            using (var contextDB = new PurchaseCtrlEntities())
            {
                var user = contextDB.UserDBs.Find(rut);
                contextDB.Entry(user).Reference(c => c.UserProfile).Load();
                return user;
            }
        }

    }
}
