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
    public class PerfilUPO : PerfilAbstract, IPerfilActions
    {
        private readonly PurchaseManagerContext rContext;

        public List<Companies> Companies { get; set; }
        public PerfilUPO(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public DataTable GetVista(Users userDB)
        {
            //todo TENGO QUE UNIR LA LISTA DE LAS Po EMITIDAS POR ESTE USUARIO.
            //using (var rContext = new PurchaseManagerContext())
            //{
            //    var l = rContext.vOrderByMinTran.Where(c => c
            //.StatusID > 1 && c.StatusID < 10)
            //    .OrderByDescending(c => c.DateLast).ToList();
            //    return this.ToDataTable<vOrderByMinTran>(l);
            //}

            return null;
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

        public void InsertOrderHeader(Companies company, OrderType type, Users userDB)
        {
            var pr = new OrderHeader
            {
                //CompanyID = company.CompanyID,
                Type = (byte)type,
                StatusID = 3,  //  3   Pre POrden
                Description = string.Empty
            };
            ;
            pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPO.CREATE_PO));
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
                    //pr.CompanyID = valor.ToString();
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
            pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPO.UPDATE_PO));
            GuardarCambios();
        }

        public void DeleteOrderHeader(int id)
        {
            var pr = rContext.OrderHeader.Find(id);
            rContext.Transactions.RemoveRange(pr.Transactions);
            rContext.OrderHeader.Remove(pr);
            GuardarCambios();
        }

        public enum EventUserPO
        {
            CREATE_PO = 1,
            UPDATE_PO = 2
        }

        public DataTable GetVistaSuppliers()
        {
            return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatus> status)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetail(OrderHeader header, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequisitionHeader(Users userDB, int id, object valor, string campo)
        {
            throw new NotImplementedException();
        }
    }
}
