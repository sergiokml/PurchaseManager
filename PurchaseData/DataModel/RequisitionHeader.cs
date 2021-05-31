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
    
    public partial class RequisitionHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequisitionHeader()
        {
            this.RequisitionDetails = new HashSet<RequisitionDetails>();
            this.Attaches = new HashSet<Attaches>();
            this.Transactions = new HashSet<Transactions>();
        }
    
        public int RequisitionHeaderID { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public string CompanyID { get; set; }
        public byte StatusID { get; set; }
        public string Code { get; set; }
        public string TypeDocumentHeader { get; set; }
    
        public virtual Companies Companies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequisitionDetails> RequisitionDetails { get; set; }
        public virtual RequisitionStatus RequisitionStatus { get; set; }
        public virtual TypeDocument TypeDocument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attaches> Attaches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
