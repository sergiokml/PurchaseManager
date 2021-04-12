using System.Collections.Generic;
using System.Data;
using System.Linq;

using PurchaseCtrl.DataBase.DataAccess;
using PurchaseCtrl.Desktop.Interfaces;
using PurchaseCtrl.Desktop.Utils;

using TenTec.Windows.iGridLib;

namespace PurchaseCtrl.Desktop.Profiles
{
    public class PerfilVAL : PerfilAbstract, IPerfilActions
    {
        private readonly PurchaseCtrlEntities rContext;
        //  public UserDB UserDB { get; set; }
        //  public List<OrderStatu> OrderStatus { get; }

        public PerfilVAL(PurchaseCtrlEntities rContext)
        {
            this.rContext = rContext;
        }

        public DataTable GetVista(UserDB userDB)
        {
            //  1   Pre PRequisition
            //  2   Active PRequisition
            //  3   Pre POrden
            //  4   Active POrder
            //  5   Valid POrder
            //  6   POrder on Supplier
            //  7   Agree by Supplier
            //  8   POrder in Process
            //  9   POrder Complete 
            var l = rContext.ViewOderByActions
              .Where(c => c.User_CC == userDB.IDUserCC && (c.Status_ID == 4 || c.Status_ID == 5)).ToList();
            return this.ToDataTable<ViewOderByAction>(l);
        }

        public void GuardarCambios(int wait)
        {
            throw new System.NotImplementedException();
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatu> status)
        {
            throw new System.NotImplementedException();
        }

        public void InsertOrderHeader(Company company, OrderType type, UserDB userDB)
        {
            throw new System.NotImplementedException();
        }

        public TranHistory InsertTranHistory(OrderHeader order, string evento, UserDB userDB)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateOrderHeader(UserDB userDB, int id, object field, string prop)
        {
            throw new System.NotImplementedException();
        }
    }
}
