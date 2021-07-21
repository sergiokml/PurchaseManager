
using System;

using PurchaseData.DataModel;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaHeader
    {
        public FPrincipal Fprpal { get; set; }
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }

        public FachadaHeader(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="type"></param>
        public void InsertItem(Companies company, TypeDocument type)
        {
            int res = 0;
            switch (CurrentPerfil) //TODO ACA EL PERFILACTION TIENE EL CURRENT USER Y CURREN PERFIL?
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    var po = new OrderHeader
                    {
                        Type = type.TypeID,
                        StatusID = 1,
                        Net = 0,
                        Exent = 0,
                        CompanyID = company.CompanyID,
                        Discount = 0
                    };
                    res = PerfilActions.InsertItemHeader(po);
                    break;
                case EPerfiles.UPR:
                    var pr = new RequisitionHeader
                    {
                        Type = type.TypeID,
                        CompanyID = company.CompanyID,
                        StatusID = 1
                    };
                    res = PerfilActions.InsertItemHeader(pr);
                    break;
                case EPerfiles.VAL:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;

            }
            if (res == 3) // Return 3
            {
                Fprpal
                    .Msg("Insert OK.", MsgProceso.Informacion);
            }
            else
            {
                Fprpal
                    .Msg("ERROR_INSERT", MsgProceso.Warning); return;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
        }
    }
}
