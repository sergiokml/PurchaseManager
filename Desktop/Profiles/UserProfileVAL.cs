
using System.Data;

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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
    }
}
