namespace PurchaseDesktop.Helpers
{
    public partial class HFunctions
    {
        public enum Perfiles
        {
            ADM,
            BAS,
            UPO,
            UPR,
            VAL
        }
        public enum Eventos
        {
            CREATE_PR,
            UPDATE_PR,
            DELETE_DETAIL,
            INSERT_DETAIL,
            INSERT_ATTACH,
            DELETE_ATTACH,
            UPDATE_PO,
            CREATE_PO,
            DELETE_PR,
            DELETE_PO,
            UPDATE_ATTACH
        }
        public enum DocumentType
        {
            Materiales = 1,
            Servicios = 2,
            SubContratos = 3
        }


        public enum TypeDocumentHeader
        {
            PR = 1,
            PO = 2
        }

        public enum TypeAttach
        {

            Public = 1,
            Private = 2
        }
    }
}
