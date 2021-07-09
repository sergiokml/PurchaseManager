using System;

namespace PurchaseData.DataModel
{
    public partial class vRequisitionByMinTransaction
    {
        public string CurrencyID { get; set; }
        public string SupplierID { get; set; }
        public Nullable<decimal> Net { get; set; }
        public Nullable<decimal> Exent { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> Discount { get; set; }

    }
}
