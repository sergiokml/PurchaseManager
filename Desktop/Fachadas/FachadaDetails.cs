using System;
using System.Data;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaDetails
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }

        public FachadaDetails(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }

        public string InsertDetail<T>(T item, DataRow headerDR, FDetails fDetails)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            if (po.CurrencyID == null)
                            {
                                return "Please select 'Currency' for this Purchase Order.";
                            }
                            var od = item as OrderDetails;
                            var limite = new Currencies().GetList().Single(c => c.CurrencyID == po.CurrencyID).MaxInput;
                            if ((po.Total + (od.Qty * od.Price)) > limite) { return "You exceed the established limit."; }
                            PerfilActions.InsertDetail<OrderDetails>(od, po);
                            //! Set nuevo Current en el formulario Details
                            fDetails.Current = PerfilActions.GetDataRow(TypeDocumentHeader.PO, po.OrderHeaderID); ;
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                            PerfilActions.InsertDetail<RequisitionDetails>(item as RequisitionDetails, pr);
                            //! Set nuevo Current en el formulario Details
                            fDetails.Current = PerfilActions.GetDataRow(TypeDocumentHeader.PR, pr.RequisitionHeaderID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                    }
                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }

        public string DeleteDetail(DataRow detailDR, DataRow headerDR)
        {
            //! Acá se define primero si es PO o PR pero igualmente se implementa la funcion en perilPX
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var detailID = Convert.ToInt32(detailDR["DetailID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            PerfilActions.DeleteDetail(po, detailID);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                            PerfilActions.DeleteDetail(pr, detailID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                        default:
                            break;
                    }

                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }

        public string UpdateDetail(object newValue, DataRow detailDR, DataRow headerDR, string campo, bool isExent)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var detailID = Convert.ToInt32(detailDR["detailID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            var details = po.OrderDetails.SingleOrDefault(c => c.DetailID == detailID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            if (isExent)
                            {
                                details.IsExent = true;
                            }
                            else
                            {
                                details.IsExent = false;
                            }
                            switch (campo)
                            {
                                case "Price":
                                    foreach (char c in newValue.ToString())
                                    {
                                        if (c < '0' || c > '9')
                                        {
                                            if (c != ',') //! Solo acepto comas
                                            {
                                                return "There is an error in the field: 'Price'.";
                                            }
                                        }
                                    }
                                    details.Price = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                                    PerfilActions.UpdateDetail<OrderDetails>(details, po);
                                    break;
                                case "NameProduct":
                                    details.NameProduct = newValue.ToString();
                                    PerfilActions.UpdateDetail<OrderDetails>(details, po);
                                    break;
                            }
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    var detail = pr.RequisitionDetails.SingleOrDefault(c => c.DetailID == detailID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    switch (campo)
                    {
                        case "NameProduct":
                            detail.NameProduct = newValue.ToString();
                            PerfilActions.UpdateDetail<RequisitionDetails>(detail, pr);
                            break;
                        default:
                            break;
                    }
                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }

    }
}
