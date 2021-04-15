﻿using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel

{
    public partial class OrderUsers
    {
        //! login
        public List<OrderUsers> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.OrderUsers.ToList();
            }
        }


    }
}
