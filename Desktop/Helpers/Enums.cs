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
            UPDATE_NOTE,
            INSERT_DELIVERY,
            UPDATE_DELIVERY,
            DELETE_DELIVERY
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
        public enum MsgProceso
        {
            Informacion = 1,
            Warning = 2,
            Send = 3,
            Error = 4,
            Empty = 5
        }
    }
}
