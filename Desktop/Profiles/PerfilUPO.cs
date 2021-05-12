﻿using System;
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
    public class PerfilUPO : HFunctions, IPerfilActions
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
            using (var rContext = new PurchaseManagerContext())
            {
                var l = rContext.vOrderByMinTransaction
              .Where(c => c.UserID == userDB.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
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

        public void InsertOrderHeader(Companies company, OrderType type, Users userDB)
        {
            //var pr = new OrderHeader
            //{
            //    //CompanyID = company.CompanyID,
            //    Type = (byte)type,
            //    StatusID = 3,  //  3   Pre POrden
            //    Description = string.Empty
            //};
            //;
            //pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPO.CREATE_PO));
            //rContext.OrderHeader.Add(pr);
            //GuardarCambios();
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
            //        //pr.CompanyID = valor.ToString();
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
            //pr.Transactions.Add(InsertTranHistory(pr, userDB, EventUserPO.UPDATE_PO));
            //GuardarCambios();
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

        public void DeleteDetail(OrderHeader header, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequisitionHeader(Users userDB, int id, object valor, string campo)
        {
            throw new NotImplementedException();
        }

        public void DeleteRequesitionHeader(int id)
        {
            throw new NotImplementedException();
        }

        public List<RequisitionDetails> GetRequisitionDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<Attaches> GetAttaches(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertRequisition(Companies company, OrderType type, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void GetFunciones()
        {
            throw new NotImplementedException();
        }

        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
        {
            throw new NotImplementedException();
        }

        public void InsertAttach(int id, object att, Users userDB)
        {
            throw new NotImplementedException();
        }

        public DataTable GetVistaAttaches(int IdItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteDetail(object header, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void DeleteDetail(int idHeader, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void DeleteAttache(int id, Users userDB)
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
    }
}
