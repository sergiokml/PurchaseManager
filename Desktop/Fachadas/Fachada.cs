using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PurchaseData.DataModel;
using PurchaseData.Helpers;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Helpers;
using PurchaseDesktop.Perfiles;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class Fachada : FuncGrid
    {
        public FachadaOpenForm FachadaOpenForm { get; set; }
        public FachadaViewForm FachadaViewForm { get; set; }
        public FachadaHeader FachadaHeader { get; set; }
        public FachadaGraficos FachadaControls { get; set; }
        public FachadaDetails FachadaDetails { get; set; }
        public FachadaAttach FachadaAttach { get; set; }
        public FachadaHitos FachadaHitos { get; set; }
        public FachadaNotes FachadaNotes { get; set; }
        public FachadaDeliverys FachadaDeliverys { get; set; }
        public FachadaGrid FachadaGrid { get; set; }
        public FachadaBanner FachadaBanner { get; set; }
        public FachadaActions FachadaActions { get; set; }


        protected UserProfileUPR perfilPr;
        protected UserProfileUPO perfilPo;
        protected UserProfileVAL perfilVal;
        protected UserProfilerADM perfilAdm;
        protected UserProfileBAS perfilBas;

        public ConfigApp ConfigApp { get; set; }
        public EPerfiles CurrentPerfil { get; set; }


        //! Objeto para no enviar el FPrincipal en cada Función, se inicia en el ctor de FPrincipal
        public FPrincipal Fprpal { get; set; }

        public Fachada(Users user, ConfigApp configApp)
        {
            ConfigApp = configApp;
            Enum.TryParse(user.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    perfilAdm = new UserProfilerADM(user); // Usar tambien lo mismo!
                    FachadaOpenForm = new FachadaOpenForm(perfilAdm);
                    FachadaViewForm = new FachadaViewForm(perfilAdm);
                    FachadaHeader = new FachadaHeader(perfilAdm, configApp);
                    FachadaControls = new FachadaGraficos(perfilAdm);
                    FachadaDetails = new FachadaDetails(perfilAdm);
                    FachadaAttach = new FachadaAttach(perfilAdm, configApp);
                    FachadaHitos = new FachadaHitos(perfilAdm);
                    FachadaNotes = new FachadaNotes(perfilAdm);
                    FachadaDeliverys = new FachadaDeliverys(perfilAdm);
                    FachadaGrid = new FachadaGrid(perfilAdm);
                    FachadaBanner = new FachadaBanner(perfilAdm, configApp);
                    FachadaActions = new FachadaActions(perfilAdm, configApp);
                    break;
                case EPerfiles.BAS:
                    perfilBas = new UserProfileBAS(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilBas);
                    FachadaViewForm = new FachadaViewForm(perfilBas);
                    FachadaHeader = new FachadaHeader(perfilBas, configApp);
                    FachadaControls = new FachadaGraficos(perfilBas);
                    FachadaDetails = new FachadaDetails(perfilBas);
                    FachadaAttach = new FachadaAttach(perfilBas, configApp);
                    FachadaHitos = new FachadaHitos(perfilBas);
                    FachadaNotes = new FachadaNotes(perfilBas);
                    FachadaDeliverys = new FachadaDeliverys(perfilBas);
                    FachadaGrid = new FachadaGrid(perfilBas);
                    FachadaBanner = new FachadaBanner(perfilBas, configApp);
                    FachadaActions = new FachadaActions(perfilBas, configApp);
                    break;
                case EPerfiles.UPO:
                    perfilPo = new UserProfileUPO(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilPo);
                    FachadaViewForm = new FachadaViewForm(perfilPo);
                    FachadaHeader = new FachadaHeader(perfilPo, configApp);
                    FachadaControls = new FachadaGraficos(perfilPo);
                    FachadaDetails = new FachadaDetails(perfilPo);
                    FachadaAttach = new FachadaAttach(perfilPo, configApp);
                    FachadaHitos = new FachadaHitos(perfilPo);
                    FachadaNotes = new FachadaNotes(perfilPo);
                    FachadaDeliverys = new FachadaDeliverys(perfilPo);
                    FachadaGrid = new FachadaGrid(perfilPo);
                    FachadaBanner = new FachadaBanner(perfilPo, configApp);
                    FachadaActions = new FachadaActions(perfilPo, configApp);
                    break;
                case EPerfiles.UPR:
                    perfilPr = new UserProfileUPR(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilPr);
                    FachadaViewForm = new FachadaViewForm(perfilPr);
                    FachadaHeader = new FachadaHeader(perfilPr, configApp);
                    FachadaControls = new FachadaGraficos(perfilPr);
                    FachadaDetails = new FachadaDetails(perfilPr);
                    FachadaAttach = new FachadaAttach(perfilPr, configApp);
                    FachadaHitos = new FachadaHitos(perfilPr);
                    FachadaNotes = new FachadaNotes(perfilPr);
                    FachadaDeliverys = new FachadaDeliverys(perfilPr);
                    FachadaGrid = new FachadaGrid(perfilPr);
                    FachadaBanner = new FachadaBanner(perfilPr, configApp);
                    FachadaActions = new FachadaActions(perfilPr, configApp);
                    break;
                case EPerfiles.VAL:
                    perfilVal = new UserProfileVAL(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilVal);
                    FachadaViewForm = new FachadaViewForm(perfilVal);
                    FachadaHeader = new FachadaHeader(perfilVal, configApp);
                    FachadaControls = new FachadaGraficos(perfilVal);
                    FachadaDetails = new FachadaDetails(perfilVal);
                    FachadaAttach = new FachadaAttach(perfilVal, configApp);
                    FachadaHitos = new FachadaHitos(perfilVal);
                    FachadaNotes = new FachadaNotes(perfilVal);
                    FachadaDeliverys = new FachadaDeliverys(perfilVal);
                    FachadaGrid = new FachadaGrid(perfilVal);
                    FachadaBanner = new FachadaBanner(perfilVal, configApp);
                    FachadaActions = new FachadaActions(perfilVal, configApp);
                    break;
                default:
                    break;
            }

        }

        #region Métodos del Grid Principal de cada Formulario


        //TODO ACA HE LLEGADO!



        public void CargarContextMenuStrip(ContextMenuStrip context, DataRow headerDR)
        {
            CtxMenu = context;
            LLenarMenuContext(CurrentPerfil, headerDR);
        }

        #endregion



        #region Cargar Controles y Acciones


        public Users CurrentUser()
        {
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    return perfilAdm.CurrentUser;
                case EPerfiles.BAS:
                    return perfilBas.CurrentUser;
                case EPerfiles.UPO:
                    return perfilPo.CurrentUser;
                case EPerfiles.UPR:
                    return perfilPr.CurrentUser;
                case EPerfiles.VAL:
                    return perfilVal.CurrentUser;
            }
            return null;
        }

        public void VerItemHtml(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
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
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                Fprpal.Msg("This Purchase Requisition does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            Process.Start(new HtmlManipulate(ConfigApp)
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

                            var listaPath = new HtmlManipulate(ConfigApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                            foreach (var item in listaPath)
                            {
                                Process.Start(item);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                Fprpal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            Process.Start(new HtmlManipulate(ConfigApp)
                                .ReemplazarDatos(headerDR, perfilPr.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            //! User PR abre las PO que se hicieron desde sus PR
                            var po = new OrderHeader().GetById(headerID);
                            //TODO El estado evita que se abra PO en estado DRAFT, o sea sin SUPPLIER (Error en Html).
                            if (po.StatusID < 2) { Fprpal.Msg("The 'status' of the Purchase Order is not allowed.", MsgProceso.Warning); return; }
                            foreach (var item in new HtmlManipulate(ConfigApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po))
                            {
                                Process.Start(item);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case EPerfiles.VAL:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.RequisitionDetails.Count < 1)
                            {
                                Fprpal.Msg("This Purchase Order does not contain Products or Services.", MsgProceso.Warning); return;
                            }
                            Process.Start(new HtmlManipulate(ConfigApp)
                                .ReemplazarDatos(headerDR, perfilVal.CurrentUser, pr));
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            foreach (var item in new HtmlManipulate(ConfigApp)
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
            SendEmailTo send;
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

                    path = new HtmlManipulate(ConfigApp).ReemplazarDatos(headerDR, perfilPr.CurrentUser, pr);
                    send = new SendEmailTo(ConfigApp);
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

                    var listaPath = new HtmlManipulate(ConfigApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                    var a = await new HtmlManipulate(ConfigApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                    send = new SendEmailTo(ConfigApp);
                    await send.SendEmail(a, $"Purchase Manager:  PO document", perfilPr.CurrentUser);
                    Fprpal.Msg(send.MessageResult, MsgProceso.Send);
                    Fprpal.IsSending = false;

                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerDR"></param>
        public void GridDobleClick(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            RequisitionHeader pr;
            OrderHeader po;
            int res = 0;
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
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
                                if (po.OrderHitos.Count == 0)
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
                            else
                            {
                                return;
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
                case EPerfiles.UPR:
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
                case EPerfiles.VAL:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            break;
                        case TypeDocumentHeader.PO:
                            ////TODO ESTO SE HARÁ CON MENPU CONTEXTUAL => ACEPTAR O RECHAZAR
                            //po = new OrderHeader().GetById(headerID);
                            //if (po.StatusID == 2)
                            //{
                            //    po.StatusID = 3;
                            //    perfilVal.UpdateItemHeader<OrderHeader>(po);
                            //}
                            //else if (po.StatusID >= 3)
                            //{
                            //    Fprpal.Msg("Your profile does not allow you to complete this action..", MsgProceso.Warning);
                            //}
                            break;
                    }
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
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
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.BAS:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.UPO:
                    if (perfilPo.InsertSupplier(item) == 0)
                    {
                        perfilPo.UpdateSupplier(item);
                        Fprpal.Msg("The record being inserted already exists in the table.", MsgProceso.Warning); return;
                    }
                    break;
                case EPerfiles.UPR:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
                case EPerfiles.VAL:
                    Fprpal.Msg("Your profile does not allow you to complete this action.", MsgProceso.Warning); return;
            }
            fSupplier.LlenarGrid();
            fSupplier.SetControles();
            fSupplier.ClearControles();
            Fprpal.GetGrid().CurRow = fSupplier.GuardarElPrevioCurrent;
        }

        public void DeleteSupplier(string headerID)
        {
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    var res = perfilPo.DeleteSupplier(headerID);
                    if (res == 547)
                    {
                        Fprpal.Msg("It has associated data, it cannot be deleted.", MsgProceso.Empty);
                    }
                    break;
                case EPerfiles.UPR:

                    break;
                case EPerfiles.VAL:
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
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    perfilPo.UpdateSupplier(item);
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
