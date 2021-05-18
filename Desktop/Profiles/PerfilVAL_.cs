//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Windows.Forms;

//using PurchaseData.DataModel;

//using PurchaseDesktop.Helpers;
//using PurchaseDesktop.Interfaces;

//using TenTec.Windows.iGridLib;

//namespace PurchaseDesktop.Profiles
//{
//    public class PerfilVAL_ : HFunctions, IPerfilActions
//    {
//        private readonly PurchaseManagerContext rContext;
//        //  public UserDB UserDB { get; set; }
//        //  public List<OrderStatu> OrderStatus { get; }

//        public PerfilVAL_(PurchaseManagerContext rContext)
//        {
//            this.rContext = rContext;
//        }

//        public List<OrderDetails> DetailsPO { get; set; }
//        public List<RequisitionDetails> DetailsPR { get; set; }
//        public DataTable TableDetails { get; set; }
//        // public DataRow Current { get; set; }
//        public TypeDocumentHeader TypeDocHeader { get; set; }
//        public OrderHeader DocumentPO { get; set; }
//        public RequisitionHeader DocumentPR { get; set; }
//        public Users CurrentUser { get; set; }

//        public DataTable GetVistaFPrincipal()
//        {
//            //  1   Pre PRequisition
//            //  2   Active PRequisition
//            //  3   Pre POrden
//            //  4   Active POrder
//            //  5   Valid POrder
//            //  6   POrder on Supplier
//            //  7   Agree by Supplier
//            //  8   POrder in Process
//            //  9   POrder Complete 
//            //var l = rContext.vOrderByMinTran
//            //  .Where(c => c.CostID == userDB.CostID && (c.StatusID == 4 || c.StatusID == 5)).ToList();
//            //return this.ToDataTable<vOrderByMinTran>(l);
//            return null;
//        }

//        public void GuardarCambios(int wait)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void InsertOrderHeader(Companies company, OrderType type, Users userDB)
//        {
//            throw new System.NotImplementedException();
//        }


//        public void UpdateItemHeader(Users userDB, object field, string prop)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void DeleteOrderHeader(int id)
//        {
//            throw new System.NotImplementedException();
//        }

//        public DataTable GetVistaFSupplier()
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteDetail(OrderHeader header, int idDetailr, Users userDB)
//        {
//            throw new NotImplementedException();
//        }



//        public void DeleteItemHeader()
//        {
//            throw new NotImplementedException();
//        }

//        public List<RequisitionDetails> GetDetailsRequisition(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Attaches> GetAttaches(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertItemHeader(Users userDB)
//        {
//            throw new NotImplementedException();
//        }

//        public void GetFunciones()
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertAttach(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertAttach(int id, object att, Users userDB)
//        {
//            throw new NotImplementedException();
//        }

//        public DataTable GetVistaAttaches(int IdItem)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteDetail(object header, int idDetailr, Users userDB)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteDetail(int idHeader, int idDetailr, Users userDB)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteAttache(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteAttache(int id, Users userDB)
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertAttach(int id, Attaches att, Users userDB)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteAttache(int id, Users userDB, Attaches item)
//        {
//            throw new NotImplementedException();
//        }

//        public List<T> GetRequisitionDetails<T>(int id) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public List<OrderDetails> GetDetailsOrder(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public iGDropDownList GetStatusItem(DataRow dataRow)
//        {
//            throw new NotImplementedException();
//        }

//        public DataTable GetVistaDetalles()
//        {
//            throw new NotImplementedException();
//        }

//        void IPerfilActions.GetDetailsRequisition(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteDetail(Users user)
//        {
//            throw new NotImplementedException();
//        }

//        public ContextMenuStrip GetMenuStrip()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
