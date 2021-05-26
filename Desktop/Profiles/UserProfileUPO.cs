
using System;
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

                var r = rContext.vRequisitionByMinTransaction.Where(c => c.StatusID == 2).ToList();
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
                        DetailsCount = item.DetailsCount
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
                    RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
                    return this.ToDataTable<Attaches>(pr.Attaches.ToList());
                case TypeDocumentHeader.PO:
                    OrderHeader po = rContext.OrderHeader.Find(headerID);
                    return this.ToDataTable<Attaches>(po.Attaches.ToList());
                default:
                    break;
            }
            return null;
        }

        public DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID)
        {
            return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
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
                    doc.Transactions.Add(transaction);
                    rContext.Entry(item).State = EntityState.Added;
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
                    OrderHeader po = rContext.OrderHeader.Find(headerID);
                    //using (var rContext = new PurchaseManagerEntities())
                    //{
                    using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                    {
                        OrderDetails detail = rContext.OrderDetails.Find(detailID, po.OrderHeaderID);
                        rContext.Entry(detail).State = EntityState.Deleted;
                        po.Transactions.Add(transaction);
                        rContext.SaveChanges();
                        trans.Commit();
                    }
                    //}
                    break;
                default:
                    break;
            }
        }

        public void DeleteAttach(int headerID, int attachID)
        {
            OrderHeader doc = rContext.OrderHeader.Find(headerID);
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_ATTACH.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
               .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                Attaches att = rContext.Attaches.Find(attachID);
                rContext.Entry(att).State = EntityState.Deleted;
                doc.Transactions.Add(transaction);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        #region Funciones Auxiliares UNION "abstract class" + "rContext"

        public void SetResultFunctions()
        {
            ReqGroupByCost_Results = rContext.ufnGetReqGroupByCost(2).ToList();
            OrderGroupByStatus_Results = rContext.ufnGetOrderGroupByStatus().ToList();
        }

        #endregion

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
                    transaction.Event = Eventos.UPDATE_PO.ToString();
                    using (var rContext = new PurchaseManagerEntities())
                    {
                        using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                        {
                            OrderHeader doc = rContext.OrderHeader.Find(headerID);
                            doc.Transactions.Add(transaction);

                            OrderDetails od = rContext.OrderDetails.Find(detailID, headerID);
                            rContext.Entry(od).CurrentValues.SetValues(item);
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
            throw new NotImplementedException();
        }
    }
}
