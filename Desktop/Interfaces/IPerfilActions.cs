using System.Collections.Generic;
using System.Data;

using PurchaseData.DataModel;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Interfaces
{
    public interface IPerfilActions
    {
        //! Types Header
        TypeDocumentHeader TypeDocHeader { get; set; }
        DataRow Current { get; set; }
        OrderHeader DocumentPO { get; set; }
        RequisitionHeader DocumentPR { get; set; }



        //! Properties
        List<OrderDetails> DetailsPO { get; set; }
        List<RequisitionDetails> DetailsPR { get; set; }

        DataTable TableDetails { get; set; }

        //! Vistas Formularios
        DataTable GetVistaFPrincipal(Users userDB);
        DataTable GetVistaFSupplier();
        DataTable GetVistaAttaches(int IdItem);
        DataTable GetVistaDetalles();

        //! Details
        void GetDetailsRequisition(int id);


        List<OrderDetails> GetDetailsOrder(int id);

        //! Status
        iGDropDownList GetStatusItem(DataRow dataRow);


        List<Attaches> GetAttaches(int id);
        void InsertAttach(int id, Attaches att, Users userDB);
        //void InsertOrderHeader(Companies company, OrderType type, Users userDB);
        void InsertItemHeader(Users userDB);
        void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem);

        void DeleteItemHeader();
        void DeleteDetail(Users user);
        void DeleteAttache(int id, Users userDB, Attaches item);



        /// <summary>
        /// UPDATE => Header de 1 Documento.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="valor"></param>
        /// <param name="campo"></param>
        void UpdateItemHeader(Users userDB, object valor, string campo);

        void GetFunciones();

        //todo ACA LAS FUNCIONES DEBEN SER GENERICAS!!!!
        //TODO POR ALGO SE DECLARA VACIO EL MÉTODO.
    }
}
