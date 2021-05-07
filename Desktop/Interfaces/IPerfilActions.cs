using System.Collections.Generic;
using System.Data;

using PurchaseData.DataModel;

using static PurchaseDesktop.Helpers.PerfilAbstract;

namespace PurchaseDesktop.Interfaces
{
    public interface IPerfilActions
    {
        DataTable GetVista(Users userDB);
        List<RequisitionDetails> GetRequisitionDetails(int id);
        List<Attaches> GetAttaches(int id);
        void GuardarCambios(int wait);
        void InsertOrderHeader(Companies company, OrderType type, Users userDB);
        void InsertRequisition(Companies company, OrderType type, Users userDB);
        void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem);
        void DeleteOrderHeader(int id);
        void DeleteRequesitionHeader(int id);
        void DeleteOrderDetail(OrderHeader header, int idDetailr, Users userDB);
        void UpdateOrderHeader(Users userDB, int id, object valor, string campo);
        void UpdateRequisitionHeader(Users userDB, int id, object valor, string campo);
        DataTable GetVistaSuppliers();
        void CargarDashBoard();

    }
}
