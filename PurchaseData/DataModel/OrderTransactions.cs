//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PurchaseData.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderTransactions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderTransactions()
        {
            this.OrderHeader = new HashSet<OrderHeader>();
        }
    
        public int TransactionID { get; set; }
        public string Event { get; set; }
        public string UserID { get; set; }
        public System.DateTime DateTran { get; set; }
        public byte StatuID { get; set; }
    
        public virtual OrderUsers OrderUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
    }
}
