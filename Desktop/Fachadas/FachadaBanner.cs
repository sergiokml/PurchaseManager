using System;
using System.Threading.Tasks;

using PurchaseData.DataModel;
using PurchaseData.Helpers;

using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaBanner
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }
        public ConfigApp ConfigApp { get; set; }

        public FachadaBanner(IPerfilActions perfilActions, ConfigApp configApp)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
            ConfigApp = configApp;
        }

        public async Task<string> CargarBanner()
        {
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    return await new HtmlManipulate(ConfigApp).ReemplazarDatos();
                case EPerfiles.UPR:
                    return await new HtmlManipulate(ConfigApp).ReemplazarDatos();
                case EPerfiles.VAL:
                    return await new HtmlManipulate(ConfigApp).ReemplazarDatos();
                default:
                    break;
            }
            return string.Empty;
        }
    }
}
