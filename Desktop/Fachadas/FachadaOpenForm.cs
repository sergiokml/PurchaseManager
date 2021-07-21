using System;
using System.Windows.Forms;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaOpenForm
    {
        public FPrincipal Fprpal { get; set; }
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }

        public FachadaOpenForm(IPerfilActions perfilActions)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
        }

        public FPrincipal OpenPrincipalForm(Fachada facade)
        {
            Fprpal = new FPrincipal(facade)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            //facade.FachadaHeader.Fprpal = Fprpal;
            //facade.FachadaOpenForm.Fprpal = Fprpal;
            return Fprpal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fDetails"></param>
        public void OPenDetailForm(FDetails fDetails)
        {
            fDetails.ShowInTaskbar = false;
            fDetails.StartPosition = FormStartPosition.CenterParent;
            fDetails.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    fDetails.ShowDialog(Fprpal);
                    break;
                case EPerfiles.UPR:
                    fDetails.ShowDialog(Fprpal);
                    break;
                case EPerfiles.VAL:
                    fDetails.ShowDialog(Fprpal);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fAttach"></param>
        public void OpenAttachForm(FAttach fAttach)
        {
            fAttach.ShowInTaskbar = false;
            fAttach.StartPosition = FormStartPosition.CenterParent;
            fAttach.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    fAttach.ShowDialog(Fprpal);
                    break;
                case EPerfiles.UPR:
                    fAttach.ShowDialog(Fprpal);
                    break;
                case EPerfiles.VAL:
                    fAttach.ShowDialog(Fprpal);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fSupplier"></param>
        public void OpenSupplierForm(FSupplier fSupplier)
        {
            fSupplier.ShowInTaskbar = false;
            fSupplier.StartPosition = FormStartPosition.CenterParent;
            fSupplier.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
            Enum.TryParse(fSupplier.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            fSupplier.ShowDialog(Fprpal);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.VAL:
                    fSupplier.ShowDialog(Fprpal);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fHitos"></param>
        public void OpenHitosForm(FHitos fHitos)
        {
            fHitos.ShowInTaskbar = false;
            fHitos.StartPosition = FormStartPosition.CenterParent;
            fHitos.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
            Enum.TryParse(fHitos.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            fHitos.ShowDialog(Fprpal);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.VAL:
                    fHitos.ShowDialog(Fprpal);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fNotes"></param>
        public void OpenNotesForm(FNotes fNotes)
        {
            fNotes.ShowInTaskbar = false;
            fNotes.StartPosition = FormStartPosition.CenterParent;
            fNotes.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    fNotes.ShowDialog(Fprpal);
                    break;
                case EPerfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.VAL:
                    fNotes.ShowDialog(Fprpal);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fDelivery"></param>
        public void OpenDeliveryForm(FDeliverys fDelivery)
        {
            fDelivery.ShowInTaskbar = false;
            fDelivery.StartPosition = FormStartPosition.CenterParent;
            fDelivery.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
            Enum.TryParse(fDelivery.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
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
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            fDelivery.ShowDialog(Fprpal);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.VAL:
                    fDelivery.ShowDialog(Fprpal);
                    break;
            }
        }

    }
}


