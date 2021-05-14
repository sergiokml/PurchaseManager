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

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Profiles
{
    public class PerfilUPO : HFunctions, IPerfilActions
    {
        private readonly PurchaseManagerContext rContext;
        public PerfilUPO(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public DataTable GetVistaFPrincipal(Users userDB)
        {
            //todo TENGO QUE UNIR LA LISTA DE LAS Po EMITIDAS POR ESTE USUARIO.
            using (var rContext = new PurchaseManagerContext())
            {
                var l = rContext.vOrderByMinTransaction
              .Where(c => c.UserID == userDB.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
                var r = rContext.vRequisitionByMinTransaction.Where(c => c.StatusID == 2).ToList();
                foreach (var m in l)
                {
                    m.TypeDocumentHeader = "PO";
                }
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
                        OrderHeaderID = item.RequisitionHeaderID,
                        StatusID = item.StatusID,
                        SupplierID = "",
                        Tax = 0,
                        Total = 0,
                        Type = item.Type,
                        UserID = item.UserID,
                        TypeDocumentHeader = "PR",
                        CompanyCode = item.CompanyCode,
                        CompanyName = item.CompanyName,
                        NameBiz = item.NameBiz,
                        Status = item.Status
                    };
                    l.Add(n);
                }

                return this.ToDataTable<vOrderByMinTransaction>(l);
            }
        }

        public void GuardarCambios(int wait = 0)
        {
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

        public void UpdateItemHeader(Users userDB, int id, object valor, string campo)
        {
            var pr = rContext.OrderHeader.Find(id);
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
                Event = EventUserPR.UPDATE_PO.ToString(),
                UserID = userDB.UserID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            GuardarCambios();
        }

        public void DeleteOrderHeader(int id)
        {
            var pr = rContext.OrderHeader.Find(id);
            rContext.Transactions.RemoveRange(pr.Transactions);
            rContext.OrderHeader.Remove(pr);
            GuardarCambios();
        }

        public DataTable GetVistaFSupplier()
        {
            return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
        }

        public void DeleteRequesitionHeader(int id)
        {
            throw new NotImplementedException();
        }


        public List<Attaches> GetAttaches(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertItemHeader(Companies company, OrderType type, Users userDB)
        {
            var po = new OrderHeader
            {
                Type = (byte)type,
                StatusID = 1,
                Description = string.Empty,
                CompanyID = company.CompanyID,
                Net = 0,
                Exent = 0
            };
            var tran = new Transactions
            {
                Event = EventUserPR.CREATE_PR.ToString(),
                UserID = userDB.UserID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            po.Transactions.Add(tran);
            rContext.OrderHeader.Add(po);
            GuardarCambios();
        }

        public void GetFunciones()
        {
            ReqGroupByCost_Results = rContext.ufnGetReqGroupByCost(2).ToList();
            OrderGroupByStatus_Results = rContext.ufnGetOrderGroupByStatus().ToList();
        }

        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
        {
            throw new NotImplementedException();
        }

        public DataTable GetVistaAttaches(int IdItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteDetail(int idHeader, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void InsertAttach(int id, Attaches att, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void DeleteAttache(int id, Users userDB, Attaches item)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetails> GetDetailsOrder(int id)
        {
            var details = rContext.OrderHeader.Find(id).OrderDetails.ToList();
            foreach (var item in details)
            {
                rContext.Entry(item).Reference(c => c.Accounts).Load();
            }
            return details;
        }

        public List<RequisitionDetails> GetDetailsRequisition(int id)
        {
            var details = rContext.RequisitionHeader.Find(id).RequisitionDetails.ToList();
            foreach (var item in details)
            {
                rContext.Entry(item).Reference(c => c.Accounts).Load();
            }
            return details;

        }

        public DataTable GetVistaDetalles(int IdItem)
        {

            return this.ToDataTable<OrderDetails>(rContext.OrderDetails
                .Where(c => c.OrderHeaderID == IdItem).ToList());
        }

        public iGDropDownList GetStatusItem(DataRow dataRow)
        {
            if (dataRow["TypeDocumentHeader"].ToString() == "PR")
            {
                LLenarCombo(this.ToDataTable<RequisitionStatus>(rContext.RequisitionStatus.ToList()));
            }
            else if (dataRow["TypeDocumentHeader"].ToString() == "PO")
            {
                LLenarCombo(this.ToDataTable<OrderStatus>(rContext.OrderStatus.ToList()));
            }
            return ComboBox;
        }
    }
}
