using System.Collections.Generic;
using System.Linq;


namespace PurchaseData.DataModel
{
    public partial class ufnGetReqGroupByCost_Result
    {
        public List<ufnGetReqGroupByCost_Result> GetList(int status)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                return contextDB.ufnGetReqGroupByCost(status).ToList();
            }
        }
    }
}
