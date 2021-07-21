
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Perfiles
{
    public class UserProfileVAL : HFunctions, IPerfilActions
    {
        public UserProfileVAL(Users user)
        {
            CurrentUser = user;
        }

        public Users CurrentUser { get; set; }

        #region Vistas
        public DataTable VistaFPrincipal()
        {
            List<vOrderByMinTransaction> l2 = new List<vOrderByMinTransaction>();
            using (var rContext = new PurchaseManagerEntities())
            {
                List<vOrderByMinTransaction> l = rContext
                    .vOrderByMinTransaction
                    .Where(c => c.StatusID >= 2)
                    .OrderByDescending(c => c.DateLast).ToList();
                //!
                var users = new Users().GetList();
                foreach (var item in l)
                {
                    if (item.RequisitionHeaderID != null)
                    {
                        var user = users.FirstOrDefault(c => c.UserID == item.UserPR);
                        item.CostID = user.CostID;
                        if (CurrentUser.CostID == user.CostID)
                        {
                            l2.Add(item);
                        }
                    }
                }

                return this.ToDataTable<vOrderByMinTransaction>(l2);
            }
        }

        public DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFAdjuntos(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFHitos(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        public DataRow GetDataRow(TypeDocumentHeader headerTD, int headerID)
        {
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
                    {
                        List<vRequisitionByMinTransaction> l = rContext.vRequisitionByMinTransaction
                      .Where(c => c.HeaderID == headerID)
                      .OrderByDescending(c => c.DateLast).ToList();
                        return this.ToDataTable<vRequisitionByMinTransaction>(l).Rows[0];
                    }
                case TypeDocumentHeader.PO:
                    break;
                default:
                    break;
            };
            return null;
        }

        #region ItemHeader CRUD

        public int InsertItemHeader<T>(T item)
        {
            throw new NotImplementedException();
        }
        public int UpdateItemHeader<T>(T item)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                    Event = Eventos.UPDATE_PO.ToString()
                };
                OrderHeader doc = item as OrderHeader;
                rContext.OrderHeader.Attach(doc);
                rContext.Entry(doc).State = EntityState.Modified;
                doc.Transactions.Add(transaction);
                return rContext.SaveChanges();
            }
        }
        public int DeleteItemHeader<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Details CRUD
        public void InsertDetail<T>(T item, object headerID)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateDetail<T>(T item, object header)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteDetail<T>(T item, int detailID)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Attach CRUD
        public void InsertAttach<T>(Attaches item, T header)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAttach<T>(T item, int attachID)
        {
            throw new NotImplementedException();
        }

        public void UpdateAttaches<T>(Attaches item, T header)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Supplier CRUD
        public int InsertSupplier(Suppliers item)
        {
            throw new System.NotImplementedException();
        }
        public int DeleteSupplier(string headerID)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateSupplier(Suppliers item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Hitos CRUD
        public void InsertHito(OrderHitos item, int headerID)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateHito(OrderHitos item, int headerID)
        {
            throw new System.NotImplementedException();
        }
        public int DeleteHito(int headerID, int hitoID)
        {
            throw new System.NotImplementedException();
        }


        #endregion

        #region Notes CRUD
        public DataTable VistaFNotes(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }
        public void InsertNote(OrderNotes item, int headerID)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateNote(OrderNotes item, int headerID)
        {
            throw new System.NotImplementedException();
        }
        public int DeleteNote(int headerID, int noteID)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Delivery CRUD
        public void InsertDelivery(OrderDelivery item, int headerID)
        {
            throw new NotImplementedException();
        }
        public DataTable VistaDelivery(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }
        public int DeleteDelivery(int headerID, int deliverID)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
