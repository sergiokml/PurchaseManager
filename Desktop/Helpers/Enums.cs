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
            //! Solamente eventos que son asociados a Transacciones, o sea para documentos.
            //! No mantención de tablas.
            CREATE_PR,
            UPDATE_PR,
            DELETE_PR,
            CREATE_PO,
            UPDATE_PO,
            DELETE_PO,
            INSERT_DETAIL,
            DELETE_DETAIL,
            UPDATE_DETAIL,
            INSERT_ATTACH,
            DELETE_ATTACH,
            UPDATE_ATTACH,
            INSERT_HITO,
            DELETE_HITO,
            UPDATE_HITO,
            INSERT_NOTE,
            DELETE_NOTE,
            UPDATE_NOTE
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

        public enum TypeAccount
        {
            Corriente = 1,
            Vista = 2,
            Ahorros = 3
        }
    }
}
