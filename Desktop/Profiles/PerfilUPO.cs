using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
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
        //  public UserDB UserDB { get; set; }
        // public List<OrderStatu> OrderStatus { get; set; }
        public List<OrderCompanies> Companies { get; set; }
        public PerfilUPO(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }
        //todo ACTIONS PERFIL PO
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
            //todo TENGO QUE UNIR LA LISTA DE LAS Po EMITIDAS POR ESTE USUARIO.
            var l = rContext.vOrderByMinTran.Where(c => c
            .StatusID > 1 && c.StatusID < 10)
                .OrderByDescending(c => c.DateLast).ToList();
            return this.ToDataTable<vOrderByMinTran>(l);
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatus> status)
        {
            CargarBefore(grid, status);
            return Grid;
        }

        public void GuardarCambios(int wait = 0)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // Thread.Sleep(wait);
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
                StatusID = 3,  //  3   Pre POrden
                Description = string.Empty
            };
            ;
            pr.OrderTransactions.Add(InsertTranHistory(pr, "CREATE_PO", userDB));
            rContext.OrderHeader.Add(pr);
            GuardarCambios();
        }

        public OrderTransactions InsertTranHistory(OrderHeader order, string evento, OrderUsers userDB)
        {
            var tran = new OrderTransactions
            {
                Event = evento,
                UserID = userDB.UserID,
                StatuID = order.StatusID,
                DateTran = DateTime.Now
            };
            return tran;
        }

        public void UpdateOrderHeader(OrderUsers userDB, int id, object field, string prop)
        {
            var pr = rContext.OrderHeader.Find(id);
            pr.OrderTransactions.Add(InsertTranHistory(pr, "UPDATE_PR", userDB));
            pr.Description = UCase.ToTitleCase(field.ToString().ToLower());
            GuardarCambios();
        }
    }
}
