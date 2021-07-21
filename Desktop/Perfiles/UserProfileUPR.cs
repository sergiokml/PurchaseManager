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
    public class UserProfileUPR : Enums, IPerfilActions
    {
        public Users CurrentUser { get; set; }

        public UserProfileUPR(Users currentUser)
        {
            CurrentUser = currentUser;
        }

        #region Vistas

        public DataTable VistaFPrincipal()
        {
            using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
            {
                List<vRequisitionByMinTransaction> l = rContext.vRequisitionByMinTransaction
              .Where(c => c.UserID == CurrentUser.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
                var poList = new List<vOrderByMinTransaction>();
                foreach (var item in l)
                {
                    if (item.StatusID == 3)
                    {
                        var po = rContext.vOrderByMinTransaction.Where(c => c.RequisitionHeaderID == item.HeaderID).Single();
                        poList.Add(po);
                    }
                }
                foreach (var item in poList)
                {
                    var pr = new vRequisitionByMinTransaction
                    {
                        StatusID = item.StatusID,
                        Code = item.Code,
                        UserID = item.UserID,
                        DateLast = item.DateLast,
                        CompanyCode = item.CompanyCode,
                        CompanyID = item.CompanyID,
                        CompanyName = item.CompanyName,
                        CostID = item.CostID,
                        Description = item.Description,
                        Event = item.Event,
                        HeaderID = item.HeaderID,
                        NameBiz = item.NameBiz,
                        Type = item.Type,
                        TypeDocumentHeader = item.TypeDocumentHeader,
                        UserPO = item.UserID,
                        NameUserID = item.NameUserID,
                        CurrencyID = item.CurrencyID,
                        SupplierID = item.SupplierID,
                        Net = item.Net,
                        Exent = item.Exent,
                        Total = item.Total,
                        Tax = item.Tax,
                        Discount = item.Discount,
                        Status = item.Status
                    };
                    l.Add(pr);
                }
                l.RemoveAll(c => c.StatusID == 3);
                return this.ToDataTable<vRequisitionByMinTransaction>(l);
            }
        }

        public DataTable VistaFDetalles(TypeDocumentHeader td, int headerID)
        {
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
                    {
                        return this
                        .ToDataTable<RequisitionDetails>(rContext.RequisitionDetails
                        .Where(c => c.HeaderID == headerID).ToList());
                    }
                case TypeDocumentHeader.PO:
                    using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
                    {
                        return this
                        .ToDataTable<OrderDetails>(rContext.OrderDetails
                        .Where(c => c.HeaderID == headerID).ToList());
                    }
            }
            return null;
        }

        public DataTable VistaFAdjuntos(TypeDocumentHeader td, int headerID)
        {
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        var pr = rContext.RequisitionHeader.Find(headerID);
                        return this.ToDataTable<Attaches>(pr.Attaches.ToList());
                    }
                case TypeDocumentHeader.PO:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        var po = rContext.OrderHeader.Find(headerID);
                        return this.ToDataTable<Attaches>(po.Attaches.ToList());
                    }
                default:
                    break;
            }
            return null;
        }

        public DataTable VistaFProveedores(TypeDocumentHeader td, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
            }
        }

        #endregion

        #region ItemHeader CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public int InsertItemHeader<T>(T item)
        {
            var doc = item as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.CREATE_PR.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                doc.Transactions.Add(transaction);
                rContext.RequisitionHeader.Add(doc);
                return rContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// 
        public int UpdateItemHeader<T>(T item)
        {
            RequisitionHeader doc = item as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                    Event = Eventos.UPDATE_PR.ToString()
                };
                rContext.RequisitionHeader.Attach(doc);
                rContext.Entry(doc).State = EntityState.Modified; // No es lo optimo, debo usar CurrentValues.SetValues
                doc.Transactions.Add(transaction);
                return rContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="td"></param>
        /// <param name="item"></param>
        public int DeleteItemHeader<T>(T item)
        {
            var doc = item as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                rContext.RequisitionHeader.Attach(doc);
                rContext.RequisitionHeader.Remove(doc);
                return rContext.SaveChanges();
            }
        }

        #endregion

        #region Details CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="header"></param>
        public void InsertDetail<T>(T item, object header)
        {
            RequisitionHeader doc = (RequisitionHeader)header;
            var detail = item as RequisitionDetails;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_DETAIL.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())")
                    .Single()
                };
                rContext.RequisitionHeader.Attach(doc);
                doc.RequisitionDetails.Add(detail);
                doc.Transactions.Add(transaction);
                rContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="td"></param>
        /// <param name="item"></param>
        /// <param name="id"></param>
        public void DeleteDetail<T>(T item, int detailID)
        {
            RequisitionHeader doc = item as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_DETAIL.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())")
                    .Single()
                };
                rContext.RequisitionHeader.Attach(doc);
                doc.RequisitionDetails.Remove(doc.RequisitionDetails.FirstOrDefault(c => c.DetailID == detailID));
                doc.Transactions.Add(transaction);
                rContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="header"></param>
        public void UpdateDetail<T>(T item, object header)
        {
            var doc = (RequisitionHeader)header;
            var detail = item as RequisitionDetails;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    UserID = CurrentUser.UserID,
                    Event = Eventos.UPDATE_DETAIL.ToString(),
                    DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())")
                    .Single()
                };
                rContext.RequisitionHeader.Attach(doc);
                rContext.Entry(doc.RequisitionDetails
                    .FirstOrDefault(c => c.DetailID == detail.DetailID)).CurrentValues.SetValues(detail);
                doc.Transactions.Add(transaction);
                rContext.SaveChanges();
            }
        }

        #endregion

        #region Attach CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="header"></param>
        public void InsertAttach<T>(Attaches item, T header)
        {
            var doc = header as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                rContext.RequisitionHeader.Attach(doc);
                doc.Transactions.Add(transaction);
                doc.Attaches.Add(item);
                rContext.SaveChanges();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="attachID"></param>
        public void DeleteAttach<T>(T item, int attachID)
        {
            RequisitionHeader doc = item as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
               .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                rContext.RequisitionHeader.Attach(doc);
                rContext.Attaches.Remove(doc.Attaches.FirstOrDefault(c => c.AttachID == attachID));
                doc.Transactions.Add(transaction);
                rContext.SaveChanges(); // Return 3
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="header"></param>
        public void UpdateAttaches<T>(Attaches item, T header)
        {
            var doc = header as RequisitionHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.UPDATE_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                rContext.RequisitionHeader.Attach(doc);
                rContext.Entry(doc.Attaches
                    .FirstOrDefault(c => c.AttachID == item.AttachID)).CurrentValues.SetValues(item);
                doc.Transactions.Add(transaction);
                rContext.SaveChanges();
            }
        }

        #endregion

        public DataRow GetDataRow(TypeDocumentHeader td, int headerID)
        {
            switch (td)
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


        #region Supplier CRUD


        public int InsertSupplier(Suppliers item)
        {
            throw new NotImplementedException();
        }
        public int DeleteSupplier(string headerID)
        {
            throw new NotImplementedException();
        }
        public void UpdateSupplier(Suppliers item)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Hitos CRUD
        public DataTable VistaFHitos(TypeDocumentHeader td, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                return this
                .ToDataTable<OrderHitos>(rContext.OrderHitos
                .Where(c => c.OrderHeaderID == headerID).ToList());
            }
        }

        public void InsertHito(OrderHitos item, int headerID)
        {
            throw new NotImplementedException();
        }

        public void UpdateHito(OrderHitos item, int headerID)
        {
            throw new NotImplementedException();
        }

        public int DeleteHito(int headerID, int hitoID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Notes CRUD
        public DataTable VistaFNotes(TypeDocumentHeader td, int headerID)
        {
            throw new NotImplementedException();
        }

        public void InsertNote(OrderNotes item, int headerID)
        {
            throw new NotImplementedException();
        }

        public void UpdateNote(OrderNotes item, int headerID)
        {
            throw new NotImplementedException();
        }

        public int DeleteNote(int headerID, int noteID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delivery CRUD
        public void InsertDelivery(OrderDelivery item, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaDelivery(TypeDocumentHeader td, int headerID)
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
