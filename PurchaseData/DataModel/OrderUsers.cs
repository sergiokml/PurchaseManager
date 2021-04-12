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
    
    public partial class OrderUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderUsers()
        {
            this.OrderTransactions = new HashSet<OrderTransactions>();
        }
    
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> LastVisit { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProfileID { get; set; }
        public string CostID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTransactions> OrderTransactions { get; set; }
        public virtual UserCosts UserCosts { get; set; }
        public virtual UserProfiles UserProfiles { get; set; }
    }
}
