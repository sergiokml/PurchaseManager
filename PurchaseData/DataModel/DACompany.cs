﻿using System.Collections.Generic;
using System.Linq;

namespace PurchaseData.DataModel
{
    public partial class Companies
    {
        public List<Companies> GetList()
        {
            using (var contextDB = new PurchaseManagerContext())
            {
                return contextDB.Companies.ToList();
            }
        }
    }
}
