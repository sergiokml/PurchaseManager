
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public ConfigApp ConfigApp { get; set; }


        public FachadaHeader(IPerfilActions perfilActions, ConfigApp configApp)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
            ConfigApp = configApp;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="headerDR"></param>
        /// <param name="campo"></param>
        public void UpdateItem(object newValue, DataRow headerDR, string campo)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            RequisitionHeader pr;
            OrderHeader po;
            int res = 0;
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
                            switch (campo)
                            {
                                case "Description":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2)
                                    {
                                        Fprpal
                                            .Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return;
                                    }
                                    po.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                                    res = PerfilActions.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "Type":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2)
                                    {
                                        Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning);
                                        Fprpal.IsSending = true;
                                        return;
                                    }
                                    po.Type = Convert.ToByte(newValue);
                                    res = PerfilActions.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "Net":
                                    Fprpal.Msg("ERROR_UPDATE", MsgProceso.Warning); return;
                                case "SupplierID":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2)
                                    {
                                        Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return;
                                    }
                                    po.SupplierID = newValue.ToString();
                                    res = PerfilActions.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "CurrencyID":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2)
                                    {
                                        Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return;
                                    }
                                    po.CurrencyID = newValue.ToString();
                                    res = PerfilActions.UpdateItemHeader<OrderHeader>(po);
                                    break;
                            }
                            if (res == 3) // Return 3
                            {
                                Fprpal.Msg("Update OK.", MsgProceso.Informacion); break;
                            }
                            else
                            {
                                Fprpal.Msg("ERROR_UPDATE", MsgProceso.Warning); return;
                            }
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            switch (campo)
                            {
                                case "Description":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2)
                                    {
                                        Fprpal
                                            .Msg("The 'status' of the Purchase Requisition is not allowed.", MsgProceso.Warning); return;
                                    }
                                    pr.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                                    res = PerfilActions.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                                case "Type":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2)
                                    {
                                        Fprpal
                                            .Msg("The 'status' of the Purchase Requisition is not allowed.", MsgProceso.Warning); return;
                                    }
                                    pr.Type = Convert.ToByte(newValue);
                                    res = PerfilActions.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                                case "CurrencyID":
                                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                                case "UserPO":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2)
                                    {
                                        Fprpal
                                            .Msg("The 'status' of the Purchase Requisition is not allowed.", MsgProceso.Warning); return;
                                    }
                                    pr.UserPO = newValue.ToString();
                                    res = PerfilActions.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            Fprpal
                                .Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                    }
                    if (res == 3) // Return 3
                    {
                        Fprpal
                            .Msg("Update OK.", MsgProceso.Informacion); break;
                    }
                    else
                    {
                        Fprpal
                            .Msg("ERROR_UPDATE", MsgProceso.Warning); return;
                    }
                case EPerfiles.VAL:
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerDR"></param>
        /// <param name="fPrincipal"></param>
        public void DeleteItem(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"You are going to delete document N° {headerID}.");
            mensaje.AppendLine();
            var f = new FMensajes(Fprpal);
            int res;
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
                            var po = new OrderHeader().GetById(headerID);
                            if (PerfilActions.CurrentUser.UserID != headerDR["UserID"].ToString()) //! User ID viene de la vista.
                            {
                                Fprpal
                                    .Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                            }
                            if (po.StatusID >= 2)
                            {
                                Fprpal
                                    .Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return;
                            }
                            if (po.RequisitionHeaderID != null)
                            {
                                mensaje.AppendLine($"The PR N°{po.RequisitionHeaderID} will be 'Active' again.");
                                mensaje.AppendLine();
                                mensaje.AppendLine("Are You Sure?");
                            }
                            else
                            {
                                mensaje.AppendLine();
                                mensaje.AppendLine("Are You Sure?");
                            }
                            f.Mensaje = mensaje;
                            f.ShowDialog();
                            if (f.Resultado == DialogResult.Cancel)
                            {
                                return;
                            }
                            res = PerfilActions.DeleteItemHeader<OrderHeader>(po);
                            if (res > 0) //! Return Indeterminado
                            {
                                //! Eliminar Files in Folder
                                try
                                {
                                    DirectoryInfo dir = new DirectoryInfo($"{ConfigApp.FolderApp}{headerID}");
                                    if (dir.Exists)
                                    {
                                        foreach (var file in dir.GetFiles())
                                        {
                                            file.Attributes = FileAttributes.Normal;
                                        }
                                        dir.Delete(true);
                                    }
                                }
                                catch (Exception) { return; }
                                Fprpal
                               .Msg("Delete OK.", MsgProceso.Informacion); break;
                            }
                            else
                            {
                                Fprpal
                                    .Msg("ERROR_DELETE", MsgProceso.Warning); break; // En caso de error tiene que volver a cargarse la Grilla!
                            }
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            if (PerfilActions.CurrentUser.UserID != headerDR["UserID"].ToString())
                            {
                                Fprpal
                                    .Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                            }
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2)
                            {
                                Fprpal
                                    .Msg("The 'status' of the Purchase Requisition is not allowed.", MsgProceso.Warning); return;
                            }
                            mensaje.AppendLine("Are You Sure?");
                            f.Mensaje = mensaje;
                            f.ShowDialog(Fprpal);
                            if (f.Resultado == DialogResult.Cancel)
                            {
                                return;
                            }
                            res = PerfilActions.DeleteItemHeader<RequisitionHeader>(pr);
                            if (res > 0) // Return Indeterminado
                            {
                                //! Eliminar Files in Folder
                                try
                                {
                                    DirectoryInfo dir = new DirectoryInfo($"{ConfigApp.FolderApp}{headerID}");
                                    if (dir.Exists)
                                    {
                                        foreach (var file in dir.GetFiles())
                                        {
                                            file.Attributes = FileAttributes.Normal;
                                        }
                                        dir.Delete(true);
                                    }
                                }
                                catch (Exception) { return; }
                                Fprpal
                               .Msg("Delete OK.", MsgProceso.Informacion); break;
                            }
                            else
                            {
                                Fprpal
                                    .Msg("ERROR_DELETE", MsgProceso.Warning); break;
                            }
                        case TypeDocumentHeader.PO:
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                    }
                    break;
                case EPerfiles.VAL:
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
        }

    }
}
