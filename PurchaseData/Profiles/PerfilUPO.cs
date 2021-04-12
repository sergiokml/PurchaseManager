using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using PurchaseCtrl.DataBase.DataAccess;
using PurchaseCtrl.Desktop.Interfaces;
using PurchaseCtrl.Desktop.Utils;

using TenTec.Windows.iGridLib;

namespace PurchaseCtrl.Desktop.Profiles
{
    public class PerfilUPO : PerfilAbstract, IPerfilActions
    {
        private readonly PurchaseCtrlEntities rContext;
        //  public UserDB UserDB { get; set; }
        // public List<OrderStatu> OrderStatus { get; set; }
        public List<Company> Companies { get; set; }
        public PerfilUPO(PurchaseCtrlEntities rContext)
        {
            this.rContext = rContext;
        }
        //todo ACTIONS PERFIL PO
        public DataTable GetVista(UserDB userDB)
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
            var l = rContext.ViewOderByActions.Where(c => c
            .Status_ID > 1 && c.Status_ID < 10)
                .OrderByDescending(c => c.Date_Last).ToList();
            return this.ToDataTable<ViewOderByAction>(l);
        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatu> status)
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

        public void InsertOrderHeader(Company company, OrderType type, UserDB userDB)
        {

            var pr = new OrderHeader
            {
                IDCompany = company.CompanyID,
                OrderType = (byte)type,
                IDOrderStatus = 3,  //  3   Pre POrden
                OrderDescription = string.Empty
            };
            ;
            pr.TranHistories.Add(InsertTranHistory(pr, "CREATE_PO", userDB));
            rContext.OrderHeaders.Add(pr);
            GuardarCambios();
        }

        public TranHistory InsertTranHistory(OrderHeader order, string evento, UserDB userDB)
        {
            var tran = new TranHistory
            {
                TranHistoyDescription = evento,
                IDUserDB = userDB.UserDBID,
                IDOrderStatus = order.IDOrderStatus,
                TranHistoyDate = DateTime.Now
            };
            return tran;
        }

        public void UpdateOrderHeader(UserDB userDB, int id, object field, string prop)
        {
            var pr = rContext.OrderHeaders.Find(id);
            pr.TranHistories.Add(InsertTranHistory(pr, "UPDATE_PR", userDB));
            pr.OrderDescription = UCase.ToTitleCase(field.ToString().ToLower());
            GuardarCambios();
        }
    }
}
