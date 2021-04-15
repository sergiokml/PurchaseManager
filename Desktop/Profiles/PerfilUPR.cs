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

        public DataTable GetVista(OrderUsers userDB)
        {
            //  1   Pre PRequisition
            //  2   Active PRequisition
            //  3   Pre POrden
            //  4   Active POrder
            //  5   Valid POrder
            //  6   POrder on Supplier
            //  7   Agree by Supplier
            //  8   POrder in Process
            //  9   POrder Complete
            //var algo = rContext.vOrderByMinTran.Where(c => c.UserID == userDB.UserID);
            //using (var rContext = new PurchaseManagerContext())
            //{
            var l = rContext.vOrderByMinTran
                  .Where(c => c.UserID == userDB.UserID && c.StatusID < 3)
                  .OrderByDescending(c => c.DateLast).ToList();
            return this.ToDataTable<vOrderByMinTran>(l);
            //}
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

        public void InsertOrderHeader(OrderCompanies company, OrderType type, OrderUsers userDB)
        {
            var pr = new OrderHeader
            {
                CompanyID = company.CompanyID,
                Type = (byte)type,
                StatusID = 1,   //  1   Pre PRequisition
                Description = string.Empty
            };
            ;
            pr.OrderTransactions.Add(InsertTranHistory(pr, userDB, EventUserPR.CREATE_PR));
            rContext.OrderHeader.Add(pr);
            GuardarCambios();
        }

        public OrderTransactions InsertTranHistory(OrderHeader order, OrderUsers userDB, Enum @evento)
        {
            var tran = new OrderTransactions
            {
                Event = evento.ToString(),
                UserID = userDB.UserID,
                StatuID = order.StatusID,
                DateTran = DateTime.Now
            };
            return tran;
        }

        public void UpdateOrderHeader(OrderUsers userDB, int id, object valor, string campo)
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
                case "CurrencyID":
                    pr.CurrencyID = valor.ToString();
                    break;
                case "SupplierID":
                    pr.SupplierID = valor.ToString();
                    break;
                default:
                    break;
            }
            pr.OrderTransactions.Add(InsertTranHistory(pr, userDB, EventUserPR.UPDATE_PR));
            GuardarCambios();
        }

        public void DeleteOrderHeader(int id)
        {
            var pr = rContext.OrderHeader.Find(id);
            rContext.OrderTransactions.RemoveRange(pr.OrderTransactions);
            rContext.OrderHeader.Remove(pr);
            GuardarCambios();
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatus> status)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetail(OrderHeader header, int idDetailr, OrderUsers userDB)
        {
            var tran = InsertTranHistory(new OrderHeader() { OrderHeaderID = header.OrderHeaderID }, userDB, EventUserPR.UPDATE_PR);

            new OrderDetails().BorrarDetail(header, idDetailr);
            //pr.OrderTransactions.Add(InsertTranHistory(pr, userDB, EventUserPR.UPDATE_PR));
            //pr.OrderDetails.Remove(contextDB.OrderDetails.Find(idDetailr, header.OrderHeaderID));
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
        }
    }
}
