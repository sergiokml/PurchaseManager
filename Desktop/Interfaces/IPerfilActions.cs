using System.Data;

using PurchaseData.DataModel;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Interfaces
{
    public interface IPerfilActions
    {
        //! Propiedades
        Users CurrentUser { get; set; }

        //! Vistas 
        DataTable VistaFPrincipal();
        DataRow GetDataRow(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFAdjuntos(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFHitos(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFNotes(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaDelivery(TypeDocumentHeader headerTD, int headerID);

        //! Insert
        void InsertPRHeader(RequisitionHeader item);
        void InsertPOHeader(OrderHeader item);
        void InsertDetail<T>(T item, object headerID); // Tabla 1:M => para PR y PO
        void InsertAttach(Attaches item, int headerID); // Tabla M:M => para PR y PO
        int InsertSupplier(Suppliers item);
        void InsertHito(OrderHitos item, int headerID); // Tabla 1:M pero solo para PO
        void InsertNote(OrderNotes item, int headerID);
        void InsertDelivery(OrderDelivery item, int headerID); // Tabla 1:M pero solo para PO



        //! Update
        void UpdateItemHeader<T>(TypeDocumentHeader headerTD, T item);
        void UpdateDetail<T>(T item, object header);
        void UpdateAttaches<T>(T item, int headerID, int attachID);
        void UpdateHito(OrderHitos item, int headerID);
        void UpdateNote(OrderNotes item, int headerID);
        void UpdateSupplier(Suppliers item);



        //! Delete
        void DeleteItemHeader(TypeDocumentHeader headerTD, int headerID);
        void DeleteDetail<T>(T item, int detailID); // Tabla 1:M
        void DeleteAttach(int headerID, int attachID);
        int DeleteSupplier(string headerID);
        int DeleteHito(int headerID, int hitoID);
        int DeleteNote(int headerID, int noteID);
        int DeleteDelivery(int headerID, int deliverID);

    }
}
