using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Profiles
{
    public class PerfilVAL : PerfilAbstract, IPerfilActions
    {
        private readonly PurchaseManagerContext rContext;
        //  public UserDB UserDB { get; set; }
        //  public List<OrderStatu> OrderStatus { get; }

        public PerfilVAL(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public DataTable GetVista(OrderUsers userDB)
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
            var l = rContext.vOrderByMinTran
              .Where(c => c.CostID == userDB.CostID && (c.StatusID == 4 || c.StatusID == 5)).ToList();
            return this.ToDataTable<vOrderByMinTran>(l);
        }

        public void GuardarCambios(int wait)
        {
            throw new System.NotImplementedException();
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatus> status)
        {
            throw new System.NotImplementedException();
        }

        public void InsertOrderHeader(OrderCompanies company, OrderType type, OrderUsers userDB)
        {
            throw new System.NotImplementedException();
        }


        public void UpdateOrderHeader(OrderUsers userDB, int id, object field, string prop)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteOrderHeader(int id)
        {
            throw new System.NotImplementedException();
        }

        public OrderTransactions InsertTranHistory(OrderHeader order, OrderUsers userDB, Enum evento)
        {
            throw new NotImplementedException();
        }

        public DataTable GetVistaSuppliers()
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetail(OrderHeader header, int idDetailr, OrderUsers userDB)
        {
            throw new NotImplementedException();
        }
    }
}
