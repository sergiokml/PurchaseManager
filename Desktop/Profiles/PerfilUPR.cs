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
    public class PerfilUPR : PerfilAbstract, IPerfilActions
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
            var pr = new OrderHeader
            {
                //CompanyID = company.CompanyID,
                Type = (byte)type,
                StatusID = 1,   //  1   Pre PRequisition
                Description = string.Empty
            };
            ;
            pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPR.CREATE_PR));
            rContext.OrderHeader.Add(pr);
            GuardarCambios();
        }

        public Transactions InsertTranHistory(OrderHeader order, Users userDB, Enum @evento)
        {
            var tran = new Transactions
            {
                Event = evento.ToString(),
                UserID = userDB.UserID,
                StatuID = order.StatusID,
                DateTran = DateTime.Now
            };
            return tran;
        }

        public void UpdateOrderHeader(Users userDB, int id, object valor, string campo)
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
                    // pr.CompanyID = valor.ToString();
                    break;
                case "CurrencyID":
                    pr.CurrencyID = valor.ToString();
                    break;
                case "SupplierID":
                    pr.SupplierID = valor.ToString();
                    break;
                default:
                    break;
            }
            pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPR.UPDATE_PR));
            GuardarCambios();
        }

        public void DeleteOrderHeader(int id)
        {
            var pr = rContext.OrderHeader.Find(id);
            rContext.Transactions.RemoveRange(pr.Transactions);
            rContext.OrderHeader.Remove(pr);
            GuardarCambios();
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatus> status)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetail(OrderHeader header, int idDetailr, Users userDB)
        {

            var tran = new Transactions
            {
                Event = EventUserPR.DELETE_DETAIL.ToString(),
                UserID = userDB.UserID,
                DateTran = DateTime.Now,
                StatuID = header.StatusID
            };
            new OrderDetails().BorrarDetail(header, idDetailr, tran);
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
                StatuID = pr.StatusID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            pr.Transactions.Add(tran);
            GuardarCambios();
        }

        public enum StatusUserPR
        {
            PrePRequisition = 1,
            ActivePRequisition = 2

        }

        public enum EventUserPR
        {
            CREATE_PR = 1,
            UPDATE_PR = 2,
            DELETE_DETAIL = 3
        }
    }
}
