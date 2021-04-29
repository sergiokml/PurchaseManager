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
    
    public partial class Suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            this.OrderHeader = new HashSet<OrderHeader>();
        }
    
        public string SupplierID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public byte TypeAccount { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string BankID { get; set; }
        public string CountryID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
        public virtual SupplierCountries SupplierCountries { get; set; }
    }
}
