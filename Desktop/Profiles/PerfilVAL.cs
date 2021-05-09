using System;
using System.Collections.Generic;
using System.Data;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Profiles
{
    public class PerfilVAL : HFunctions, IPerfilActions
    {
        private readonly PurchaseManagerContext rContext;
        //  public UserDB UserDB { get; set; }
        //  public List<OrderStatu> OrderStatus { get; }

        public PerfilVAL(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public DataTable GetVista(Users userDB)
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
            //var l = rContext.vOrderByMinTran
            //  .Where(c => c.CostID == userDB.CostID && (c.StatusID == 4 || c.StatusID == 5)).ToList();
            //return this.ToDataTable<vOrderByMinTran>(l);
            return null;
        }

        public void GuardarCambios(int wait)
        {
            throw new System.NotImplementedException();
        }

        public void InsertOrderHeader(Companies company, OrderType type, Users userDB)
        {
            throw new System.NotImplementedException();
        }


        public void UpdateOrderHeader(Users userDB, int id, object field, string prop)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteOrderHeader(int id)
        {
            throw new System.NotImplementedException();
        }

        public DataTable GetVistaSuppliers()
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetail(OrderHeader header, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequisitionHeader(Users userDB, int id, object valor, string campo)
        {
            throw new NotImplementedException();
        }

        public void DeleteRequesitionHeader(int id)
        {
            throw new NotImplementedException();
        }

        public List<RequisitionDetails> GetRequisitionDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<Attaches> GetAttaches(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertRequisition(Companies company, OrderType type, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void GetFunciones()
        {
            throw new NotImplementedException();
        }

        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
        {
            throw new NotImplementedException();
        }
    }
}
