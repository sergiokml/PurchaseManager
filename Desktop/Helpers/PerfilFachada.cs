using System;
using System.Collections.Generic;
using System.Data;
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
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            int status = Convert.ToInt32(headerDR["StatusID"]);
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
                            perfilPr.CtxMenu = context;
                            perfilPr.LLenarMenuContext(headerDR, status);
                            break;
                        case TypeDocumentHeader.PO:
                            perfilPo.CtxMenu = context;
                            perfilPo.LLenarMenuContext(headerDR, status);
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
            int status = Convert.ToInt32(headerDR["StatusID"]);
            List<RequisitionDetails> rd;
            List<OrderDetails> od;

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
                            if (Convert.ToInt32(headerDR["DetailsCount"]) == 0)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            rd = new RequisitionDetails().GetListByID(headerID);
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilPo.CurrentUser, rd));
                            break;
                        case TypeDocumentHeader.PO:
                            if (Convert.ToInt32(headerDR["HitosCount"]) == 0)
                            {
                                return "This Purchase Order does not contain a 'Hito'.";
                            }
                            if (headerDR["SupplierID"] == DBNull.Value)
                            {
                                return "The Purchase Order does not contain a 'Supplier'.";
                            }
                            if (Convert.ToInt32(headerDR["DetailsCount"]) == 0)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            if (headerDR["CurrencyID"] == DBNull.Value)
                            {
                                return "This Purchase Order does not contain a 'Currency'.";
                            }
                            if (Convert.ToDecimal(headerDR["Net"]) <= 0)
                            {
                                return "This Purchase Order does not contain a 'Net'.";
                            }
                            od = new OrderDetails().GetListByID(headerID);
                            var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, od);
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
                            if (Convert.ToInt32(headerDR["DetailsCount"]) == 0)
                            {
                                return "This Purchase Order does not contain Products or Services.";
                            }
                            rd = new RequisitionDetails().GetListByID(headerID);
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilPr.CurrentUser, rd));
                            break;
                        case TypeDocumentHeader.PO:
                            //! User PR abre las PO que se hicieron desde sus PR
                            if (status <= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            od = new OrderDetails().GetListByID(headerID);
                            foreach (var item in new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, od))
                            {
                                Process.Start(item);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.VAL:
                    //! User VAL solo ve PO 
                    od = new OrderDetails().GetListByID(headerID);
                    foreach (var item in new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, od))
                    {
                        Process.Start(item);
                    }
                    break;
                default:
                    break;
            }
            return "OK";
        }

        public async Task<string> SendItem(DataRow headerDR, FPrincipal fPrincipal)
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
            int status = Convert.ToInt32(headerDR["StatusID"]);

            switch (td)
            {
                case TypeDocumentHeader.PR:
                    if (Convert.ToInt32(headerDR["DetailsCount"]) == 0)
                    {
                        return "This Purchase Order does not contain Products or Services.";
                    }

                    f.Mensaje = mensaje;
                    f.ShowDialog();
                    if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }

                    fPrincipal.IsSending = true;
                    fPrincipal.LblMsg.Text = string.Empty;
                    fPrincipal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    fPrincipal.LblMsg.Image = Properties.Resources.loading;


                    var details = new RequisitionDetails().GetListByID(headerID);
                    path = new HtmlManipulate(configApp)
                        .ReemplazarDatos(headerDR, perfilPr.CurrentUser, details);
                    send = new SendEmailTo(configApp);
                    await send.SendEmail(path, "Purchase Manager: PR document ", perfilPr.CurrentUser);
                    break;
                case TypeDocumentHeader.PO:
                    //if (status == 1) { return "The 'status' of the Purchase Order is not allowed."; }

                    f.Mensaje = mensaje;
                    f.ShowDialog(fPrincipal);
                    if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }

                    fPrincipal.IsSending = true;
                    fPrincipal.LblMsg.Text = string.Empty;
                    fPrincipal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    fPrincipal.LblMsg.Image = Properties.Resources.loading;


                    var od = new OrderDetails().GetListByID(headerID);
                    var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, od);
                    var a = await new HtmlManipulate(configApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                    send = new SendEmailTo(configApp);
                    await send.SendEmail(a, $"Purchase Manager:  PO document", perfilPr.CurrentUser);
                    break;
                default:
                    break;
            }
            return send.MessageResult;
        }

        public string GridDobleClick(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            int status = Convert.ToInt32(headerDR["StatusID"]);
            RequisitionHeader pr;
            OrderHeader po;


            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    //! Update Status
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";

                        case TypeDocumentHeader.PO:

                            po = new OrderHeader().GetById(headerID);
                            //if (po.StatusID >= 3) { return "The 'status' of the Purchase Order is not allowed."; }
                            if (Convert.ToInt32(headerDR["HitosCount"]) == 0) { return "This Purchase Order does not contain a 'Hito'."; }
                            if (Convert.ToInt32(headerDR["DetailsCount"]) == 0) { return "This Purchase Order does not contain Products or Services."; }
                            if (po.StatusID == 1)
                            {
                                if (headerDR["Description"] == DBNull.Value)
                                {
                                    return "Please enter 'Description'.";
                                }
                                if (string.IsNullOrEmpty(headerDR["SupplierID"].ToString()))
                                {
                                    return "Please enter 'Supplier'.";
                                }
                                else if (string.IsNullOrEmpty(headerDR["CurrencyID"].ToString()))
                                {
                                    return "Please enter 'Currency'.";
                                }
                                else if (Convert.ToDecimal(headerDR["Net"]) == 0)
                                {
                                    return "Net cannot be 'zero'.";
                                }
                                po.StatusID = 2;
                                perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
                                break;
                            }
                            else if (po.StatusID == 2)
                            {
                                po.StatusID = 1;
                                perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
                                break;
                            }
                            else if (po.StatusID == 4)
                            {
                                po.StatusID = 5;
                                perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
                                break;
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case Perfiles.UPR:
                    //! Update Status
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            pr = new RequisitionHeader().GetById(headerID);
                            if (Convert.ToInt32(headerDR["DetailsCount"]) == 0) { return "This Purchase Requesition does not contain Products or Services."; }
                            if (pr.StatusID == 1)
                            {
                                if (headerDR["Description"] == DBNull.Value)
                                {
                                    return "Please enter 'Description'.";
                                }
                                if (headerDR["UserPO"] == DBNull.Value)
                                {
                                    return "Please enter 'UserPO'.";
                                }
                                pr.StatusID = 2;
                                perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, headerID);
                                break;
                            }
                            else if (pr.StatusID == 2)
                            {
                                pr.StatusID = 1;
                                perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, headerID);
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
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            break;
                        case TypeDocumentHeader.PO:
                            po = new OrderHeader().GetById(headerID);
                            if (po.StatusID == 2)
                            {
                                po.StatusID = 3;
                                perfilVal.UpdateItemHeader<OrderHeader>(td, po, headerID);
                            }
                            else if (po.StatusID == 3)
                            {
                                po.StatusID = 2;
                                perfilVal.UpdateItemHeader<OrderHeader>(td, po, headerID);
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

        public async Task<string> SeleccionarContextMenuStripAsync(DataRow headerDR, string action, FPrincipal fPrincipal)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            RequisitionHeader pr;
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            OrderHeader po;
            if (!Directory.Exists(configApp.FolderApp + headerID))
            {
                Directory.CreateDirectory(configApp.FolderApp + headerID);
            }
            switch (action)
            {
                case "CONVERTREQ":
                    //! Crear una PO desde una PR

                    //! Cambio el estado de la PR a 3
                    pr = new RequisitionHeader().GetById(headerID);
                    if (pr != null && pr.StatusID == 2)
                    {
                        pr.StatusID = 3;
                        perfilPo.UpdateItemHeader(td, pr, headerID);
                        //! Insert PO
                        po = new OrderHeader
                        {
                            Description = pr.Description,
                            Type = Convert.ToByte(headerDR["Type"]),
                            StatusID = 1,
                            Net = 0,
                            Exent = 0,
                            RequisitionHeaderID = pr.RequisitionHeaderID,
                            CompanyID = pr.CompanyID,
                            Discount = 0
                        };
                        //! Traspasar los detalles
                        var details = new RequisitionDetails().GetListByID(headerID);
                        foreach (var item in details)
                        {
                            var detail = new OrderDetails
                            {
                                Qty = item.Qty,
                                AccountID = item.AccountID,
                                DescriptionProduct = item.DescriptionProduct,
                                NameProduct = item.NameProduct,
                                MedidaID = item.MedidaID,
                                IsExent = false
                            };
                            po.OrderDetails.Add(detail);
                        }
                        //! Traspasar los adjuntos públicos
                        var att = new Attaches().GetListByPrID(headerID).Where(c => c.Modifier == 1);
                        foreach (var item in att)
                        {
                            var tt = new Attaches
                            {
                                Description = item.Description,
                                FileName = item.FileName,
                                Modifier = item.Modifier
                            };
                            po.Attaches.Add(tt);
                            //! Copiar los archivos a la nueva ubicación
                            File.Copy($"{configApp.FolderApp}{item.FileName}", $"{configApp.FolderApp}{@"\"}{headerID}{@"\"}{Path.GetFileName(item.FileName)}", true);

                        }
                        perfilPo.InsertPOHeader(po);
                    }
                    break;
                case "OPENREQ":
                    //! Traer el DataRow de la PR
                    var d = perfilPo.GetDataRow(TypeDocumentHeader.PR, Convert.ToInt32(headerDR["RequisitionHeaderID"]));
                    if (d != null)
                    {
                        VerItemHtml(d);
                    }
                    else
                    {
                        return "ERROR";
                    }
                    break;
                case "SEND":
                    //todo PROVISORIAMENTE ESTO CAMBIARÁ EL ESTADO A ENVIADO A SUPPLIER

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
                        if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }


                        fPrincipal.IsSending = true;
                        fPrincipal.LblMsg.Text = string.Empty;
                        fPrincipal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                        fPrincipal.LblMsg.Image = Properties.Resources.loading;



                        //!PDF de la PO
                        var od = new OrderDetails().GetListByID(headerID);
                        var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, od);
                        var a = await new HtmlManipulate(configApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                        //! Adjuntos
                        List<Attaches> att = new Attaches().GetListByPoID(headerID).Where(c => c.Modifier == 1).ToList();


                        //! Email To Supplier
                        var send = new SendEmailTo(configApp);
                        var asunto = $"Orden de Compra {po.Code} ({po.CompanyID})";
                        await send.SendEmailToSupplier(a, asunto, perfilPr.CurrentUser, supp, att);



                        po.StatusID = 4;
                        perfilVal.UpdateItemHeader<OrderHeader>(td, po, headerID);

                    }


                    break;
                default:
                    break;
            }
            return "OK";
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
                    break;
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
                    perfilPo.PintarGrid();
                    fDetails.ShowDialog(fPrincipal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fDetails.GetGrid();
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
                    perfilPo.PintarGrid();
                    fAttach.ShowDialog(fPrincipal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fAttach.GetGrid();
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
                    perfilPo.InsertPOHeader(po);
                    return true;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader
                    {
                        Type = type.TypeID,
                        CompanyID = company.CompanyID,
                        StatusID = 1
                    };
                    perfilPr.InsertPRHeader(pr);
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
            int status = Convert.ToInt32(headerDR["StatusID"]);
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
                                    perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
                                    break;
                                case "Type":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    po.Type = Convert.ToByte(newValue);
                                    perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
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
                                    perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
                                    break;
                                case "CurrencyID":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    po.CurrencyID = newValue.ToString();
                                    perfilPo.UpdateItemHeader<OrderHeader>(td, po, headerID);
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
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, headerID);
                                    break;
                                case "Type":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    pr.Type = Convert.ToByte(newValue);
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, headerID);
                                    break;
                                case "CurrencyID":
                                    return "Your profile does not allow you to complete this action.";
                                case "UserPO":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                                    pr.UserPO = newValue.ToString();
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, headerID);
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

        public string DeleteItem(DataRow headerDR, FPrincipal fPrincipal)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);

            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"You are going to delete document N° {headerID}.");
            mensaje.AppendLine();
            mensaje.AppendLine("Are You Sure?");
            var f = new FMensajes(fPrincipal);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (perfilPo.CurrentUser.UserID != headerDR["UserID"].ToString())
                    {
                        return "Your profile does not allow you to complete this action.";
                    }
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }

                    f.Mensaje = mensaje;
                    f.ShowDialog();
                    if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }

                    perfilPo.DeleteItemHeader(td, headerID);
                    break;
                case Perfiles.UPR:
                    if (perfilPr.CurrentUser.UserID != headerDR["UserID"].ToString())
                    {
                        return "Your profile does not allow you to complete this action.";
                    }
                    if (status >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }

                    f.Mensaje = mensaje;
                    f.ShowDialog(fPrincipal);
                    if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }

                    perfilPr.DeleteItemHeader(td, headerID);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        #endregion

        #region Details CRUD

        public string InsertDetail(object item, DataRow headerDR)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            int status = Convert.ToInt32(headerDR["StatusID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
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
                            if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            var od = (OrderDetails)item;
                            decimal total = new OrderHeader().GetById(headerID).Total;
                            if ((total + od.Total) < 99000000) // Máximo por Header
                            {
                                perfilPo.InsertDetail(item, headerID);
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case Perfiles.UPR:
                    if (status >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    perfilPr.InsertDetail(item, headerID);
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
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            perfilPo.DeleteDetail(td, headerID, Convert.ToInt32(detailDR["DetailID"]));
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPr.DeleteDetail(td, headerID, Convert.ToInt32(detailDR["DetailID"]));
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
            int status = Convert.ToInt32(headerDR["StatusID"]);
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
                            var od = new OrderDetails().GetByID(detailID);
                            if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            if (isExent)
                            {
                                od.IsExent = true;
                            }
                            else
                            {
                                od.IsExent = false;
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
                                    od.Price = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                                    perfilPo.UpdateDetail<OrderDetails>(td, od, headerID, detailID);
                                    break;
                                case "NameProduct":
                                    od.NameProduct = newValue.ToString();
                                    perfilPo.UpdateDetail<OrderDetails>(td, od, headerID, detailID);
                                    break;
                            }
                            break;
                        default:
                            break;
                    }


                    break;
                case Perfiles.UPR:
                    var rd = new RequisitionDetails().GetByID(detailID);
                    if (status >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    switch (campo)
                    {
                        case "NameProduct":
                            rd.NameProduct = UCase.ToTitleCase(newValue.ToString().ToLower());
                            perfilPr.UpdateDetail<RequisitionDetails>(td, rd, headerID, detailID);
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

            int status = Convert.ToInt32(headerDR["StatusID"]);
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
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    if (File.Exists($"{configApp.FolderApp}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPo.InsertAttach(item, headerID);
                    File.Copy(path, $"{configApp.FolderApp}{item.FileName}", true);
                    break;
                case Perfiles.UPR:
                    if (status >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    if (File.Exists($"{configApp.FolderApp}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPr.InsertAttach(item, headerID);
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
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var attachID = Convert.ToInt32(attachDR["attachID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    File.Delete($"{configApp.FolderApp}{attachDR["FileName"]}");
                    perfilPo.DeleteAttach(headerID, attachID);
                    break;
                case Perfiles.UPR:
                    //todo Saber si tiene acceso a la carpeta en el server: ds
                    // DirectorySecurity ds = Directory.GetAccessControl(configApp.FolderApp);


                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    File.Delete($"{configApp.FolderApp}{attachDR["FileName"]}");
                    perfilPr.DeleteAttach(headerID, attachID);
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
                return "Opening File...";
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
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var att = new Attaches().GetByID(attachID);
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
                            att.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            perfilPo.UpdateAttaches(att, headerID, attachID);
                            break;
                        case "Modifier":
                            att.Modifier = Convert.ToByte(newValue);
                            perfilPo.UpdateAttaches(att, headerID, attachID);
                            break;

                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (campo)
                    {
                        case "Description":
                            att.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            perfilPr.UpdateAttaches(att, headerID, attachID);
                            break;
                        case "Modifier":
                            att.Modifier = Convert.ToByte(newValue);
                            perfilPr.UpdateAttaches(att, headerID, attachID);
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

        public string DeleteSupplier(string headerID)
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
                        return "It has associated data, it cannot be deleted.";
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
