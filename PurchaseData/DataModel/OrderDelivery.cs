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
    
    public partial class OrderDelivery
    {
        public int DeliveryID { get; set; }
        public int OrderHeaderID { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual OrderHeader OrderHeader { get; set; }
    }
}
