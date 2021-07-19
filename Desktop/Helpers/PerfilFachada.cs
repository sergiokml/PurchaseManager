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
        protected UserProfilerADM perfilAdm;
        protected UserProfileBAS perfilBas;
        private readonly Perfiles currentPerfil;
        private readonly TextInfo UCase = CultureInfo.InvariantCulture.TextInfo;
        private readonly ConfigApp configApp;

        //! Objeto para no enviar el FPrincipal en cada Función
        public FPrincipal Fprpal { get; set; }

        public PerfilFachada(UserProfileUPR upr, UserProfileUPO upo, UserProfileVAL val, UserProfilerADM adm, UserProfileBAS bas, Users user)
        {
            configApp = new ConfigApp().GetConfigApp();
            perfilPr = upr;
            perfilPo = upo;
            perfilVal = val;
            perfilAdm = adm;
            perfilBas = bas;
            Enum.TryParse(user.ProfileID, out Perfiles p);
            currentPerfil = p;
            upr.CurrentUser = user;
            upo.CurrentUser = user;
            val.CurrentUser = user;
            adm.CurrentUser = user;
            bas.CurrentUser = user;
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
                            perfilAdm.Grid = grid;
                            perfilAdm.CargarColumnasFPrincipal(Perfiles.UPO);
                            break;
                        case Perfiles.BAS:
                            perfilPo.Grid = grid;
                            perfilPo.CargarColumnasFPrincipal(Perfiles.UPO);
                            break;
                        case Perfiles.UPO:
                            perfilPo.Grid = grid;
                            perfilPo.CargarColumnasFPrincipal(Perfiles.UPO);
                            break;
                        case Perfiles.UPR:
                            perfilPr.Grid = grid;
                            perfilPr.CargarColumnasFPrincipal(Perfiles.UPR);
                            break;
                        case Perfiles.VAL:
                            perfilVal.Grid = grid;
                            perfilVal.CargarColumnasFPrincipal(Perfiles.VAL);
                            break;
                    }
                    break;
                case "FDetails":
                    Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            perfilAdm.CargarColumnasFDetail(Perfiles.ADM, td);
                            break;
                        case Perfiles.BAS:
                            perfilBas.CargarColumnasFDetail(Perfiles.BAS, td);
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
                    }
                    break;
                case "FAttach":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            perfilAdm.CargarColumnasFAttach(Perfiles.ADM);
                            break;
                        case Perfiles.BAS:
                            perfilBas.CargarColumnasFAttach(Perfiles.BAS);
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
                    }
                    break;
                case "FSupplier":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            perfilAdm.CargarColumnasFSupplier(Perfiles.ADM);
                            break;
                        case Perfiles.BAS:
                            perfilBas.CargarColumnasFSupplier(Perfiles.BAS);
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
                    }
                    break;
                case "FHitos":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            perfilAdm.CargarColumnasFHitos(Perfiles.ADM);
                            break;
                        case Perfiles.BAS:
                            perfilBas.CargarColumnasFHitos(Perfiles.BAS);
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
                    }
                    break;
                case "FNotes":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            perfilAdm.CargarColumnasFNotes(Perfiles.ADM);
                            break;
                        case Perfiles.BAS:
                            perfilBas.CargarColumnasFNotes(Perfiles.BAS);
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
                    }
                    break;
                case "FDelivery":
                    switch (currentPerfil)
                    {
                        case Perfiles.ADM:
                            perfilAdm.CargarColumnasFDelivery(Perfiles.ADM);
                            break;
                        case Perfiles.BAS:
                            perfilBas.CargarColumnasFDelivery(Perfiles.BAS);
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
                    }
                    break;
            }
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
            }
        }

        public void CargarDashBoard(iGrid grid, BunifuPieChart c1, BunifuPieChart c2, Panel panelDash)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    perfilAdm.Grid = grid;
                    perfilAdm.CargarDatos(perfilAdm.CurrentUser, c1, c2, panelDash);
                    break;
                case Perfiles.BAS:
                    perfilBas.Grid = grid;
                    perfilBas.CargarDatos(perfilBas.CurrentUser, c1, c2, panelDash);
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
            }
        }

        public Users CurrentUser()
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    return perfilAdm.CurrentUser;
                case Perfiles.BAS:
                    return perfilBas.CurrentUser;
                case Perfiles.UPO:
                    return perfilPo.CurrentUser;
                case Perfiles.UPR:
                    return perfilPr.CurrentUser;
                case Perfiles.VAL:
                    return perfilVal.CurrentUser;
            }
            return null;
        }

        public void VerItemHtml(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                Fprpal.Msg("This Purchase Requisition does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilPo.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.OrderHitos.Count == 0)
                            {
                                Fprpal.Msg("This Purchase Order does not contain a 'Hito'.", MsgProceso.Warning); return;
                            }
                            if (po.SupplierID == null)
                            {
                                Fprpal.Msg("The Purchase Order does not contain a 'Supplier'.", MsgProceso.Warning); return;
                            }
                            if (po.OrderDetails.Count <= 0)
                            {
                                Fprpal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            if (po.CurrencyID == null)
                            {
                                Fprpal.Msg("This Purchase Order does not contain a 'Currency'.", MsgProceso.Warning); return;
                            }
                            if (po.Net <= 0)
                            {
                                Fprpal.Msg("This Purchase Order does not contain a 'Net'.", MsgProceso.Warning); return;
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
                                Fprpal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            Process.Start(new HtmlManipulate(configApp)
                                .ReemplazarDatos(headerDR, perfilPr.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            //! User PR abre las PO que se hicieron desde sus PR
                            var po = new OrderHeader().GetById(headerID);
                            //TODO El estado evita que se abra PO en estado DRAFT, o sea sin SUPPLIER (Error en Html).
                            if (po.StatusID < 2) { Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return; }
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
                                Fprpal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
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
                    }
                    break;
            }
        }

        public async Task SendItem(DataRow headerDR)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"The {headerDR["TypeDocumentHeader"]} N° {headerID} will be sent to your own inbox.");
            mensaje.AppendLine();
            mensaje.AppendLine("Are You Sure?");
            var f = new FMensajes(Fprpal);

            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            string path;
            SendEmailTo send = null;

            switch (td)
            {
                case TypeDocumentHeader.PR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.RequisitionDetails.Count == 0)
                    {
                        Fprpal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
                    }
                    f.Mensaje = mensaje;
                    f.ShowDialog();
                    if (f.Resultado == DialogResult.Cancel)
                    {
                        Fprpal.Msg("", MsgProceso.Empty);
                        Fprpal.IsSending = false; return;
                    }
                    Fprpal.IsSending = true;
                    Fprpal.LblMsg.Text = string.Empty;
                    Fprpal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    Fprpal.LblMsg.Image = Properties.Resources.loading;

                    path = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPr.CurrentUser, pr);
                    send = new SendEmailTo(configApp);
                    await send.SendEmail(path, "Purchase Manager: PR document ", perfilPr.CurrentUser);
                    Fprpal.Msg(send.MessageResult, MsgProceso.Send);
                    Fprpal.IsSending = false;
                    break;
                case TypeDocumentHeader.PO:
                    var po = new OrderHeader().GetById(headerID);
                    //TODO El estado evita que se abra PO en estado DRAFT, o sea sin SUPPLIER (Error en Html).
                    //TODO NO sirve ni para PO aunque esten en estado borrador y bien emitidas.
                    if (po.StatusID < 2)
                    {
                        Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return;
                    }
                    f.Mensaje = mensaje;
                    f.ShowDialog(Fprpal);
                    // if (f.Resultado == DialogResult.Cancel) { return "Operation Cancelled."; }
                    if (f.Resultado == DialogResult.Cancel)
                    {
                        Fprpal.Msg("", MsgProceso.Empty);
                        Fprpal.IsSending = false; return;
                    }
                    Fprpal.IsSending = true;
                    Fprpal.LblMsg.Text = string.Empty;
                    Fprpal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    Fprpal.LblMsg.Image = Properties.Resources.loading;

                    var listaPath = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                    var a = await new HtmlManipulate(configApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                    send = new SendEmailTo(configApp);
                    await send.SendEmail(a, $"Purchase Manager:  PO document", perfilPr.CurrentUser);
                    Fprpal.Msg(send.MessageResult, MsgProceso.Send);
                    Fprpal.IsSending = false;

                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerDR"></param>
        /// <param name="fPrincipal"></param>
        public void GridDobleClick(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            RequisitionHeader pr;
            OrderHeader po;
            int res = 0;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    //! Update Status 1 o 2
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            po = new OrderHeader().GetById(headerID);
                            if (po.StatusID == 1)
                            {
                                if (po.OrderDetails.Count == 0)
                                {
                                    Fprpal
                                        .Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
                                }
                                if (po.Net <= 0)
                                {
                                    Fprpal
                                        .Msg("Net cannot be 'zero'.", MsgProceso.Warning); return;
                                }
                                if (Convert.ToInt32(headerDR["HitosCount"]) == 0)
                                {
                                    Fprpal
                                        .Msg("This Purchase Order does not contain a 'Hito'.", MsgProceso.Warning); return;
                                }
                                if (po.Description == null)
                                {
                                    Fprpal
                                        .Msg("Please enter 'Description'.", MsgProceso.Warning); return;
                                }
                                if (po.SupplierID == null)
                                {
                                    Fprpal
                                        .Msg("Please enter 'Supplier'.", MsgProceso.Warning); return;
                                }
                                if (po.CurrencyID == null)
                                {
                                    Fprpal
                                        .Msg("Please enter 'Currency'.", MsgProceso.Warning); return;
                                }
                                po.StatusID = 2;
                                res = perfilPo.UpdateItemHeader<OrderHeader>(po);
                            }
                            else if (po.StatusID == 2)
                            {
                                po.StatusID = 1;
                                res = perfilPo.UpdateItemHeader<OrderHeader>(po);
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
                                Fprpal
                                    .Msg("This Purchase Requesition does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            if (pr.StatusID == 1)
                            {
                                if (headerDR["Description"] == DBNull.Value)
                                {
                                    Fprpal
                                        .Msg("Please enter 'Description'.", MsgProceso.Warning); return;
                                }
                                if (headerDR["UserPO"] == DBNull.Value)
                                {
                                    Fprpal
                                        .Msg("Please enter 'UserPO'.", MsgProceso.Warning); return;
                                }
                                pr.StatusID = 2;
                                res = perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
                            }
                            else if (pr.StatusID == 2)
                            {
                                pr.StatusID = 1;
                                res = perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
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
                        case TypeDocumentHeader.PO:
                            Fprpal
                                .Msg("Your profile does not allow you to complete this action..", MsgProceso.Warning); return;
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
                                Fprpal.Msg("Your profile does not allow you to complete this action..", MsgProceso.Warning);
                            }

                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
        }

        public async Task SeleccionarContextMenuStripAsync(DataRow headerDR, string action)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            RequisitionHeader pr;
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            OrderHeader po;

            switch (action)
            {
                case "CONVERTREQ":
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
                        //! Copiar los adjuntos a la nuev acarpeta de la PO
                        if (!Directory.Exists($"{configApp.FolderApp}{po.OrderHeaderID}"))
                        {
                            Directory.CreateDirectory($"{configApp.FolderApp}{po.OrderHeaderID}");
                        }
                        pr.Attaches.ToList().RemoveAll(c => c.Modifier == 0);
                        foreach (var item in pr.Attaches.ToList())
                        {
                            var sub = item.FileName;
                            if (!File.Exists($"{configApp.FolderApp}{po.OrderHeaderID}{sub}"))
                            {
                                CopyFile($"{configApp.FolderApp}{pr.RequisitionHeaderID}{item.FileName}", $"{configApp.FolderApp}{po.OrderHeaderID}{sub}");
                                //perfilPo.InsertAttach<OrderHeader>(item, po);

                            }
                        }

                        //TODO ESTO DEBE IR AL FINAL DE ESTE MÉTODO
                        Fprpal.LlenarGrid();
                        Fprpal.ClearControles();
                        Fprpal.SetControles();
                        Fprpal.CargarDashboard();
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
                    if (!Fprpal.IsSending)
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
                            var f = new FMensajes(Fprpal)
                            {
                                Mensaje = mensaje
                            };
                            f.ShowDialog(Fprpal);
                            if (f.Resultado == DialogResult.OK)
                            {
                                Fprpal.IsSending = true;
                                Fprpal.LblMsg.Text = string.Empty;
                                Fprpal.LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                                Fprpal.LblMsg.Image = Properties.Resources.loading;

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

                                Fprpal.Msg(send.MessageResult, MsgProceso.Send);
                                Fprpal.IsSending = false;
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fDetails"></param>
        public void OPenDetailForm(FDetails fDetails)
        {
            fDetails.ShowInTaskbar = false;
            fDetails.StartPosition = FormStartPosition.CenterParent;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    perfilPo.Grid = fDetails.GetGrid();
                    fDetails.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                    fDetails.ShowDialog(Fprpal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fDetails.GetGrid();
                    fDetails.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                    fDetails.ShowDialog(Fprpal);
                    break;
                case Perfiles.VAL:
                    perfilVal.Grid = fDetails.GetGrid();
                    fDetails.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    perfilPo.Grid = fAttach.GetGrid();
                    fAttach.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                    fAttach.ShowDialog(Fprpal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fAttach.GetGrid();
                    fAttach.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                    fAttach.ShowDialog(Fprpal);
                    break;
                case Perfiles.VAL:
                    perfilVal.Grid = fAttach.GetGrid();
                    fAttach.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
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
            Enum.TryParse(fSupplier.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            perfilPo.Grid = fSupplier.GetGrid();
                            fSupplier.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                            fSupplier.ShowDialog(Fprpal);
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.VAL:
                    perfilVal.Grid = fSupplier.GetGrid();
                    fSupplier.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
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
            Enum.TryParse(fHitos.Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            perfilPo.Grid = fHitos.GetGrid();
                            fHitos.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                            fHitos.ShowDialog(Fprpal);
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.VAL:
                    perfilVal.Grid = fHitos.GetGrid();
                    fHitos.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
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
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    perfilPo.Grid = fNotes.GetGrid();
                    fNotes.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                    fNotes.ShowDialog(Fprpal);
                    break;
                case Perfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.VAL:
                    perfilVal.Grid = fNotes.GetGrid();
                    fNotes.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
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
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            perfilPo.Grid = fDelivery.GetGrid();
                            fDelivery.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                            fDelivery.ShowDialog(Fprpal);
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.VAL:
                    perfilVal.Grid = fDelivery.GetGrid();
                    fDelivery.GuardarElPrevioCurrent = Fprpal.GetGrid().CurRow;
                    fDelivery.ShowDialog(Fprpal);
                    break;
            }
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="type"></param>
        public void InsertItem(Companies company, TypeDocument type)
        {
            int res = 0;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
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
                    res = perfilPo.InsertItemHeader(po);
                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader
                    {
                        Type = type.TypeID,
                        CompanyID = company.CompanyID,
                        StatusID = 1
                    };
                    res = perfilPr.InsertItemHeader(pr);
                    break;
                case Perfiles.VAL:
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
                                    res = perfilPo.UpdateItemHeader<OrderHeader>(po);
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
                                    res = perfilPo.UpdateItemHeader<OrderHeader>(po);
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
                                    res = perfilPo.UpdateItemHeader<OrderHeader>(po);
                                    break;
                                case "CurrencyID":
                                    po = new OrderHeader().GetById(headerID);
                                    if (po.StatusID >= 2)
                                    {
                                        Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return;
                                    }
                                    po.CurrencyID = newValue.ToString();
                                    res = perfilPo.UpdateItemHeader<OrderHeader>(po);
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
                case Perfiles.UPR:
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
                                    res = perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
                                    break;
                                case "Type":
                                    pr = new RequisitionHeader().GetById(headerID);
                                    if (pr.StatusID >= 2)
                                    {
                                        Fprpal
                                            .Msg("The 'status' of the Purchase Requisition is not allowed.", MsgProceso.Warning); return;
                                    }
                                    pr.Type = Convert.ToByte(newValue);
                                    res = perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
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
                                    res = perfilPr.UpdateItemHeader<RequisitionHeader>(pr);
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
                case Perfiles.VAL:
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
                            Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (perfilPo.CurrentUser.UserID != headerDR["UserID"].ToString()) //! User ID viene de la vista.
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
                            res = perfilPo.DeleteItemHeader<OrderHeader>(po);
                            if (res > 0) //! Return Indeterminado
                            {
                                //! Eliminar Files in Folder
                                try
                                {
                                    DirectoryInfo dir = new DirectoryInfo($"{configApp.FolderApp}{headerID}");
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
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            if (perfilPr.CurrentUser.UserID != headerDR["UserID"].ToString())
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
                            res = perfilPr.DeleteItemHeader<RequisitionHeader>(pr);
                            if (res > 0) // Return Indeterminado
                            {
                                //! Eliminar Files in Folder
                                try
                                {
                                    DirectoryInfo dir = new DirectoryInfo($"{configApp.FolderApp}{headerID}");
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
                case Perfiles.VAL:
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
        }

        #endregion

        #region Details CRUD

        public string InsertDetail<T>(T item, DataRow headerDR, FDetails fDetails)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
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
                            var po = new OrderHeader().GetById(headerID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            if (po.CurrencyID == null)
                            {
                                return "Please select 'Currency' for this Purchase Order.";
                            }
                            var od = item as OrderDetails;
                            var limite = new Currencies().GetList().Single(c => c.CurrencyID == po.CurrencyID).MaxInput;
                            if ((po.Total + (od.Qty * od.Price)) > limite) { return "You exceed the established limit."; }
                            perfilPo.InsertDetail<OrderDetails>(od, po);
                            //! Set nuevo Current en el formulario Details
                            fDetails.Current = perfilPo.GetDataRow(TypeDocumentHeader.PO, po.OrderHeaderID); ;
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
                            //! Set nuevo Current en el formulario Details
                            fDetails.Current = perfilPr.GetDataRow(TypeDocumentHeader.PR, pr.RequisitionHeaderID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                    }
                    break;
                case Perfiles.VAL:
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
                    }
                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    perfilPr.DeleteDetail(pr, detailID);
                    break;
                case Perfiles.VAL:
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
                    //! Copiar el archivo en la carpeta
                    CopyFile(path, $"{configApp.FolderApp}{headerID}{item.FileName}");
                    break;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    if (File.Exists($"{configApp.FolderApp}{headerID}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPr.InsertAttach<RequisitionHeader>(item, pr);
                    //! Copiar el archivo en la carpeta
                    CopyFile(path, $"{configApp.FolderApp}{headerID}{item.FileName}");
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        private void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void CopyFile(string source, string target)
        {
            try
            {
                File.Copy(source, target, true); //! True: sobre escribir el file
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteAttach(DataRow attachDR, DataRow headerDR)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var attachID = Convert.ToInt32(attachDR["attachID"]);
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
                            var po = new OrderHeader().GetById(headerID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            //! Borrar el archivo de la carpeta
                            DeleteFile($"{configApp.FolderApp}{headerID}{attachDR["FileName"]}");
                            perfilPo.DeleteAttach(po, attachID);
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            //todo Saber si tiene acceso a la carpeta en el server: ds
                            // DirectorySecurity ds = Directory.GetAccessControl(configApp.FolderApp);
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            //! Borrar el archivo de la carpeta
                            DeleteFile($"{configApp.FolderApp}{headerID}{attachDR["FileName"]}");
                            perfilPr.DeleteAttach<RequisitionHeader>(pr, attachID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                    }
                    break;
                case Perfiles.VAL:
                    break;
            }
            return "OK";
        }

        public string OpenAttach(DataRow attachDR, DataRow headerDR)
        {
            try
            {
                var headerID = Convert.ToInt32(headerDR["headerID"]);
                Process.Start($"{configApp.FolderApp}{headerID}{attachDR["FileName"]}");
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
                    }
                    break;
                case Perfiles.VAL:
                    break;
            }
            return "OK";

        }
        #endregion

        #region Supplier CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fPrincipal"></param>
        public void InsertSupplier(Suppliers item, FSupplier fSupplier)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.UPO:
                    if (perfilPo.InsertSupplier(item) == 0)
                    {
                        perfilPo.UpdateSupplier(item);
                        Fprpal.Msg("The record being inserted already exists in the table.", MsgProceso.Warning); return;
                    }
                    break;
                case Perfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case Perfiles.VAL:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
            }
            fSupplier.LlenarGrid();
            fSupplier.SetControles();
            fSupplier.ClearControles();
            Fprpal.GetGrid().CurRow = fSupplier.GuardarElPrevioCurrent;
        }

        public void DeleteSupplier(string headerID)
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
                        Fprpal.Msg("It has associated data, it cannot be deleted.", MsgProceso.Empty);
                    }
                    break;
                case Perfiles.UPR:

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
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
