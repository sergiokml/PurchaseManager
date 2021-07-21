using System;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaGrid : FuncGrid
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }

        public FachadaGrid(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }
        public void CargarGrid(iGrid grid)
        {
            //! Asociar el Grid de cada Form po única vez a la Clase HFunctions
            Grid = grid;
            switch (grid.Parent.Name) // Formulario Padre
            {
                case "FPrincipal":
                    CargarColumnasFPrincipal(CurrentPerfil); break;
                case "FDetails":
                    CargarColumnasFDetail(); break;
                case "FAttach":
                    CargarColumnasFAttach(); break;
                case "FSupplier":
                    CargarColumnasFSupplier(); break;
                case "FHitos":
                    CargarColumnasFHitos(); break;
                case "FNotes":
                    CargarColumnasFNotes(); break;
                case "FDeliverys":
                    CargarColumnasFDelivery(); break;
            }
        }
        public void FormatearGrid()
        {
            Formatear();
        }

    }
}
