
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Profiles
{

    public class UserProfileVAL : HFunctions, IPerfilActions
    {
        private readonly PurchaseManagerEntities rContext;
        public Users CurrentUser { get; set; }

        public UserProfileVAL(PurchaseManagerEntities rContext)
        {
            this.rContext = rContext;
        }

        public DataTable VistaFPrincipal()
        {
            //todo INCLUIR LAS PO QUE YO VALIDÉ !!!!
            var l = rContext.vOrderByMinTransaction.Where(c => c.StatusID == 2).OrderByDescending(c => c.DateLast).ToList();
            return this.ToDataTable<vOrderByMinTransaction>(l);
        }

        public DataRow GetDataRow(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFAdjuntos(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFHitos(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void InsertPRHeader(RequisitionHeader item)
        {
            throw new System.NotImplementedException();
        }

        public void InsertPOHeader(OrderHeader item)
        {
            throw new System.NotImplementedException();
        }

        public void InsertDetail<T>(T item, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void InsertAttach(Attaches item, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateItemHeader<T>(TypeDocumentHeader headerTD, T item, int headerID)
        {
            Transactions transaction = new Transactions
            {
                UserID = CurrentUser.UserID,
                DateTran = rContext.Database
              .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };

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
        }

        public void UpdateDetail<T>(TypeDocumentHeader headerTD, T item, int headerID, int detailID)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAttaches<T>(T item, int headerID, int attachID)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteItemHeader(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteDetail(TypeDocumentHeader headerTD, int headerID, int detailID)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAttach(int headerID, int attachID)
        {
            throw new System.NotImplementedException();
        }

        public void InsertSupplier(Suppliers item)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteSupplier(string headerID)
        {
            throw new System.NotImplementedException();
        }

        public void InsertHito(OrderHitos item, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateHito(OrderHitos item, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteHito(int headerID, int hitoID)
        {
            throw new System.NotImplementedException();
        }

        public DataTable VistaFNotes(TypeDocumentHeader headerTD, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void InsertNote(OrderNotes item, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateNote(OrderNotes item, int headerID)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteNote(int headerID, int noteID)
        {
            throw new System.NotImplementedException();
        }
    }
}
