using System;
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

using PurchaseData.DataModel;
using PurchaseData.Helpers;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Helpers;
using PurchaseDesktop.Perfiles;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Fachadas
{
    public class PerfilFachada : HFunctions
    {
        public FachadaOpenForm FachadaOpenForm { get; set; }
        public FachadaViewForm FachadaViewForm { get; set; }
        public FachadaHeader FachadaHeader { get; set; }
        public FachadaControls FachadaControls { get; set; }


        protected UserProfileUPR perfilPr;
        protected UserProfileUPO perfilPo;
        protected UserProfileVAL perfilVal;
        protected UserProfilerADM perfilAdm;
        protected UserProfileBAS perfilBas;

        public ConfigApp ConfigApp { get; set; }
        public EPerfiles CurrentPerfil { get; set; }
        // public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;


        //! Objeto para no enviar el FPrincipal en cada Función, se inicia en el ctor de FPrincipal
        public FPrincipal Fprpal { get; set; }

        public PerfilFachada(Users user, ConfigApp configApp)
        {
            ConfigApp = configApp;
            Enum.TryParse(user.ProfileID, out EPerfiles p);
            CurrentPerfil = p;

            // TODO ACÁ DEBO HACER UNA "FACTORÍA" QUE CREA CLASES:            
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    perfilAdm = new UserProfilerADM(user); // Usar tambien lo mismo!!!!!!
                    FachadaOpenForm = new FachadaOpenForm(perfilAdm);
                    FachadaViewForm = new FachadaViewForm(perfilAdm);
                    FachadaHeader = new FachadaHeader(perfilAdm, configApp);
                    FachadaControls = new FachadaControls(perfilAdm);
                    break;
                case EPerfiles.BAS:
                    perfilBas = new UserProfileBAS(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilBas);
                    FachadaViewForm = new FachadaViewForm(perfilBas);
                    FachadaHeader = new FachadaHeader(perfilBas, configApp);
                    FachadaControls = new FachadaControls(perfilBas);
                    break;
                case EPerfiles.UPO:
                    perfilPo = new UserProfileUPO(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilPo);
                    FachadaViewForm = new FachadaViewForm(perfilPo);
                    FachadaHeader = new FachadaHeader(perfilPo, configApp);
                    FachadaControls = new FachadaControls(perfilPo);
                    break;
                case EPerfiles.UPR:
                    perfilPr = new UserProfileUPR(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilPr);
                    FachadaViewForm = new FachadaViewForm(perfilPr);
                    FachadaHeader = new FachadaHeader(perfilPr, configApp);
                    FachadaControls = new FachadaControls(perfilPr);
                    break;
                case EPerfiles.VAL:
                    perfilVal = new UserProfileVAL(user);
                    FachadaOpenForm = new FachadaOpenForm(perfilVal);
                    FachadaViewForm = new FachadaViewForm(perfilVal);
                    FachadaHeader = new FachadaHeader(perfilVal, configApp);
                    FachadaControls = new FachadaControls(perfilVal);
                    break;
                default:
                    break;
            }

        }

        #region Métodos del Grid Principal de cada Formulario

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

        public async Task SeleccionarContextMenuStripAsync(DataRow headerDR, string action)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            RequisitionHeader pr;
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            OrderHeader po;
            StringBuilder mensaje;
            var f = new FMensajes(Fprpal);
            string t;
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
                        if (!Directory.Exists($"{ConfigApp.FolderApp}{po.OrderHeaderID}"))
                        {
                            Directory.CreateDirectory($"{ConfigApp.FolderApp}{po.OrderHeaderID}");
                        }
                        pr.Attaches.ToList().RemoveAll(c => c.Modifier == 0);
                        foreach (var item in pr.Attaches.ToList())
                        {
                            var sub = item.FileName;
                            if (!File.Exists($"{ConfigApp.FolderApp}{po.OrderHeaderID}{sub}"))
                            {
                                CopyFile($"{ConfigApp.FolderApp}{pr.RequisitionHeaderID}{item.FileName}", $"{ConfigApp.FolderApp}{po.OrderHeaderID}{sub}");
                            }
                        }
                    }
                    break;
                case "OPENREQ":
                    //! Estoy en posicion de PO, quiero traer el DataRow de la PR... (botón derecho)
                    switch (CurrentPerfil)
                    {
                        case EPerfiles.ADM:
                            break;
                        case EPerfiles.BAS:
                            break;
                        case EPerfiles.UPO:
                            if (headerDR["RequisitionHeaderID"] != DBNull.Value)
                            {
                                DataRow d = perfilPo
                                    .GetDataRow(TypeDocumentHeader.PR, Convert.ToInt32(headerDR["RequisitionHeaderID"]));
                                VerItemHtml(d);
                            }
                            break;
                        case EPerfiles.UPR:
                            break;
                        case EPerfiles.VAL:
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
                    //! Enviar al Proveedor luego de que la PO es validada por el validador.
                    Fprpal.IsSending = false;
                    if (!Fprpal.IsSending)
                    {
                        po = new OrderHeader().GetById(headerID);
                        if (po.StatusID == 3)
                        {
                            mensaje = new StringBuilder();
                            var supp = new Suppliers().GetByID(headerDR["SupplierID"].ToString());
                            mensaje.AppendLine($"Email To: {supp.Name} ({supp.Email})");
                            mensaje.AppendLine($"Purchase Order Code N° {po.Code} (Attach: {po.Attaches.Count})");
                            mensaje.AppendLine();
                            mensaje.AppendLine("Are You Sure?");
                            f = new FMensajes(Fprpal)
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
                                var listaPath = new HtmlManipulate(ConfigApp).ReemplazarDatos(headerDR, perfilPo.CurrentUser, po);
                                var a = await new HtmlManipulate(ConfigApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                                //! Adjuntos
                                //TODO  QUITAR ESTO Y USAR LA REFERENCIA?
                                po.Attaches.ToList().RemoveAll(c => c.Modifier == 0);
                                //! Email To Supplier
                                var send = new SendEmailTo(ConfigApp);
                                var asunto = $"Orden de Compra {po.Code} ({po.CompanyID})";
                                await send.SendEmailToSupplier(a, asunto, perfilPr.CurrentUser, supp, po.Attaches.ToList(), po.OrderHeaderID);
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
                    t = po.Total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    mensaje = new StringBuilder();
                    mensaje.AppendLine("Are you sure to 'validate' this Purchase Order to be sent to the Supplier?");
                    mensaje.AppendLine($"N° {po.OrderHeaderID} / Total ${t} ({po.CurrencyID})");
                    mensaje.AppendLine();
                    mensaje.AppendLine("Are You Sure?");
                    f = new FMensajes(Fprpal)
                    {
                        Mensaje = mensaje
                    };
                    f.ShowDialog(Fprpal);
                    if (f.Resultado == DialogResult.OK)
                    {
                        if (po.StatusID == 2) // 2  Active
                        {
                            po.StatusID = 3;
                            perfilVal.UpdateItemHeader<OrderHeader>(po);
                        }
                    }
                    break;
                case "REJECTED":
                    po = new OrderHeader().GetById(headerID);
                    t = po.Total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    mensaje = new StringBuilder();
                    mensaje.AppendLine("Are you sure to 'reject' this Purchase Order?");
                    mensaje.AppendLine($"N° {po.OrderHeaderID} / Total ${t} ({po.CurrencyID})");
                    mensaje.AppendLine();
                    mensaje.AppendLine("Are You Sure?");
                    f = new FMensajes(Fprpal)
                    {
                        Mensaje = mensaje
                    };
                    f.ShowDialog(Fprpal);
                    if (f.Resultado == DialogResult.OK)
                    {
                        if (po.StatusID == 2) // 2  Active
                        {
                            po.StatusID = 7;
                            perfilVal.UpdateItemHeader<OrderHeader>(po);
                        }
                    }
                    break;
                case "ACCEPTED":
                    po = new OrderHeader().GetById(headerID);
                    t = po.Total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    mensaje = new StringBuilder();
                    mensaje.AppendLine("Has this Purchase Order been accepted by the supplier?");
                    mensaje.AppendLine($"N° {po.OrderHeaderID} / Total ${t} ({po.CurrencyID})");
                    mensaje.AppendLine();
                    mensaje.AppendLine("Are You Sure?");
                    f = new FMensajes(Fprpal)
                    {
                        Mensaje = mensaje
                    };
                    f.ShowDialog(Fprpal);
                    if (f.Resultado == DialogResult.OK)
                    {
                        if (po.StatusID == 4) // 4 Enviada
                        {
                            po.StatusID = 5;
                            perfilVal.UpdateItemHeader<OrderHeader>(po);
                        }
                    }
                    break;
                case "COMPLETE":
                    po = new OrderHeader().GetById(headerID);
                    t = po.Total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    mensaje = new StringBuilder();
                    mensaje.AppendLine("You want to mark this Purchase Order as completed?");
                    mensaje.AppendLine($"N° {po.OrderHeaderID} / Total ${t} ({po.CurrencyID})");
                    mensaje.AppendLine();
                    mensaje.AppendLine("Are You Sure?");
                    f = new FMensajes(Fprpal)
                    {
                        Mensaje = mensaje
                    };
                    f.ShowDialog(Fprpal);
                    if (f.Resultado == DialogResult.OK)
                    {
                        if (po.StatusID == 5) // 5  Aceptada
                        {
                            po.StatusID = 6;
                            perfilVal.UpdateItemHeader<OrderHeader>(po);
                        }
                    }
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
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

        #endregion

        #region Header CRUD





        #endregion

        #region Details CRUD

        public string InsertDetail<T>(T item, DataRow headerDR, FDetails fDetails)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
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
                case EPerfiles.UPR:
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
                case EPerfiles.VAL:
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
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
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
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                            perfilPr.DeleteDetail(pr, detailID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                        default:
                            break;
                    }

                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }

        public string UpdateDetail(object newValue, DataRow detailDR, DataRow headerDR, string campo, bool isExent)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var detailID = Convert.ToInt32(detailDR["detailID"]);
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
                case EPerfiles.UPR:
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
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }
        #endregion

        #region Attach CRUD

        public string InsertAttach(Attaches item, DataRow headerDR, string path)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            if (!Directory.Exists(ConfigApp.FolderApp + headerID))
            {
                Directory.CreateDirectory(ConfigApp.FolderApp + headerID);
            }
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    if (File.Exists($"{ConfigApp.FolderApp}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPo.InsertAttach<OrderHeader>(item, po);
                    //! Copiar el archivo en la carpeta
                    CopyFile(path, $"{ConfigApp.FolderApp}{headerID}{item.FileName}");
                    break;
                case EPerfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    if (File.Exists($"{ConfigApp.FolderApp}{headerID}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    perfilPr.InsertAttach<RequisitionHeader>(item, pr);
                    //! Copiar el archivo en la carpeta
                    CopyFile(path, $"{ConfigApp.FolderApp}{headerID}{item.FileName}");
                    break;
                case EPerfiles.VAL:
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
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            //! Borrar el archivo de la carpeta
                            DeleteFile($"{ConfigApp.FolderApp}{headerID}{attachDR["FileName"]}");
                            perfilPo.DeleteAttach(po, attachID);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            //todo Saber si tiene acceso a la carpeta en el server: ds
                            // DirectorySecurity ds = Directory.GetAccessControl(configApp.FolderApp);
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            //! Borrar el archivo de la carpeta
                            DeleteFile($"{ConfigApp.FolderApp}{headerID}{attachDR["FileName"]}");
                            perfilPr.DeleteAttach<RequisitionHeader>(pr, attachID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                    }
                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }

        public string OpenAttach(DataRow attachDR, DataRow headerDR)
        {
            try
            {
                var headerID = Convert.ToInt32(headerDR["headerID"]);
                Process.Start($"{ConfigApp.FolderApp}{headerID}{attachDR["FileName"]}");
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
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
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
                case EPerfiles.UPR:
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
                case EPerfiles.VAL:
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

        #region Hitos CRUD

        public string InsertHito(OrderHitos item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.InsertHito(item, headerID);
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

        public string DeleteHito(DataRow hitoDR, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.DeleteHito(headerID, Convert.ToInt32(hitoDR["HitoID"]));
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

        public string UpdateHito(object newValue, DataRow hitoDR, DataRow headerDR, string campo)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var hitoID = Convert.ToInt32(hitoDR["HitoID"]);
            var h = new OrderHitos().GetByID(hitoID);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
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

        #region Notes CRUD

        public string InsertNote(OrderNotes item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.InsertNote(item, headerID);
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

        public string DeleteNote(DataRow noteDR, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.DeleteNote(headerID, Convert.ToInt32(noteDR["OrderNoteID"]));
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

        public string UpdateNote(object newValue, DataRow hitoDR, DataRow headerDR, string campo)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var noteID = Convert.ToInt32(hitoDR["OrderNoteID"]);
            var n = new OrderNotes().GetByID(noteID);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
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

        #region Delivery CRUD

        public string InsertDelivery(OrderDelivery item, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.InsertDelivery(item, headerID);
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

        public string DeleteDelivery(DataRow noteDR, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    if (status >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    perfilPo.DeleteDelivery(headerID, Convert.ToInt32(noteDR["DeliveryID"]));
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
