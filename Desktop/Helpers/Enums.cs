using System;

namespace PurchaseDesktop.Helpers
{
    public partial class HFunctions
    {
        public enum EventUserPR
        {
            CREATE_PR,
            UPDATE_PR,
            DELETE_DETAIL,
            INSERT_DETAIL,
            INSERT_ATTACH,
            DELETE_ATTACH,
            UPDATE_PO,
            CREATE_PO
        }
        public enum OrderType
        {
            Materiales = 1,
            Servicios = 2,
            SubContratos = 3
        }



        [Flags]
        public enum TypeDocumentHeader
        {
            PR = 1,
            PO = 2
        }
    }
}
