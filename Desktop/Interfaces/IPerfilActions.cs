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
        DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFAdjuntos(TypeDocumentHeader headerTD, int headerID);
        DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID);


        //! Insert
        void InsertItemHeader(Companies company, DocumentType type);
        void InsertDetail<T>(T item, int headerID) where T : class; // Tabla 1:M
        void InsertAttach(Attaches item, int headerID); // Tabla M:M


        //! Update
        void UpdateItemHeader<T>(T item, int headerID);


        //! Delete
        void DeleteItemHeader(TypeDocumentHeader headerTD, int headerID);
        void DeleteDetail(int headerID, int detailID); // Tabla 1:M
        void DeleteAttach(int headerID, int attachID);


        //! Auxiliar => sirve para UNIR el rContext + Clase Abastracta.       
        void SetResultFunctions();





































        //! Types Header        
        // DataRow Current { get; set; }


        //OrderHeader DocumentPO { get; set; }
        //RequisitionHeader DocumentPR { get; set; }



        ////! Properties
        //List<OrderDetails> DetailsPO { get; set; }
        //List<RequisitionDetails> DetailsPR { get; set; }

        //DataTable TableDetails { get; set; }

        ////! Vistas Formularios
        //DataTable GetVistaFPrincipal();
        //DataTable GetVistaFSupplier();
        //DataTable GetVistaAttaches(int IdItem);
        //DataTable GetVistaDetalles();

        ////! Details
        //void GetDetailsRequisition(int id);


        //List<OrderDetails> GetDetailsOrder(int id);

        ////! Controles desde Abastractas
        //iGDropDownList GetStatusItem(DataRow dataRow);
        //ContextMenuStrip GetMenuStrip();

        //List<Attaches> GetAttaches(int id);
        //void InsertAttach(int id, Attaches att, Users userDB);
        ////void InsertOrderHeader(Companies company, OrderType type, Users userDB);
        //void InsertItemHeader(Users userDB);
        //void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem);

        //void DeleteItemHeader();
        //void DeleteDetail(Users user);
        //void DeleteAttache(int id, Users userDB, Attaches item);



        ///// <summary>
        ///// UPDATE => Header de 1 Documento.
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="valor"></param>
        ///// <param name="campo"></param>
        //void UpdateItemHeader(Users userDB, object valor, string campo);

        //void GetFunciones();

        ////todo ACA LAS FUNCIONES DEBEN SER GENERICAS!!!!
        ////TODO POR ALGO SE DECLARA VACIO EL MÉTODO.
    }
}
