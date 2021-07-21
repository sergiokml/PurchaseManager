using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Perfiles
{
    public class UserProfilerADM : Enums, IPerfilActions
    {
        public UserProfilerADM(Users currentUser)
        {
            CurrentUser = currentUser;
        }

        public Users CurrentUser { get; set; }

        public void DeleteAttach<T>(T item, int attachID)
        {
            throw new NotImplementedException();
        }

        public int DeleteDelivery(int headerID, int deliverID)
        {
            throw new NotImplementedException();
        }

        public void DeleteDetail<T>(T item, int detailID)
        {
            throw new NotImplementedException();
        }

        public int DeleteHito(int headerID, int hitoID)
        {
            throw new NotImplementedException();
        }

        public int DeleteItemHeader<T>(T item)
        {
            throw new NotImplementedException();
        }

        public int DeleteNote(int headerID, int noteID)
        {
            throw new NotImplementedException();
        }

        public int DeleteSupplier(string headerID)
        {
            throw new NotImplementedException();
        }

        public DataRow GetDataRow(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }

        public void InsertAttach<T>(Attaches item, T header)
        {
            throw new NotImplementedException();
        }

        public void InsertDelivery(OrderDelivery item, int headerID)
        {
            throw new NotImplementedException();
        }

        public void InsertDetail<T>(T item, object headerID)
        {
            throw new NotImplementedException();
        }

        public void InsertHito(OrderHitos item, int headerID)
        {
            throw new NotImplementedException();
        }

        public int InsertItemHeader<T>(T item)
        {
            throw new NotImplementedException();
        }

        public void InsertNote(OrderNotes item, int headerID)
        {
            throw new NotImplementedException();
        }

        public int InsertSupplier(Suppliers item)
        {
            throw new NotImplementedException();
        }

        public void UpdateAttaches<T>(Attaches item, T header)
        {
            throw new NotImplementedException();
        }

        public void UpdateDetail<T>(T item, object header)
        {
            throw new NotImplementedException();
        }

        public void UpdateHito(OrderHitos item, int headerID)
        {
            throw new NotImplementedException();
        }

        public int UpdateItemHeader<T>(T item)
        {
            throw new NotImplementedException();
        }

        public void UpdateNote(OrderNotes item, int headerID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSupplier(Suppliers item)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaDelivery(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaFAdjuntos(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaFDetalles(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaFHitos(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaFNotes(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }

        public DataTable VistaFPrincipal()
        {
            using (var rContext = new PurchaseManagerEntities())
            {
                List<vOrderByMinTransaction> l = rContext
                    .vOrderByMinTransaction
                    .Where(c => c.StatusID >= 2)
                    .OrderByDescending(c => c.DateLast).ToList();
                //!
                var users = new Users().GetList();
                foreach (var item in l)
                {
                    if (item.RequisitionHeaderID != null)
                    {
                        var user = users.FirstOrDefault(c => c.UserID == item.UserPR);
                        item.CostID = user.CostID;

                    }
                }
                return this.ToDataTable<vOrderByMinTransaction>(l);
            }
        }

        public DataTable VistaFProveedores(TypeDocumentHeader headerTD, int headerID)
        {
            throw new NotImplementedException();
        }
    }
}
