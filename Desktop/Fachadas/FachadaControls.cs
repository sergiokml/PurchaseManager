using System;
using System.Windows.Forms;

using Bunifu.Charts.WinForms.ChartTypes;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaControls : HFunctions
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }

        public FachadaControls(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }

        public void CargarDashBoard(iGrid grid, BunifuPieChart c1, BunifuPieChart c2, Panel panelDash)
        {
            CargarDatos(PerfilActions.CurrentUser, c1, c2, panelDash);
        }
    }
}
