using System;
using System.Collections.Generic;
using System.Data;

using PurchaseData.DataModel;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.PerfilAbstract;

namespace PurchaseDesktop.Interfaces
{
    public interface IPerfilActions
    {
        DataTable GetVista(Users userDB);
        iGrid SetGridBeging(iGrid grid, List<OrderStatus> status);
        void GuardarCambios(int wait);
        void InsertOrderHeader(Companies company, OrderType type, Users userDB);
        void DeleteOrderHeader(int id);
        void DeleteOrderDetail(OrderHeader header, int idDetailr, Users userDB);
        void UpdateOrderHeader(Users userDB, int id, object valor, string campo);
        void UpdateRequisitionHeader(Users userDB, int id, object valor, string campo);
        Transactions InsertTranHistory(OrderHeader order, Users userDB, Enum @evento);
        DataTable GetVistaSuppliers();

    }
}
