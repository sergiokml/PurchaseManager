using System;
using System.Data;
using System.Globalization;

using PurchaseData.DataModel;

using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaDeliverys
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public FachadaDeliverys(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }


        public string InsertDelivery(OrderDelivery item, DataRow headerDR)
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
                    PerfilActions.InsertDelivery(item, headerID);
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

        public string DeleteDelivery(DataRow noteDR, DataRow headerDR)
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
                    PerfilActions.DeleteDelivery(headerID, Convert.ToInt32(noteDR["DeliveryID"]));
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



    }
}
