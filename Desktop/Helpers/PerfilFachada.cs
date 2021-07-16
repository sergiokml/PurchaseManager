using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bunifu.Charts.WinForms.ChartTypes;

using PurchaseData.DataModel;
using PurchaseData.Helpers;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Profiles;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Helpers
{
    public class PerfilFachada
    {
        protected UserProfileUPR perfilPr;
        protected UserProfileUPO perfilPo;
        protected UserProfileVAL perfilVal;
        private readonly Perfiles currentPerfil;
        private readonly TextInfo UCase = CultureInfo.InvariantCulture.TextInfo;
        private readonly ConfigApp configApp;

        public PerfilFachada(UserProfileUPR upr, UserProfileUPO upo, UserProfileVAL val, Users user)
        {
            configApp = new ConfigApp().GetConfigApp();
            perfilPr = upr;
            perfilPo = upo;
            perfilVal = val;
            Enum.TryParse(user.ProfileID, out Perfiles p);
            currentPerfil = p;
            upr.CurrentUser = user;
            upo.CurrentUser = user;
            val.CurrentUser = user;
        }

        #region Cargar Controles y Acciones

        public void CargarGrid(iGrid grid, string form, DataRow headerDR)
        {

            switch (form)
            {
                case "FPrincipal":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.Grid = grid;
                            perfilPo.PintarGrid();
                            perfilPo.CargarColumnasFPrincipal(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.Grid = grid;
                            perfilPr.PintarGrid();
                            perfilPr.CargarColumnasFPrincipal(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.Grid = grid;
                            perfilVal.PintarGrid();
                            perfilVal.CargarColumnasFPrincipal(Perfiles.VAL);
                            break;
                        default:
                            break;
                    }
                    break;
                case "FDetails":
                    Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.CargarColumnasFDetail(Perfiles.UPO, td);
                            break;
                        case Perfiles.UPR:
                            perfilPr.CargarColumnasFDetail(Perfiles.UPR, td);
                            break;
                        case Perfiles.VAL:
                            perfilVal.CargarColumnasFDetail(Perfiles.VAL, td);
                            break;
                        default:
                            break;
                    }
                    break;
                case "FAttach":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.CargarColumnasFAttach(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.CargarColumnasFAttach(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.CargarColumnasFAttach(Perfiles.VAL);
                            break;
                        default:
                            break;
                    }
                    break;
                case "FSupplier":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.CargarColumnasFSupplier(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.CargarColumnasFSupplier(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.CargarColumnasFSupplier(Perfiles.VAL);
                            break;
                        default:
                            break;
                    }
                    break;
                case "FHitos":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.CargarColumnasFHitos(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.CargarColumnasFHitos(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.CargarColumnasFHitos(Perfiles.VAL);
                            break;
                        default:
                            break;
                    }
                    break;
                case "FNotes":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.CargarColumnasFNotes(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.CargarColumnasFNotes(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.CargarColumnasFNotes(Perfiles.VAL);
                            break;
                        default:
                            break;
                    }
                    break;
                case "FDelivery":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            perfilPo.CargarColumnasFDelivery(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.CargarColumnasFDelivery(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.CargarColumnasFDelivery(Perfiles.VAL);
                            break;
                        default:
                            break;
                    }
                    break;
            }




            //switch (currentPerfil)
            //{
            //    case Perfiles.ADM:
            //        break;
            //    case Perfiles.BAS:
            //        break;
            //    case Perfiles.UPO:
            //        perfilPo.Grid = grid;
            //        perfilPo.PintarGrid();
            //        switch (form)
            //        {
            //            case "FPrincipal":
            //                perfilPo.CargarColumnasFPrincipal(Perfiles.UPO);
            //                break;
            //            case "FDetails":
            //                Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            //                perfilPo.CargarColumnasFDetail(Perfiles.UPO, td);
            //                break;
            //            case "FAttach":
            //                perfilPo.CargarColumnasFAttach(Perfiles.UPO);
            //                break;
            //            case "FSupplier":
            //                perfilPo.CargarColumnasFSupplier(Perfiles.UPO);
            //                break;
            //            case "FHitos":
            //                perfilPo.CargarColumnasFHitos(Perfiles.UPO);
            //                break;
            //            case "FNotes":
            //                perfilPo.CargarColumnasFNotes(Perfiles.UPO);
            //                break;
            //        }
            //        break;
            //    case Perfiles.UPR:
            //        perfilPr.Grid = grid;
            //        perfilPr.PintarGrid();
            //        switch (form)
            //        {
            //            case "FPrincipal":
            //                perfilPr.CargarColumnasFPrincipal(Perfiles.UPR);
            //                break;
            //            case "FDetails":
            //                Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            //                perfilPr.CargarColumnasFDetail(Perfiles.UPR, td);
            //                break;
            //            case "FAttach":
            //                perfilPr.CargarColumnasFAttach(Perfiles.UPR);
            //                break;
            //            case "FSupplier":
            //                perfilPr.CargarColumnasFSupplier(Perfiles.UPR);
            //                break;
            //            case "FHitos":
            //                perfilPr.CargarColumnasFHitos(Perfiles.UPR);
            //                break;
            //            case "FNotes":
            //                perfilPr.CargarColumnasFNotes(Perfiles.UPO);
            //                break;
            //        }
            //        break;
            //    case Perfiles.VAL:
            //        perfilPr.Grid = grid;
            //        perfilPr.PintarGrid();
            //        switch (form)
            //        {
            //            case "FPrincipal":
            //                perfilPr.CargarColumnasFPrincipal(Perfiles.VAL);
            //                break;
            //            case "FDetails":
            //                Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            //                perfilPr.CargarColumnasFDetail(Perfiles.VAL, td);
            //                break;
            //            case "FAttach":
            //                perfilPr.CargarColumnasFAttach(Perfiles.VAL);
            //                break;
            //            case "FSupplier":
            //                perfilPr.CargarColumnasFSupplier(Perfiles.VAL);
            //                break;
            //            case "FHitos":
            //                perfilPr.CargarColumnasFHitos(Perfiles.VAL);
            //                break;
            //            case "FNotes":
            //                perfilPr.CargarColumnasFNotes(Perfiles.VAL);
            //                break;
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }

        public void CargarContextMenuStrip(ContextMenuStrip context, DataRow headerDR)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.CtxMenu = context;
                    perfilPo.CtxMenu.Items.Clear();
                    perfilPo.LLenarMenuContext(Perfiles.UPO, headerDR);
                    break;
                case Perfiles.UPR:
                    //! No hay opciones para User PR
                    break;
                case Perfiles.VAL:
                    perfilPo.CtxMenu = context;
                    perfilPo.CtxMenu.Items.Clear();
                    perfilPo.LLenarMenuContext(Perfiles.VAL, headerDR);

                    break;
                default:
                    break;
            }
        }

        public void CargarDashBoard(iGrid grid, BunifuPieChart c1, BunifuPieChart c2, Panel panelDash)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = grid;
                    perfilPo.CargarDatos(perfilPo.CurrentUser, c1, c2, panelDash);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = grid;
                    perfilPr.CargarDatos(perfilPr.CurrentUser, c1, c2, panelDash);
                    break;
                case Perfiles.VAL:
                    perfilVal.Grid = grid;
                    perfilVal.CargarDatos(perfilVal.CurrentUser, c1, c2, panelDash);
                    break;
                default:
                    break;
            }
        }

        public Users CurrentUser()
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.CurrentUser;
                case Perfiles.UPR:
                    return perfilPr.CurrentUser;
                case Perfiles.VAL:
                    return perfilVal.CurrentUser;
                default:
                    break;
            }
            return null;
        }

        public string VerItemHtml(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilPo.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.OrderHitos.Count == 0)
                            {
                                return "This Purchase Order does not contain a 'Hito'.";
                            }
                            if (po.SupplierID == null)
                            {
                                return "The Purchase Order does not contain a 'Supplier'.";
                            }
                            if (po.OrderDetails.Count <= 0)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            if (po.CurrencyID == null)
                            {
                                return "This Purchase Order does not contain a 'Currency'.";
                            }
                            if (po.Net <= 0)
                            {
                                return "This Purchase Order does not contain a 'Net'.";
                            }

                            var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                            foreach (var item in listaPath)
                            {
                                Process.Start(item);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilPr.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            //! User PR abre las PO que se hicieron desde sus PR
                            var po = new OrderHeader().GetById(headerID);
                            //TODO El estado evita que se abra PO en estado DRAFT, o sea sin SUPPLIER (Error en Html).
                            if (po.StatusID < 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            foreach (var item in new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po))
                            {
                                Process.Start(item);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.VAL:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilVal.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            foreach (var item in new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilVal.CurrentUser, po))
                            {
                                Process.Start(item);
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
            return "OK";
        }

        public async Task SendItem(DataRow headerDR, FPrincipal fPrincipal)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"The {headerDR["TypeDocumentHeader"]} N° {headerID} will be sent to your own inbox.");
            mensaje.AppendLine();
            mensaje.AppendLine("Are You Sure?");
            var f = new FMensajes(fPrincipal);

            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            string path;
            SendEmailTo send = null;

            switch (td)
            {
                case TypeDocumentHeader.PR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.RequisitionDetails.Count == 0)
                    {
                        fPrincipal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning);
                    }

                    f.Mensaje = mensaje;
                    f.ShowDialog();
                    //if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }
                    if (f.Resultado == DialogResult.Cancel)
                    {
                        fPrincipal.Msg("", MsgProceso.Empty);
                        fPrincipal.IsSending = false;
                    }
                    fPrincipal.IsSending = true;
                    fPrincipal.LblMsg.Text = string.Empty;
                    fPrincipal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    fPrincipal.LblMsg.Image = Properties.Resources.loading;

                    path = new HtmlManipulate(configApp)
                        .ReemplazarDatos(headerDR, perfilPr.CurrentUser, pr);
                    send = new SendEmailTo(configApp);
                    await send.SendEmail(path, "Purchase Manager: PR document ", perfilPr.CurrentUser);
                    fPrincipal.Msg(send.MessageResult, MsgProceso.Send);
                    fPrincipal.IsSending = false;
                    break;
                case TypeDocumentHeader.PO:
                    var po = new OrderHeader().GetById(headerID);
                    //TODO El estado evita que se abra PO en estado DRAFT, o sea sin SUPPLIER (Error en Html).
                    //TODO NO sirve ni para PO aunque esten en estado borrador y bien emitidas.
                    if (po.StatusID < 2) { fPrincipal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); }
                    f.Mensaje = mensaje;
                    f.ShowDialog(fPrincipal);
                    // if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }
                    if (f.Resultado == DialogResult.Cancel)
                    {
                        fPrincipal.Msg("", MsgProceso.Empty);
                        fPrincipal.IsSending = false;
                    }
                    fPrincipal.IsSending = true;
                    fPrincipal.LblMsg.Text = string.Empty;
                    fPrincipal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    fPrincipal.LblMsg.Image = Properties.Resources.loading;

                    var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                    var a = await new HtmlManipulate(configApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                    send = new SendEmailTo(configApp);
                    await send.SendEmail(a, $"Purchase Manager:  PO document", perfilPr.CurrentUser);
                    fPrincipal.Msg(send.MessageResult, MsgProceso.Send);
                    fPrincipal.IsSending = false;

                    break;
                default:
                    break;
            }
            // return send.MessageResult;
        }

        public void GridDobleClick(DataRow headerDR, FPrincipal fPrincipal)
        {
            //TODO USAR BREAK PARA QUE UPDATE EL FPRINCIPAL / USAR RETURN PARA VOLVER SIN NADA.
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            RequisitionHeader pr;
            OrderHeader po;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    //! Update Status 1 o 2
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            fPrincipal.Msg("Your profile does not allow you to complete this action.", Proceso.Warning); break;
                        case TypeDocumentHeader.PO:
                            po = new OrderHeader().GetById(headerID);
                            //if (po.StatusID >= 3) { return "The 'status' of the Purchase Order is not allowed."; }
                            if (po.StatusID == 1)
                            {
                                if (po.OrderDetails.Count == 0)
                                {
                                    fPrincipal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); break;
                                }
                                if (po.Net <= 0)
                                {
                                    fPrincipal.Msg("Net cannot be 'zero'.", MsgProceso.Warning); break;
                                }
                                if (Convert.ToInt32(headerDR["HitosCount"]) == 0)
                                {
                                    fPrincipal.Msg("This Purchase Order does not contain a 'Hito'.", MsgProceso.Warning); break;
                                }
                                if (po.Description == null)
                                {
                                    fPrincipal.Msg("Please enter 'Description'.", MsgProceso.Warning); break;
                                }
                                if (po.SupplierID == null)
                                {
                                    fPrincipal.Msg("Please enter 'Supplier'.", MsgProceso.Warning); break;
                                }
                                if (po.CurrencyID == null)
                                {
                                    fPrincipal.Msg("Please enter 'Currency'.", MsgProceso.Warning); break;
                                }
                                po.StatusID = 2;
                                if (perfilPo.UpdateItemHeader<OrderHeader>(po) > 0)
                                {
                                    fPrincipal.Msg("Update OK.", MsgProceso.Warning); break;
                                }
                            }
                            else if (po.StatusID == 2)
                            {
                                po.StatusID = 1;
                                perfilPo.UpdateItemHeader<OrderHeader>(po); break;
                            }
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    //! Update Status
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:

                            pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count == 0)
                            {
                                fPrincipal
                                    .Msg("This Purchase Requesition does not contain Products or Services.", MsgProceso.Warning); break;
                            }
                            if (pr.StatusID == 1)
                            {
                                if (headerDR["Description"] == DBNull.Value)
                                {
                                    fPrincipal.Msg("Please enter 'Description'.", MsgProceso.Warning); break;
                                }
                                if (headerDR["UserPO"] == DBNull.Value)
                                {
                                    fPrincipal.Msg("Please enter 'UserPO'.", MsgProceso.Warning); break;
                                }
                                pr.StatusID = 2;
                                if (perfilPr.UpdateItemHeader<RequisitionHeader>(pr) > 0)
                                {
                                    fPrincipal.Msg("Update OK.", MsgProceso.Informacion); break;
                                }
                            }
                            else if (pr.StatusID == 2)
                            {
                                pr.StatusID = 1;
                                var res = perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
                                if (res == 3) // Return 3
                                {
                                    fPrincipal.Msg("Update OK.", MsgProceso.Informacion);
                                }
                                else
                                {
                                    fPrincipal.Msg("ERROR_UPDATE", MsgProceso.Informacion); return;
                                }
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            fPrincipal.Msg("Your profile does not allow you to complete this action..", MsgProceso.Warning); break;
                    }
                    break;
                case Perfiles.VAL:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            break;
                        case TypeDocumentHeader.PO:
                            //TODO ESTO SE HARÁ CON MENPU CONTEXTUAL => ACEPTAR O RECHAZAR
                            po = new OrderHeader().GetById(headerID);
                            if (po.StatusID == 2)
                            {
                                po.StatusID = 3;
                                perfilVal.UpdateItemHeader<OrderHeader>(po);
                            }
                            else if (po.StatusID >= 3)
                            {
                                fPrincipal.Msg("Your profile does not allow you to complete this action..", MsgProceso.Warning);
                            }

                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            fPrincipal.LlenarGrid();
            fPrincipal.ClearControles();
            fPrincipal.SetControles();
        }

        public async Task SeleccionarContextMenuStripAsync(DataRow headerDR, string action, FPrincipal fPrincipal)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            RequisitionHeader pr;
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            OrderHeader po;

            switch (action)
            {
                case "CONVERTREQ":
                    if (!Directory.Exists(configApp.FolderApp + headerID))
                    {
                        Directory.CreateDirectory(configApp.FolderApp + headerID);
                    }
                    //! Crear una PO desde una PR
                    pr = new RequisitionHeader().GetById(headerID);
                    if (pr != null && pr.StatusID == 2)
                    {
                        //TODO  NO SE DONDE PONER ESTA ACCION, POR EL MOMENTO FUNCIONA OK!
                        using (var rContext = new PurchaseManagerEntities())
                        {
                            using (DbContextTransaction trans = rContext.Database.BeginTransaction())
                            {
                                var respuesta = rContext
                                 .CONVERT_PR(pr.Description, pr.Type, 0, 0, 1, pr.RequisitionHeaderID, pr.CompanyID, 0);
                                po = rContext.OrderHeader.Where(c => c.RequisitionHeaderID == pr.RequisitionHeaderID).Single();
                                Transactions transaction = new Transactions
                                {
                                    Event = Eventos.CREATE_PO.ToString(),
                                    UserID = CurrentUser().UserID,
                                    DateTran = rContext.Database
                                    .SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                                };
                                po.Transactions.Add(transaction);
                                rContext.SaveChanges();
                                trans.Commit();
                            }
                        }
                        //fPrincipal.Msg("", MsgProceso.Empty);
                        fPrincipal.LlenarGrid();
                        fPrincipal.ClearControles();
                        fPrincipal.SetControles();
                    }
                    break;
                case "OPENREQ":
                    //! Estoy en posicion de PO, quiero traer el DataRow de la PR... (botón derecho)
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            break;
                        case Perfiles.BAS:
                            break;
                        case Perfiles.UPO:
                            if (headerDR["RequisitionHeaderID"] != DBNull.Value)
                            {
                                DataRow d = perfilPo
                                    .GetDataRow(TypeDocumentHeader.PR, Convert.ToInt32(headerDR["RequisitionHeaderID"]));
                                VerItemHtml(d);
                            }
                            break;
                        case Perfiles.UPR:
                            break;
                        case Perfiles.VAL:
                            if (headerDR["RequisitionHeaderID"] != DBNull.Value)
                            {
                                DataRow e = perfilVal
                                    .GetDataRow(TypeDocumentHeader.PR, Convert.ToInt32(headerDR["RequisitionHeaderID"]));
                                VerItemHtml(e);
                            }
                            break;

                        default:
                            break;
                    }
                    break;
                case "SEND":
                    if (!fPrincipal.IsSending)
                    {
                        po = new OrderHeader().GetById(headerID);
                        if (po.StatusID == 3)
                        {
                            StringBuilder mensaje = new StringBuilder();
                            var supp = new Suppliers().GetByID(headerDR["SupplierID"].ToString());
                            mensaje.AppendLine($"Email To: {supp.Name} ({supp.Email})");
                            mensaje.AppendLine($"Purchase Order Code N° {po.Code} (Status: {po.OrderStatus.Description})");
                            mensaje.AppendLine();
                            mensaje.AppendLine("Are You Sure?");
                            var f = new FMensajes(fPrincipal)
                            {
                                Mensaje = mensaje
                            };
                            f.ShowDialog(fPrincipal);
                            if (f.Resultado == DialogResult.OK)
                            {
                                fPrincipal.IsSending = true;
                                fPrincipal.LblMsg.Text = string.Empty;
                                fPrincipal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                                fPrincipal.LblMsg.Image = Properties.Resources.loading;

                                //!PDF de la PO        
                                var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                                var a = await new HtmlManipulate(configApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                                //! Adjuntos
                                //TODO  QUITAR ESTO Y USAR LA REFERENCIA?
                                List<Attaches> att = new Attaches().GetListByPoID(headerID).Where(c => c.Modifier == 1).ToList();
                                //! Email To Supplier
                                var send = new SendEmailTo(configApp);
                                var asunto = $"Orden de Compra {po.Code} ({po.CompanyID})";
                                await send.SendEmailToSupplier(a, asunto, perfilPr.CurrentUser, supp, att);
                                //! Update the PO
                                po.StatusID = 4;
                                perfilVal.UpdateItemHeader<OrderHeader>(po);

                                fPrincipal.Msg(send.MessageResult, MsgProceso.Send);
                                fPrincipal.IsSending = false;
                            }
                        }
                    }
                    break;
                case "VALIDATED":
                    po = new OrderHeader().GetById(headerID);
                    if (po.StatusID == 2) // 2  Active
                    {
                        po.StatusID = 3;
                        perfilVal.UpdateItemHeader<OrderHeader>(po);
                    }
                    break;
                case "REJECTED":
                    po = new OrderHeader().GetById(headerID);
                    if (po.StatusID == 2) // 2  Active
                    {
                        po.StatusID = 7;
                        perfilVal.UpdateItemHeader<OrderHeader>(po);
                    }
                    break;
                case "ACCEPTED":
                    po = new OrderHeader().GetById(headerID);
                    if (po.StatusID == 4) // 4 Enviada
                    {
                        po.StatusID = 5;
                        perfilVal.UpdateItemHeader<OrderHeader>(po);
                    }
                    break;
                case "COMPLETE":
                    po = new OrderHeader().GetById(headerID);
                    if (po.StatusID == 5) // 5  Aceptada
                    {
                        po.StatusID = 6;
                        perfilVal.UpdateItemHeader<OrderHeader>(po);
                    }
                    break;
                default:
                    break;
            }
            //return "OK";
        }

        public async Task<string> CargarBanner()
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return await new HtmlManipulate(configApp).ReemplazarDatos();
                case Perfiles.UPR:
                    return await new HtmlManipulate(configApp).ReemplazarDatos();
                case Perfiles.VAL:
                    return await new HtmlManipulate(configApp).ReemplazarDatos();
                default:
                    break;
            }
            return string.Empty;
        }

        #endregion

        #region Abrir Formularios

        public void OPenDetailForm(FDetails fDetails, FPrincipal fPrincipal)
        {
            fDetails.ShowInTaskbar = false;
            fDetails.StartPosition = FormStartPosition.CenterParent;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = fDetails.GetGrid();
                    fDetails.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                    perfilPo.PintarGrid();
                    fDetails.ShowDialog(fPrincipal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fDetails.GetGrid();
                    fDetails.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                    perfilPr.PintarGrid();
                    fDetails.ShowDialog(fPrincipal);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
        }

        public string OpenAttachForm(FAttach fAttach, FPrincipal fPrincipal)
        {
            fAttach.ShowInTaskbar = false;
            fAttach.StartPosition = FormStartPosition.CenterParent;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = fAttach.GetGrid();
                    fAttach.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                    perfilPo.PintarGrid();
                    fAttach.ShowDialog(fPrincipal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fAttach.GetGrid();
                    fAttach.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                    perfilPr.PintarGrid();
                    fAttach.ShowDialog(fPrincipal);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string OpenSupplierForm(FSupplier fSupplier, FPrincipal fPrincipal)
        {
            fSupplier.ShowInTaskbar = false;
            fSupplier.StartPosition = FormStartPosition.CenterParent;
            Enum.TryParse(fSupplier.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            perfilPo.Grid = fSupplier.GetGrid();
                            fSupplier.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                            perfilPo.PintarGrid();
                            fSupplier.ShowDialog(fPrincipal);
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    return "Your profile does not allow you to complete this action.";
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string OpenHitosForm(FHitos fHitos, FPrincipal fPrincipal)
        {
            fHitos.ShowInTaskbar = false;
            fHitos.StartPosition = FormStartPosition.CenterParent;
            Enum.TryParse(fHitos.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);

            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            perfilPo.Grid = fHitos.GetGrid();
                            fHitos.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                            perfilPo.PintarGrid();
                            fHitos.ShowDialog(fPrincipal);
                            break;
                        default:
                            break;
                    }

                    break;
                case Perfiles.UPR:
                    return "Your profile does not allow you to complete this action.";
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string OpenNotesForm(FNotes fNotes, FPrincipal fPrincipal)
        {
            fNotes.ShowInTaskbar = false;
            fNotes.StartPosition = FormStartPosition.CenterParent;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = fNotes.GetGrid();
                    fNotes.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                    perfilPo.PintarGrid();
                    fNotes.ShowDialog(fPrincipal);
                    break;
                case Perfiles.UPR:
                    return "Your profile does not allow you to complete this action.";
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string OpenDeliveryForm(FDeliverys fDelivery, FPrincipal fPrincipal)
        {
            fDelivery.ShowInTaskbar = false;
            fDelivery.StartPosition = FormStartPosition.CenterParent;
            Enum.TryParse(fDelivery.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);

            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            perfilPo.Grid = fDelivery.GetGrid();
                            fDelivery.CurRowPrincipal = fPrincipal.GetGrid().CurRow;
                            perfilPo.PintarGrid();
                            fDelivery.ShowDialog(fPrincipal);
                            break;
                        default:
                            break;
                    }

                    break;
                case Perfiles.UPR:
                    return "Your profile does not allow you to complete this action.";
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        #endregion

        #region Vistas Formularios

        public DataTable GetVistaFPrincipal()
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaFPrincipal();
                case Perfiles.UPR:
                    return perfilPr.VistaFPrincipal();
                case Perfiles.VAL:
                    return perfilVal.VistaFPrincipal();
            }
            return null;
        }

        public DataTable GetVistaDetalles(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaFDetalles(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.UPR:
                    return perfilPr.VistaFDetalles(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.VAL:
                    break;
            }
            return null;
        }

        public DataTable GetVistaHitos(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaFHitos(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
            }
            return null;

        }

        public DataTable GetVistaNotes(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaFNotes(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
            }
            return null;

        }

        public DataTable GetVistaAttaches(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaFAdjuntos(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.UPR:
                    return perfilPr.VistaFAdjuntos(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.VAL:
                    break;
            }
            return null;
        }

        public DataTable GetVistaSuppliers(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaFProveedores(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.UPR:
                    return perfilPr.VistaFProveedores(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.VAL:
                    break;
            }
            return null;
        }

        public DataTable GetVistaDelivery(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    return perfilPo.VistaDelivery(td, Convert.ToInt32(headerDR["headerID"]));
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
            }
            return null;

        }

        #endregion

        #region IGrid
        public iGrid FormatearGrid(iGrid grid, string frm, DataRow headerDR)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = grid;
                    perfilPo.Formatear(Perfiles.UPO, frm, headerDR);
                    return perfilPo.Grid;
                case Perfiles.UPR:
                    perfilPr.Grid = grid;
                    perfilPr.Formatear(Perfiles.UPR, frm, headerDR);
                    return perfilPr.Grid;
                case Perfiles.VAL:
                    perfilVal.Grid = grid;
                    perfilVal.Formatear(Perfiles.VAL, frm, headerDR);
                    return perfilVal.Grid;
                default:
                    break;
            }
            return grid;
        }

        #endregion

        #region Header CRUD

        public bool InsertItem(Companies company, TypeDocument type)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var po = new OrderHeader
                    {
                        Type = type.TypeID,
                        StatusID = 1,
                        Net = 0,
                        Exent = 0,
                        CompanyID = company.CompanyID,
                        Discount = 0
                    };
                    perfilPo.InsertItemHeader(po);
                    return true;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader
                    {
                        Type = type.TypeID,
                        CompanyID = company.CompanyID,
                        StatusID = 1
                    };
                    perfilPr.InsertItemHeader(pr);
                    return true;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }

        public string UpdateItem(object newValue, DataRow headerDR, string campo)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            RequisitionHeader pr;
            OrderHeader po;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:

                            switch (campo)
                            {
                                case "Description":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    po.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                                    perfilPo.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "Type":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    po.Type = Convert.ToByte(newValue);
                                    perfilPo.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "Net":
                                    //po = new OrderHeader().GetById(headerID);
                                    //po.Net = Convert.ToInt32(newValue);
                                    //perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
                                    break;
                                case "SupplierID":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    po.SupplierID = newValue.ToString();
                                    perfilPo.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "CurrencyID":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    po.CurrencyID = newValue.ToString();
                                    perfilPo.UpdateItemHeader<OrderHeader>(po);
                                    break;
                            }

                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            switch (campo)
                            {
                                case "Description":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    pr.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                                case "Type":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    pr.Type = Convert.ToByte(newValue);
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                                case "CurrencyID":
                                    return "Your profile does not allow you to complete this action.";
                                case "UserPO":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    pr.UserPO = newValue.ToString();
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                        default:
                            break;
                    }

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public void DeleteItem(DataRow headerDR, FPrincipal fPrincipal)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);

            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"You are going to delete document N° {headerID}.");
            mensaje.AppendLine();
            var f = new FMensajes(fPrincipal);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            fPrincipal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Informacion);
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (perfilPo.CurrentUser.UserID != headerDR["UserID"].ToString()) // User ID viene de la vista.
                            {
                                fPrincipal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Informacion); break;
                            }
                            if (po.StatusID >= 2)
                            {
                                fPrincipal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Informacion); break;
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
                            perfilPo.DeleteItemHeader<OrderHeader>(po);
                            break;
                        default:
                            break;
                    }

                    break;
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            if (perfilPr.CurrentUser.UserID != headerDR["UserID"].ToString())
                            {
                                fPrincipal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Informacion);
                            }
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2)
                            {
                                fPrincipal.Msg("The 'status' of the Purchase Requisition is not allowed.", MsgProceso.Informacion);
                            }
                            mensaje.AppendLine("Are You Sure?");
                            f.Mensaje = mensaje;
                            f.ShowDialog(fPrincipal);
                            if (f.Resultado == DialogResult.OK)
                            {
                                perfilPr.DeleteItemHeader<RequisitionHeader>(pr);
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                        default:
                            break;
                    }

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            fPrincipal.LlenarGrid();
            fPrincipal.ClearControles();
            fPrincipal.SetControles();
            fPrincipal.CargarDashboard();

        }

        #endregion

        #region Details CRUD

        public string InsertDetail<T>(T item, DataRow headerDR, FDetails fDetails)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            DataRow d;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            var od = item as OrderDetails;
                            if ((po.Total + od.Total) < 99000000) // Máximo por Header
                            {
                                perfilPo.InsertDetail<OrderDetails>(od, po);
                            }
                            //Set nuevo Current en el formulario Details
                            d = perfilPo.GetDataRow(TypeDocumentHeader.PO, po.OrderHeaderID);
                            fDetails.Current = d;
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                            perfilPr.InsertDetail<RequisitionDetails>(item as RequisitionDetails, pr);
                            //Set nuevo Current en el formulario Details
                            d = perfilPr.GetDataRow(TypeDocumentHeader.PR, pr.RequisitionHeaderID);
                            fDetails.Current = d;

                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                        default:
                            break;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            perfilPo.DeleteDetail(po, detailID);
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    perfilPr.DeleteDetail(pr, detailID);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string UpdateDetail(object newValue, DataRow detailDR, DataRow headerDR, string campo, bool isExent)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var detailID = Convert.ToInt32(detailDR["detailID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
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
                                    perfilPo.UpdateDetail<OrderDetails>(details, po);
                                    break;
                                case "NameProduct":
                                    details.NameProduct = newValue.ToString();
                                    perfilPo.UpdateDetail<OrderDetails>(details, po);
                                    break;


                            }

                            break;
                        default:
                            break;
                    }


                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    var detail = pr.RequisitionDetails.SingleOrDefault(c => c.DetailID == detailID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    switch (campo)
                    {
                        case "NameProduct":
                            detail.NameProduct = newValue.ToString();
                            perfilPr.UpdateDetail<RequisitionDetails>(detail, pr);
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }
        #endregion

        #region Attach CRUD

        public string InsertAttach(Attaches item, DataRow headerDR, string path)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            if (!Directory.Exists(configApp.FolderApp + headerID))
            {
                Directory.CreateDirectory(configApp.FolderApp + headerID);
            }
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    if (File.Exists($"{configApp.FolderApp}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPo.InsertAttach<OrderHeader>(item, po);
                    File.Copy(path, $"{configApp.FolderApp}{item.FileName}", true);
                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    if (File.Exists($"{configApp.FolderApp}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPr.InsertAttach<RequisitionHeader>(item, pr);
                    File.Copy(path, $"{configApp.FolderApp}{item.FileName}", false);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public string DeleteAttach(DataRow attachDR, DataRow headerDR)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var attachID = Convert.ToInt32(attachDR["attachID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    File.Delete($"{configApp.FolderApp}{attachDR["FileName"]}");
                    perfilPo.DeleteAttach(po, attachID);
                    break;
                case Perfiles.UPR:
                    //todo Saber si tiene acceso a la carpeta en el server: ds
                    // DirectorySecurity ds = Directory.GetAccessControl(configApp.FolderApp);
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    File.Delete($"{configApp.FolderApp}{attachDR["FileName"]}");
                    perfilPr.DeleteAttach<RequisitionHeader>(pr, attachID);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }


            return "OK";
        }

        public string OpenAttach(DataRow attachDR)
        {
            try
            {
                Process.Start($"{configApp.FolderApp}{attachDR["FileName"]}");
                return "OK";
            }
            catch (Exception)
            {
                return "The system cannot find the requested path.";
            }
        }

        public string UpdateAttach(object newValue, DataRow attachDR, DataRow headerDR, string campo)
        {
            var headerID = Convert.ToInt32(headerDR["HeaderID"]);
            var attachID = Convert.ToInt32(attachDR["AttachID"]);
            var att = new Attaches().GetByID(attachID);

            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (campo)
                    {
                        case "Description":
                            att.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            perfilPo.UpdateAttaches<OrderHeader>(att, po);
                            break;
                        case "Modifier":
                            att.Modifier = Convert.ToByte(newValue);
                            perfilPo.UpdateAttaches<OrderHeader>(att, po);
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    //var attt = pr.Attaches.FirstOrDefault(c => c.AttachID == attachID);
                    switch (campo)
                    {
                        case "Description":
                            att.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            perfilPr.UpdateAttaches<RequisitionHeader>(att, pr);
                            break;
                        case "Modifier":
                            att.Modifier = Convert.ToByte(newValue);
                            perfilPr.UpdateAttaches<RequisitionHeader>(att, pr);
                            break;

                        default:
                            break;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";

        }
        #endregion

        #region Supplier CRUD

        public string InsertSupplier(Suppliers item)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var res = perfilPo.InsertSupplier(item);
                    if (res == 0)
                    {
                        return "The record being inserted already exists in the table.";
                    }
                    break;
                case Perfiles.UPR:

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public void DeleteSupplier(string headerID, FPrincipal fPrincipal)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    var res = perfilPo.DeleteSupplier(headerID);
                    if (res == 547)
                    {
                        fPrincipal.Msg("It has associated data, it cannot be deleted.", MsgProceso.Empty);
                    }
                    break;
                case Perfiles.UPR:

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            fPrincipal.LlenarGrid();
            fPrincipal.ClearControles();
            fPrincipal.SetControles();
        }

        public string UpdateSupplier(Suppliers item)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.UpdateSupplier(item);
                    break;
                case Perfiles.UPR:

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        #endregion

        #region Hitos CRUD

        public string InsertHito(OrderHitos item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.InsertHito(item, headerID);
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.DeleteHito(headerID, Convert.ToInt32(hitoDR["HitoID"]));
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (campo)
                    {
                        case "Description":
                            h.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            perfilPo.UpdateHito(h, headerID);
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";

        }

        #endregion

        #region Notes CRUD

        public string InsertNote(OrderNotes item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.InsertNote(item, headerID);
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.DeleteNote(headerID, Convert.ToInt32(noteDR["OrderNoteID"]));
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (campo)
                    {
                        case "Modifier":
                            n.Modifier = Convert.ToByte(newValue);
                            perfilPo.UpdateNote(n, headerID);
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";

        }
        #endregion

        #region Delivery CRUD

        public string InsertDelivery(OrderDelivery item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.InsertDelivery(item, headerID);
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.DeleteDelivery(headerID, Convert.ToInt32(noteDR["DeliveryID"]));
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }


        #endregion

        public iGDropDownList CargarComBox(DataRow dataRow)
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":
            //        return perfilPo.GetStatusItem(dataRow);
            //    case "UPR":
            //        break;
            //    case "VAL":
            //        break;
            //}
            return null;
        }

    }
}
