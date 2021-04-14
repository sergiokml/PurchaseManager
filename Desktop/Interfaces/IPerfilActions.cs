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
        DataTable GetVista(OrderUsers userDB);
        iGrid SetGridBeging(iGrid grid, List<OrderStatus> status);
        void GuardarCambios(int wait);
        void InsertOrderHeader(OrderCompanies company, OrderType type, OrderUsers userDB);
        void DeleteOrderHeader(int id);
        void UpdateOrderHeader(OrderUsers userDB, int id, object field, string prop);
        OrderTransactions InsertTranHistory(OrderHeader order, OrderUsers userDB, Enum @evento);
        DataTable GetVistaSuppliers();

    }
}
