using System.Collections.Generic;
using System.Data;

using PurchaseData.DataModel;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Interfaces
{
    public interface IPerfilActions
    {
        //! Vistas Formularios
        DataTable GetVistaFPrincipal(Users userDB);
        DataTable GetVistaFSupplier();
        DataTable GetVistaAttaches(int IdItem);
        DataTable GetVistaDetalles(int IdItem);

        //! Details
        List<RequisitionDetails> GetDetailsRequisition(int id);
        List<OrderDetails> GetDetailsOrder(int id);


        List<Attaches> GetAttaches(int id);
        void InsertAttach(int id, Attaches att, Users userDB);
        void GuardarCambios(int wait);
        //void InsertOrderHeader(Companies company, OrderType type, Users userDB);
        void InsertItemHeader(Companies company, OrderType type, Users userDB);
        void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem);
        void DeleteOrderHeader(int id);
        void DeleteRequesitionHeader(int id);
        void DeleteDetail(int idHeader, int idDetailr, Users userDB);
        void DeleteAttache(int id, Users userDB, Attaches item);
        void UpdateItemHeader(Users userDB, int id, object valor, string campo);

        void GetFunciones();

        //todo ACA LAS FUNCIONES DEBEN SER GENERICAS!!!!
        //TODO POR ALGO SE DECLARA VACIO EL MÉTODO.
    }
}
