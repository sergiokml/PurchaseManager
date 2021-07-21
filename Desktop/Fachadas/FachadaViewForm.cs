using System;
using System.Data;

using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Fachadas
{
    //! Estos métodos no se invocan si no se abren los respectivos Form.
    public class FachadaViewForm
    {
        public IPerfilActions PerfilActions { get; set; }

        public FachadaViewForm(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
        }

        public DataTable GetVistaFPrincipal()
        {
            return PerfilActions.VistaFPrincipal();
        }

        public DataTable GetVistaDetalles(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            return PerfilActions.VistaFDetalles(td, Convert.ToInt32(headerDR["headerID"]));
        }

        public DataTable GetVistaHitos(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            return PerfilActions.VistaFHitos(td, Convert.ToInt32(headerDR["headerID"]));
        }

        public DataTable GetVistaNotes(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            return PerfilActions.VistaFNotes(td, Convert.ToInt32(headerDR["headerID"]));
        }

        public DataTable GetVistaAttaches(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            return PerfilActions.VistaFAdjuntos(td, Convert.ToInt32(headerDR["headerID"]));
        }

        public DataTable GetVistaSuppliers(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            return PerfilActions.VistaFProveedores(td, Convert.ToInt32(headerDR["headerID"]));
        }

        public DataTable GetVistaDelivery(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            return PerfilActions.VistaDelivery(td, Convert.ToInt32(headerDR["headerID"]));
        }
    }
}
