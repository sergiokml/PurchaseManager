using System;
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
        private readonly PurchaseManagerContext rContext;
        public Users CurrentUser { get; set; }

        public UserProfileUPR(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public DataTable VistaFPrincipal()
        {
            using (PurchaseManagerContext rContext = new PurchaseManagerContext())
            {
                System.Collections.Generic.List<vRequisitionByMinTransaction> l = rContext.vRequisitionByMinTransaction
              .Where(c => c.UserID == CurrentUser.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
                return this.ToDataTable<vRequisitionByMinTransaction>(l);
            }
        }

        public DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID)
        {
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    return this
                        .ToDataTable<RequisitionDetails>(rContext.RequisitionDetails
                        .Where(c => c.HeaderID == headerID).ToList());

                case TypeDocumentHeader.PO:
                    return this
                        .ToDataTable<OrderDetails>(rContext.OrderDetails
                        .Where(c => c.HeaderID == headerID).ToList());
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
            switch (headerTD)
            {
                case TypeDocumentHeader.PR:
                    return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
                case TypeDocumentHeader.PO:
                    break;
                default:
                    break;
            }
            return null;
        }

        public void InsertItemHeader(Companies company, DocumentType type)
        {
            RequisitionHeader pr = new RequisitionHeader
            {
                Type = (byte)type,
                StatusID = 1,
                Description = string.Empty,
                CompanyID = company.CompanyID
            };
            Transactions transaction = new Transactions
            {
                Event = Eventos.CREATE_PR.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                pr.Transactions.Add(transaction);
                rContext.RequisitionHeader.Add(pr);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void UpdateItemHeader<T>(T item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                Event = Eventos.UPDATE_PR.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
                rContext.Entry(pr).CurrentValues.SetValues(item);
                pr.Transactions.Add(transaction);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void DeleteItemHeader(TypeDocumentHeader headerTD, int headerID)
        {
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
                rContext.Transactions.RemoveRange(pr.Transactions);
                rContext.RequisitionHeader.Remove(pr);
                rContext.Attaches.RemoveRange(pr.Attaches);
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

        public void InsertDetail<T>(T item, int headerID) where T : class
        {
            RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
            Transactions transaction = new Transactions
            {
                Event = Eventos.INSERT_DETAIL.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                pr.Transactions.Add(transaction);
                rContext.Entry(item).State = EntityState.Added;
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void DeleteDetail(int headerID, int detailID)
        {
            RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
            Transactions transaction = new Transactions
            {
                Event = Eventos.DELETE_DETAIL.ToString(),
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
                .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
            {
                RequisitionDetails detail = rContext.RequisitionDetails.Find(detailID, pr.RequisitionHeaderID);
                rContext.Entry(detail).State = EntityState.Deleted;
                pr.Transactions.Add(transaction);
                rContext.SaveChanges();
                trans.Commit();
            }
        }

        public void InsertAttach(Attaches item, int headerID)
        {
            RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
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

        public void DeleteAttach(int headerID, int attachID)
        {
            RequisitionHeader pr = rContext.RequisitionHeader.Find(headerID);
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
                pr.Transactions.Add(transaction);
                rContext.SaveChanges();
                trans.Commit();
            }

        }
    }
}
