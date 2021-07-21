using System;
using System.Data;
using System.Globalization;

using PurchaseData.DataModel;

using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaHitos
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public FachadaHitos(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }
        #region Hitos CRUD

        public string InsertHito(OrderHitos item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    PerfilActions.InsertHito(item, headerID);
                    break;
                case EPerfiles.UPR:
                    break;
                case EPerfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string DeleteHito(DataRow hitoDR, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    PerfilActions.DeleteHito(headerID, Convert.ToInt32(hitoDR["HitoID"]));
                    break;
                case EPerfiles.UPR:
                    break;
                case EPerfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string UpdateHito(object newValue, DataRow hitoDR, DataRow headerDR, string campo)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var hitoID = Convert.ToInt32(hitoDR["HitoID"]);
            var h = new OrderHitos().GetByID(hitoID);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (campo)
                    {
                        case "Description":
                            h.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            PerfilActions.UpdateHito(h, headerID);
                            break;
                        default:
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    break;
                case EPerfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";

        }

        #endregion

    }
}
