using System;
using System.Data;
using System.Globalization;

using PurchaseData.DataModel;

using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaNotes
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public FachadaNotes(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }
        #region Notes CRUD

        public string InsertNote(OrderNotes item, DataRow headerDR)
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
                    PerfilActions.InsertNote(item, headerID);
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

        public string DeleteNote(DataRow noteDR, DataRow headerDR)
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
                    PerfilActions.DeleteNote(headerID, Convert.ToInt32(noteDR["OrderNoteID"]));
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

        public string UpdateNote(object newValue, DataRow hitoDR, DataRow headerDR, string campo)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var noteID = Convert.ToInt32(hitoDR["OrderNoteID"]);
            var n = new OrderNotes().GetByID(noteID);
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
                        case "Modifier":
                            n.Modifier = Convert.ToByte(newValue);
                            PerfilActions.UpdateNote(n, headerID);
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
