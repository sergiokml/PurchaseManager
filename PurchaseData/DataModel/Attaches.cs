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
    
    public partial class Attaches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attaches()
        {
            this.OrderHeader = new HashSet<OrderHeader>();
            this.RequisitionHeader = new HashSet<RequisitionHeader>();
        }
    
        public int AttachID { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte Modifier { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequisitionHeader> RequisitionHeader { get; set; }
    }
}
