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
    
    public partial class vRequisitionByMinTransaction
    {
        public int HeaderID { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public byte Type { get; set; }
        public byte StatusID { get; set; }
        public string UserID { get; set; }
        public string CostID { get; set; }
        public Nullable<System.DateTime> DateLast { get; set; }
        public string Event { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string NameBiz { get; set; }
        public string CompanyCode { get; set; }
        public string Status { get; set; }
        public string TypeDocumentHeader { get; set; }
        public Nullable<int> DetailsCount { get; set; }
        public string UserPO { get; set; }
        public string NameUserID { get; set; }
    }
}
