
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
        private readonly PurchaseManagerEntities rContext;
        public Users CurrentUser { get; set; }

        public UserProfileUPO(PurchaseManagerEntities rContext)
        {
            this.rContext = rContext;
        }

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
                        Status = item.Status,
                        TypeDocumentHeader = item.TypeDocumentHeader,
                        DetailsCount = item.DetailsCount,
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
                        RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
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

        public void InsertPOHeader(OrderHeader item)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.CREATE_PO.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                item.Transactions.Add(transaction);
                rContext.OrderHeader.Add(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void InsertDetail<T>(T item, int headerID)
        {
            OrderHeader doc = rContext.OrderHeader.Find(headerID);
            var i = item as OrderDetails;


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

                    var res = rContext.INSERT_ORDERDETAILS(headerID, i.NameProduct, i.DescriptionProduct, i.Qty, i.Price, i.AccountID, i.MedidaID, i.IsExent);



                    doc.Transactions.Add(transaction);
                    //rContext.Entry(item).State = EntityState.Added;
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

        public void InsertAttach(Attaches item, int headerID)
        {
            OrderHeader pr = rContext.OrderHeader.Find(headerID);
            Transactions transaction = new Transactions
            {
                Event = Eventos.INSERT_ATTACH.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                pr.Transactions.Add(transaction);
                pr.Attaches.Add(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void UpdateItemHeader<T>(TypeDocumentHeader headerTD, T item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
              .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    transaction.Event = Eventos.UPDATE_PR.ToString();
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            RequisitionHeader doc = rContext.RequisitionHeader.Find(headerID);
                            rContext.Entry(doc).CurrentValues.SetValues(item);
                            doc.Transactions.Add(transaction);
                            rContext.SaveChanges();
                            trans.Commit();
                        }
                    }
                    break;
                case TypeDocumentHeader.PO:
                    transaction.Event = Eventos.UPDATE_PO.ToString();
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            OrderHeader doc = rContext.OrderHeader.Find(headerID);
                            rContext.Entry(doc).CurrentValues.SetValues(item);
                            doc.Transactions.Add(transaction);
                            rContext.SaveChanges();
                            trans.Commit();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void DeleteItemHeader(TypeDocumentHeader headerTD, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    OrderHeader doc = rContext.OrderHeader.Find(headerID);
                    rContext.Transactions.RemoveRange(doc.Transactions);
                    rContext.OrderHeader.Remove(doc);
                    rContext.Attaches.RemoveRange(doc.Attaches);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public void DeleteDetail(TypeDocumentHeader headerTD, int headerID, int detailID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_DETAIL.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                      .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
                    //using (var rContext = new PurchaseManagerEntities())
                    //{
                    using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                    {
                        RequisitionDetails detail = rContext.RequisitionDetails.Find(detailID, pr.RequisitionHeaderID);
                        rContext.Entry(detail).State = EntityState.Deleted;
                        pr.Transactions.Add(transaction);
                        rContext.SaveChanges();
                        trans.Commit();
                    }
                    //  }
                    break;
                case TypeDocumentHeader.PO:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        OrderHeader po = rContext.OrderHeader.Find(headerID);
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            //OrderDetails detail = rContext.OrderDetails.Find(detailID, po.OrderHeaderID);
                            //rContext.Entry(detail).State = EntityState.Deleted;
                            rContext.DELETE_ORDERDETAILS(detailID, po.OrderHeaderID);
                            po.Transactions.Add(transaction);

                            rContext.SaveChanges();
                            trans.Commit();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void DeleteAttach(int headerID, int attachID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_ATTACH.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
               .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                Attaches att = rContext.Attaches.Find(attachID);
                rContext.Attaches.Remove(att);
                po.Transactions.Add(transaction);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void InsertPRHeader(RequisitionHeader item)
        {
            throw new NotImplementedException();
        }

        public void UpdateDetail<T>(TypeDocumentHeader headerTD, T item, int headerID, int detailID)
        {
            Transactions transaction = new Transactions
            {
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
             .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    //transaction.Event = Eventos.UPDATE_PR.ToString();
                    //using (var rContext = new PurchaseManagerEntities())
                    //{
                    //    using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                    //    {
                    //        RequisitionHeader doc = rContext.RequisitionHeader.Find(headerID);
                    //        doc.Transactions.Add(transaction);

                    //        RequisitionDetails od = rContext.RequisitionDetails.Find(detailID, headerID);
                    //        rContext.Entry(od).CurrentValues.SetValues(item);
                    //        rContext.SaveChanges();
                    //        trans.Commit();
                    //    }
                    //}
                    break;
                case TypeDocumentHeader.PO:
                    var i = item as OrderDetails;
                    transaction.Event = Eventos.UPDATE_DETAIL.ToString();
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            OrderHeader doc = rContext.OrderHeader.Find(headerID);
                            doc.Transactions.Add(transaction);
                            rContext.UPDATE_ORDERDETAILS(detailID, headerID, i.NameProduct, i.DescriptionProduct, i.Qty, i.Price, i.AccountID, i.MedidaID, i.IsExent);
                            //OrderDetails od = rContext.OrderDetails.Find(detailID, headerID);
                            //rContext.Entry(od).CurrentValues.SetValues(item);

                            rContext.SaveChanges();
                            trans.Commit();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void UpdateAttaches<T>(T item, int headerID, int attachID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.UPDATE_ATTACH.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
             .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (var rContext = new PurchaseManagerEntities())
            {
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    OrderHeader doc = rContext.OrderHeader.Find(headerID);
                    doc.Transactions.Add(transaction);
                    Attaches od = rContext.Attaches.Find(attachID);
                    rContext.Entry(od).CurrentValues.SetValues(item);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }

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

        public DataTable VistaFHitos(TypeDocumentHeader headerTD, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                return this
                .ToDataTable<OrderHitos>(rContext.OrderHitos
                .Where(c => c.OrderHeaderID == headerID).ToList());
            }
        }

        public int InsertSupplier(Suppliers item)
        {
            Suppliers s = rContext.Suppliers.Find(item.SupplierID);
            if (s == null)
            {
                rContext.Suppliers.Add(item);
                return rContext.SaveChanges();
            }
            return 0;
        }

        public int DeleteSupplier(string headerID)
        {
            Suppliers s = rContext.Suppliers.Find(headerID);
            rContext.Suppliers.Remove(s);
            return rContext.SaveChanges();
        }

        public void UpdateSupplier(Suppliers item)
        {
            Suppliers s = rContext.Suppliers.Find(item.SupplierID);
            item.ModifiedDate = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single();
            rContext.Entry(s).CurrentValues.SetValues(item);
            rContext.SaveChanges();
        }

        public void InsertHito(OrderHitos item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.INSERT_HITO.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                pr.OrderHitos.Add(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void UpdateHito(OrderHitos item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.UPDATE_HITO.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
            .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                OrderHitos h = rContext.OrderHitos.Find(item.HitoID, headerID);
                rContext.Entry(h).CurrentValues.SetValues(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public int DeleteHito(int headerID, int hitoID)
        {
            var res = 0;
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_HITO.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                OrderHitos h = rContext.OrderHitos.Find(hitoID, headerID);
                rContext.OrderHitos.Remove(h);
                po.Transactions.Add(transaction);
                res = rContext.SaveChanges();
                trans.Commit();
            }
            return res; // 3 porque son 3 tablas
        }

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
            Transactions transaction = new Transactions
            {
                Event = Eventos.INSERT_NOTE.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                pr.OrderNotes.Add(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void UpdateNote(OrderNotes item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.UPDATE_NOTE.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
            .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                OrderNotes n = rContext.OrderNotes.Find(item.OrderNoteID, headerID);
                rContext.Entry(n).CurrentValues.SetValues(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public int DeleteNote(int headerID, int noteID)
        {
            var res = 0;
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_NOTE.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                OrderNotes n = rContext.OrderNotes.Find(noteID, headerID);
                rContext.OrderNotes.Remove(n);
                po.Transactions.Add(transaction);
                res = rContext.SaveChanges();
                trans.Commit();
            }
            return res; // 3 porque son 3 tablas
        }

        public void InsertDelivery(OrderDelivery item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.INSERT_DELIVERY.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader pr = rContext.OrderHeader.Find(headerID);
                pr.Transactions.Add(transaction);
                pr.OrderDelivery.Add(item);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public DataTable VistaDelivery(TypeDocumentHeader headerTD, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                return this
                .ToDataTable<OrderDelivery>(rContext.OrderDelivery
                .Where(c => c.OrderHeaderID == headerID).ToList());
            }
        }

        public int DeleteDelivery(int headerID, int deliverID)
        {
            var res = 0;
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_DELIVERY.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                OrderHeader po = rContext.OrderHeader.Find(headerID);
                OrderDelivery h = rContext.OrderDelivery.Find(deliverID, headerID);
                rContext.OrderDelivery.Remove(h);
                po.Transactions.Add(transaction);
                res = rContext.SaveChanges();
                trans.Commit();
            }
            return res; // 3 porque son 3 tablas
        }
    }
}
