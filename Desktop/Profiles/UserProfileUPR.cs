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
    public class UserProfileUPR : HFunctions, IPerfilActions
    {
        //private readonly PurchaseManagerEntities rContext;
        public Users CurrentUser { get; set; }

        public UserProfileUPR(PurchaseManagerEntities rContext)
        {
            //this.rContext = rContext;
        }

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
                        DetailsCount = item.DetailsCount,
                        Event = item.Event,
                        HeaderID = item.HeaderID,
                        NameBiz = item.NameBiz,
                        Status = item.Status,
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
                        Discount = item.Discount
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
                    using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
                    {

                        RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
                        return this.ToDataTable<Attaches>(pr.Attaches.ToList());
                    }
                case TypeDocumentHeader.PO:
                    using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
                    {

                        OrderHeader po = rContext.OrderHeader.Find(headerID);
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

        public void InsertPRHeader(RequisitionHeader item)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.CREATE_PR.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    item.Transactions.Add(transaction);
                    rContext.RequisitionHeader.Add(item);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public void UpdateItemHeader<T>(TypeDocumentHeader td, T item)
        {
            RequisitionHeader doc = item as RequisitionHeader;
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        Transactions transaction = new Transactions
                        {
                            UserID = CurrentUser.UserID,
                            DateTran = rContext.Database
                            .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                            Event = Eventos.UPDATE_PR.ToString()
                        };
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            rContext.RequisitionHeader.Attach(doc);
                            rContext.Entry(doc).State = EntityState.Modified;
                            doc.Transactions.Add(transaction);
                            rContext.SaveChanges();
                            trans.Commit();
                        }
                    }
                    break;
                case TypeDocumentHeader.PO:
                    break;
                default:
                    break;
            }
        }

        public void DeleteItemHeader(TypeDocumentHeader td, int headerID)
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    RequisitionHeader doc = rContext.RequisitionHeader.Find(headerID);
                    rContext.Transactions.RemoveRange(doc.Transactions);
                    rContext.RequisitionHeader.Remove(doc);
                    rContext.Attaches.RemoveRange(doc.Attaches);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }


        public void InsertDetail<T>(T item, int headerID)
        {
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
                    RequisitionHeader doc = rContext.RequisitionHeader.Find(headerID);
                    doc.Transactions.Add(transaction);
                    rContext.Entry(item).State = EntityState.Added;
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public void DeleteDetail<T>(TypeDocumentHeader td, T item, int detailID)
        {
            //TODO ESTE QUEDÓ EXELENTEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    RequisitionHeader doc = item as RequisitionHeader;
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        rContext.RequisitionHeader.Attach(doc);
                        doc.RequisitionDetails
                            .Remove(doc.RequisitionDetails
                            .FirstOrDefault(c => c.DetailID == detailID));
                        Transactions transaction = new Transactions
                        {
                            Event = Eventos.DELETE_DETAIL.ToString(),
                            UserID = CurrentUser.UserID,
                            DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())")
                            .Single()
                        };
                        rContext.RequisitionHeader.Attach(doc);
                        doc.Transactions.Add(transaction);
                        rContext.SaveChanges();
                    }
                    break;
                case TypeDocumentHeader.PO:
                    break;
                default:
                    break;
            }
        }

        public void InsertAttach(Attaches item, int headerID)
        {
            //TODO ACA DEBO TRAER T (HEADER PO PR)
            using (var rContext = new PurchaseManagerEntities())
            {
                RequisitionHeader doc = rContext.RequisitionHeader.Find(headerID);
                Transactions transaction = new Transactions
                {
                    Event = Eventos.INSERT_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    doc.Transactions.Add(transaction);
                    doc.Attaches.Add(item);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public void DeleteAttach(int headerID, int attachID)
        {
            using (var rContext = new PurchaseManagerEntities())
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
                    RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
                    Attaches att = rContext.Attaches.Find(attachID);
                    rContext.Attaches.Remove(att);
                    //rContext.Entry(att).State = EntityState.Deleted;
                    pr.Transactions.Add(transaction);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public void InsertPOHeader(OrderHeader item)
        {
            throw new NotImplementedException();
        }

        public void UpdateDetail<T>(TypeDocumentHeader td, T item, object header)
        {
            //TODO ACA DEBO SACAR HEADERID
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    var d = (RequisitionDetails)header;
                    var pr = item as RequisitionHeader;
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        Transactions transaction = new Transactions
                        {
                            UserID = CurrentUser.UserID,
                            DateTran = rContext.Database
                            .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                            Event = Eventos.UPDATE_DETAIL.ToString()
                        };
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            RequisitionDetails detail = rContext.RequisitionDetails.Find(d.DetailID, pr.RequisitionHeaderID);
                            rContext.Entry(detail).State = EntityState.Modified;
                            pr.Transactions.Add(transaction);
                            rContext.SaveChanges();
                            trans.Commit();
                        }
                    }
                    break;
                case TypeDocumentHeader.PO:
                    break;
                default:
                    break;
            }
        }

        public void UpdateAttaches<T>(T item, int headerID, int attachID)
        {

            using (var rContext = new PurchaseManagerEntities())
            {
                Transactions transaction = new Transactions
                {
                    Event = Eventos.UPDATE_ATTACH.ToString(),
                    UserID = CurrentUser.UserID,
                    DateTran = rContext.Database
                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                };
                using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                {
                    RequisitionHeader doc = rContext.RequisitionHeader.Find(headerID);
                    doc.Transactions.Add(transaction);
                    rContext.Entry(item).State = EntityState.Modified;
                    //Attaches od = rContext.Attaches.Find(attachID);
                    //rContext.Entry(od).CurrentValues.SetValues(item);
                    rContext.SaveChanges();
                    trans.Commit();
                }
            }

        }

        public DataRow GetDataRow(TypeDocumentHeader td, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaFHitos(TypeDocumentHeader td, int headerID)
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
            throw new NotImplementedException();
        }

        public int DeleteSupplier(string headerID)
        {
            throw new NotImplementedException();
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

        public void UpdateSupplier(Suppliers item)
        {
            throw new NotImplementedException();
        }

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
    }
}
