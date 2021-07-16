
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Profiles
{
    public class UserProfileUPO : HFunctions, IPerfilActions
    {
        public Users CurrentUser { get; set; }

        #region Vistas
        public DataTable VistaFPrincipal()
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                var l = rContext.vOrderByMinTransaction.Where(c => c.UserID == CurrentUser.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
                var r = rContext.vRequisitionByMinTransaction.Where(c => c.StatusID == 2).Where(c => c.UserPO == CurrentUser.UserID).ToList();
                foreach (var item in r)
                {
                    var n = new vOrderByMinTransaction
                    {
                        Code = item.Code,
                        CompanyID = item.CompanyID,
                        CostID = item.CostID,
                        DateLast = item.DateLast,
                        Description = item.Description,
                        Event = item.Event,
                        Exent = 0,
                        Net = 0,
                        HeaderID = item.HeaderID,
                        StatusID = item.StatusID,
                        SupplierID = string.Empty,
                        Tax = 0,
                        Total = 0,
                        Type = item.Type,
                        UserID = item.UserID,
                        CompanyCode = item.CompanyCode,
                        CompanyName = item.CompanyName,
                        NameBiz = item.NameBiz,
                        TypeDocumentHeader = item.TypeDocumentHeader,
                        Status = item.Status,
                        NameUserID = item.NameUserID
                    };
                    l.Add(n);
                }
                return this.ToDataTable<vOrderByMinTransaction>(l);
            }
        }

        public DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID)
        {
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        return this
                        .ToDataTable<RequisitionDetails>(rContext.RequisitionDetails
                        .Where(c => c.HeaderID == headerID).ToList());
                    }

                case TypeDocumentHeader.PO:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        return this
                        .ToDataTable<OrderDetails>(rContext.OrderDetails
                        .Where(c => c.HeaderID == headerID).ToList());
                    }
            }
            return null;
        }

        public DataTable VistaFAdjuntos(TypeDocumentHeader headerTD, int headerID)
        {
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        var pr = rContext.RequisitionHeader.Find(headerID);
                        return this.ToDataTable<Attaches>(pr.Attaches.Where(c => c.Modifier == 1).ToList());
                    }
                case TypeDocumentHeader.PO:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        OrderHeader po = rContext.OrderHeader.Find(headerID);
                        return this.ToDataTable<Attaches>(po.Attaches.ToList());
                    }
                default:
                    break;
            }
            return null;
        }

        public DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID)
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
        public void InsertItemHeader<T>(T item)
        {
            var doc = item as OrderHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.CREATE_PO.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
               .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                doc.Transactions.Add(transaction);
                rContext.OrderHeader.Add(doc);
                rContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public int UpdateItemHeader<T>(T item)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    UserID = CurrentUser.UserID,
                    Event = Eventos.UPDATE_PO.ToString(),
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                Type typeParameterType = typeof(T);
                if (typeParameterType.Name == "OrderHeader")
                {
                    OrderHeader doc = item as OrderHeader;
                    rContext.OrderHeader.Attach(doc);
                    rContext.Entry(doc).State = EntityState.Modified;
                    doc.Transactions.Add(transaction);
                }
                else if (typeParameterType.Name == "RequisitionHeader")
                {
                    RequisitionHeader pr = item as RequisitionHeader;
                    rContext.RequisitionHeader.Attach(pr);
                    rContext.Entry(pr).State = EntityState.Modified;
                    pr.Transactions.Add(transaction);
                }
                return rContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void DeleteItemHeader<T>(T item)
        {
            var doc = item as OrderHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                rContext.OrderHeader.Attach(doc);
                rContext.OrderHeader.Remove(doc);
                rContext.SaveChanges();
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
            var i = item as OrderDetails;
            OrderHeader doc = (OrderHeader)header;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_DETAIL.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    try
                    {
                        var res = rContext.INSERT_ORDERDETAILS(doc.OrderHeaderID, i.NameProduct, i.DescriptionProduct, i.Qty, i.Price, i.AccountID, i.MedidaID, i.IsExent);
                        rContext.OrderHeader.Attach(doc);
                        doc.Transactions.Add(transaction);
                        rContext.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="detailID"></param>
        public void DeleteDetail<T>(T item, int detailID)
        {
            OrderHeader doc = item as OrderHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_DETAIL.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    rContext.DELETE_ORDERDETAILS(detailID, doc.OrderHeaderID);
                    rContext.OrderHeader.Attach(doc);
                    doc.Transactions.Add(transaction);
                    rContext.SaveChanges();
                    trans.Commit();
                }
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
            var i = item as OrderDetails;
            var doc = (OrderHeader)header;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    UserID = CurrentUser.UserID,
                    Event = Eventos.UPDATE_DETAIL.ToString(),
                    DateTran = rContext.Database.
                SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    rContext.UPDATE_ORDERDETAILS(i.DetailID, doc.OrderHeaderID, i.NameProduct, i.DescriptionProduct, i.Qty, i.Price, i.AccountID, i.MedidaID, i.IsExent);
                    rContext.OrderHeader.Attach(doc);
                    doc.Transactions.Add(transaction);
                    rContext.SaveChanges();
                    trans.Commit();
                }
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
            var doc = header as OrderHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                rContext.OrderHeader.Attach(doc);
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
            OrderHeader doc = item as OrderHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                rContext.OrderHeader.Attach(doc);
                doc.Attaches.Remove(doc.Attaches.FirstOrDefault(c => c.AttachID == attachID));
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
        public void UpdateAttaches<T>(Attaches item, T header)
        {
            var doc = header as OrderHeader;
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.UPDATE_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
             .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                rContext.OrderHeader.Attach(doc);
                rContext.Entry(doc.Attaches.FirstOrDefault(c => c.AttachID == item.AttachID)).CurrentValues.SetValues(item);
                doc.Transactions.Add(transaction);
                rContext.SaveChanges();
            }
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
                    using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
                    {
                        List<vOrderByMinTransaction> l = rContext.vOrderByMinTransaction
                      .Where(c => c.HeaderID == headerID)
                      .OrderByDescending(c => c.DateLast).ToList();
                        return this.ToDataTable<vOrderByMinTransaction>(l).Rows[0];
                    }
                default:
                    break;
            };
            return null;
        }


        #region Supplier CRUD

        public int InsertSupplier(Suppliers item)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Suppliers s = rContext.Suppliers.Find(item.SupplierID);
                if (s == null)
                {
                    rContext.Suppliers.Add(item);
                    return rContext.SaveChanges();
                }
                return 0;
            }
        }

        public int DeleteSupplier(string headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Suppliers s = rContext.Suppliers.Find(headerID);
                rContext.Suppliers.Remove(s);
                return rContext.SaveChanges();
            }
        }

        public void UpdateSupplier(Suppliers item)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Suppliers s = rContext.Suppliers.Find(item.SupplierID);
                item.ModifiedDate = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single();
                rContext.Entry(s).CurrentValues.SetValues(item);
                rContext.SaveChanges();
            }
        }

        #endregion

        #region Hitos CRUD
        public DataTable VistaFHitos(TypeDocumentHeader headerTD, int headerID)
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
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_HITO.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                pr.OrderHitos.Add(item);
                rContext.SaveChanges();
            }
        }

        public void UpdateHito(OrderHitos item, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.UPDATE_HITO.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                OrderHitos h = rContext.OrderHitos.Find(item.HitoID, headerID);
                rContext.Entry(h).CurrentValues.SetValues(item);
                rContext.SaveChanges();
            }
        }

        public int DeleteHito(int headerID, int hitoID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_HITO.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
              .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                OrderHitos h = rContext.OrderHitos.Find(hitoID, headerID);
                rContext.OrderHitos.Remove(h);
                po.Transactions.Add(transaction);
                return rContext.SaveChanges();
            }
        }

        #endregion

        #region Notes CRUD
        public DataTable VistaFNotes(TypeDocumentHeader headerTD, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                return this
                .ToDataTable<OrderNotes>(rContext.OrderNotes
                .Where(c => c.OrderHeaderID == headerID).ToList());
            }
        }

        public void InsertNote(OrderNotes item, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_NOTE.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                pr.OrderNotes.Add(item);
                rContext.SaveChanges();
            }
        }

        public void UpdateNote(OrderNotes item, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.UPDATE_NOTE.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
            .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                OrderNotes n = rContext.OrderNotes.Find(item.OrderNoteID, headerID);
                rContext.Entry(n).CurrentValues.SetValues(item);
                rContext.SaveChanges();
            }
        }

        public int DeleteNote(int headerID, int noteID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_NOTE.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                OrderNotes n = rContext.OrderNotes.Find(noteID, headerID);
                rContext.OrderNotes.Remove(n);
                po.Transactions.Add(transaction);
                return rContext.SaveChanges();
            }
        }

        #endregion

        #region Delivery CRUD
        public DataTable VistaDelivery(TypeDocumentHeader headerTD, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                return this
                .ToDataTable<OrderDelivery>(rContext.OrderDelivery
                .Where(c => c.OrderHeaderID == headerID).ToList());
            }
        }

        public void InsertDelivery(OrderDelivery item, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_DELIVERY.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                pr.OrderDelivery.Add(item);
                rContext.SaveChanges();
            }
        }


        public int DeleteDelivery(int headerID, int deliverID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.DELETE_DELIVERY.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
               .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                OrderDelivery h = rContext.OrderDelivery.Find(deliverID, headerID);
                rContext.OrderDelivery.Remove(h);
                po.Transactions.Add(transaction);
                return rContext.SaveChanges();
            }
        }

        #endregion

    }
}
