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
    
    public partial class OrderHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderHeader()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
            this.OrderHitos = new HashSet<OrderHitos>();
            this.OrderNotes = new HashSet<OrderNotes>();
            this.Attaches = new HashSet<Attaches>();
            this.Transactions = new HashSet<Transactions>();
        }
    
        public int OrderHeaderID { get; set; }
        public string Description { get; set; }
        public Nullable<byte> Type { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> Net { get; set; }
        public Nullable<decimal> Exent { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public byte StatusID { get; set; }
        public string CurrencyID { get; set; }
        public string SupplierID { get; set; }
        public Nullable<int> RequisitionHeaderID { get; set; }
        public string CompanyID { get; set; }
        public string TypeDocumentHeader { get; set; }
    
        public virtual Currencies Currencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHitos> OrderHitos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderNotes> OrderNotes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attaches> Attaches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
