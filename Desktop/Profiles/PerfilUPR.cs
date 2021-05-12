using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Profiles
{
    public class PerfilUPR : HFunctions, IPerfilActions
    {
        private readonly PurchaseManagerContext rContext;

        public PerfilUPR(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public DataTable GetVista(Users userDB)
        {
            using (var rContext = new PurchaseManagerContext())
            {
                var l = rContext.vRequisitionByMinTransaction
              .Where(c => c.UserID == userDB.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
                return this.ToDataTable<vRequisitionByMinTransaction>(l);
            }
        }

        public DataTable GetVistaDetalles(int IdItem)
        {
            //using (var rContext = new PurchaseManagerContext())
            //{
            return this.ToDataTable<RequisitionDetails>(rContext.RequisitionDetails
               .Where(c => c.RequisitionHeaderID == IdItem).ToList());
            // }

        }

        public DataTable GetVistaAttaches(int IdItem)
        {
            var pr = rContext.RequisitionHeader.Find(IdItem);
            return this.ToDataTable<Attaches>(pr.Attaches.ToList());
        }

        public DataTable GetVistaSuppliers()
        {
            return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
        }

        public void GuardarCambios(int wait = 0)
        {
            // TODO ESTO ELIMINARLO? PUEDE ESTAR EN LA CLASE GridCustom, PERO HABRÁ DIFERENVCIA ENTRE GUARDAR CAMBIOS PO VS PR?        
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(wait);
                rContext.SaveChanges();
                Cursor.Current = Cursors.Default;
            }
            catch (DbEntityValidationException e)
            {
                foreach (DbEntityValidationResult eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void InsertOrderHeader(Companies company, OrderType type, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderHeader(Users userDB, int id, object valor, string campo)
        {
            //var pr = rContext.OrderHeader.Find(id);
            //switch (campo)
            //{
            //    case "Description":
            //        pr.Description = UCase.ToTitleCase(valor.ToString().ToLower());
            //        break;
            //    case "Type":
            //        pr.Type = Convert.ToByte(valor);
            //        break;
            //    case "StatusID":
            //        pr.StatusID = Convert.ToByte(valor);
            //        break;
            //    case "CompanyID":
            //        // pr.CompanyID = valor.ToString();
            //        break;
            //    case "CurrencyID":
            //        pr.CurrencyID = valor.ToString();
            //        break;
            //    case "SupplierID":
            //        pr.SupplierID = valor.ToString();
            //        break;
            //    default:
            //        break;
            //}
            //pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPR.UPDATE_PR));
            //GuardarCambios();
        }

        public void DeleteRequesitionHeader(int id)
        {
            var pr = rContext.RequisitionHeader.Find(id);
            rContext.Transactions.RemoveRange(pr.Transactions);
            rContext.RequisitionHeader.Remove(pr);
            GuardarCambios();
        }

        public void DeleteDetail(int idHeader, int idDetailr, Users userDB)
        {
            var pr = rContext.RequisitionHeader.Find(idHeader);
            var tran = new Transactions
            {
                Event = EventUserPR.DELETE_DETAIL.ToString(),
                UserID = userDB.UserID,
                DateTran = DateTime.Now
            };

            var detail = rContext.RequisitionDetails.Find(idDetailr, pr.RequisitionHeaderID);
            rContext.RequisitionDetails.Remove(detail);
            pr.Transactions.Add(tran);
            GuardarCambios();
        }

        public void UpdateRequisitionHeader(Users userDB, int id, object valor, string campo)
        {
            var pr = rContext.RequisitionHeader.Find(id);
            switch (campo)
            {
                case "Description":
                    pr.Description = UCase.ToTitleCase(valor.ToString().ToLower());
                    break;
                case "Type":
                    pr.Type = Convert.ToByte(valor);
                    break;
                case "StatusID":
                    pr.StatusID = Convert.ToByte(valor);
                    break;
                case "CompanyID":
                    pr.CompanyID = valor.ToString();
                    break;
                default:
                    break;
            }
            var tran = new Transactions
            {
                Event = EventUserPR.UPDATE_PR.ToString(),
                UserID = userDB.UserID,
                //StatuID = pr.StatusID
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            GuardarCambios();
        }

        public void DeleteOrderHeader(int id)
        {
            throw new NotImplementedException();
        }

        public List<RequisitionDetails> GetRequisitionDetails(int id)
        {
            var details = rContext.RequisitionHeader.Find(id).RequisitionDetails.ToList();
            foreach (var item in details)
            {
                rContext.Entry(item).Reference(c => c.Accounts).Load();
            }
            return details;
        }

        public List<Attaches> GetAttaches(int id)
        {
            return rContext.RequisitionHeader.Find(id).Attaches.ToList();
        }

        public void InsertRequisition(Companies company, OrderType type, Users userDB)
        {
            var pr = new RequisitionHeader
            {
                Type = (byte)type,
                StatusID = 1,
                Description = string.Empty,
                CompanyID = company.CompanyID
            };
            var tran = new Transactions
            {
                Event = EventUserPR.CREATE_PR.ToString(),
                UserID = userDB.UserID,
                //StatuID = pr.StatusID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            rContext.RequisitionHeader.Add(pr);
            GuardarCambios();
        }

        public void GetFunciones()
        {
            //IQueryable<ufnGetReqGroupByCost_Result> a = rContext.ufnGetReqGroupByCost(2);
            ReqGroupByCost_Results = rContext.ufnGetReqGroupByCost(2).ToList();
            OrderGroupByStatus_Results = rContext.ufnGetOrderGroupByStatus().ToList();
            //Label1.Text = rContext.;
        }

        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
        {
            var pr = rContext.RequisitionHeader.Find(idItem);
            var tran = new Transactions
            {
                Event = EventUserPR.INSERT_DETAIL.ToString(),
                UserID = userDB.UserID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            pr.RequisitionDetails.Add(detail);
            GuardarCambios();
        }

        public void InsertAttach(int id, Attaches att, Users userDB)
        {
            var pr = rContext.RequisitionHeader.Find(id);
            var tran = new Transactions
            {
                Event = EventUserPR.INSERT_ATTACH.ToString(),
                UserID = userDB.UserID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            pr.Attaches.Add(att);
            GuardarCambios();
        }


        public void DeleteAttache(int id, Users userDB, Attaches item)
        {
            var pr = rContext.RequisitionHeader.Find(id);
            var att = rContext.Attaches.Find(item.AttachID);
            var tran = new Transactions
            {
                Event = EventUserPR.DELETE_ATTACH.ToString(),
                UserID = userDB.UserID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            rContext.Attaches.Remove(att);
            GuardarCambios();
        }

        public enum EventUserPR
        {
            CREATE_PR = 1,
            UPDATE_PR = 2,
            DELETE_DETAIL = 3,
            INSERT_DETAIL = 4,
            INSERT_ATTACH = 5,
            DELETE_ATTACH = 6
        }
    }
}
