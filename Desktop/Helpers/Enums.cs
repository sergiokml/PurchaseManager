namespace PurchaseDesktop.Helpers
{
    public partial class HFunctions
    {
        public enum EventUserPR
        {
            CREATE_PR = 1,
            UPDATE_PR = 2,
            DELETE_DETAIL = 3,
            INSERT_DETAIL = 4,
            INSERT_ATTACH = 5,
            DELETE_ATTACH = 6,
            UPDATE_PO = 7,
            CREATE_PO = 8
        }
        public enum OrderType
        {
            Materiales = 1,
            Servicios = 2,
            SubContratos = 3
        }
    }
}
