using System.Collections.Generic;
using System.Data;

using PurchaseCtrl.DataBase.DataAccess;
using PurchaseCtrl.Desktop.Profiles;

using TenTec.Windows.iGridLib;

namespace PurchaseCtrl.Desktop.Interfaces
{
    public interface IPerfilActions
    {
        // UserDB UserDB { get; set; }
        // List<OrderStatu> OrderStatus { get; }
        DataTable GetVista(UserDB userDB);
        iGrid SetGridBeging(iGrid grid, List<OrderStatu> status);
        void GuardarCambios(int wait);
        void InsertOrderHeader(Company company, PerfilAbstract.OrderType type, UserDB userDB);
        //void UpdateOrderHeader(int id, string field);
        void UpdateOrderHeader(UserDB userDB, int id, object field, string prop);
        TranHistory InsertTranHistory(OrderHeader order, string evento, UserDB userDB);


    }
}
