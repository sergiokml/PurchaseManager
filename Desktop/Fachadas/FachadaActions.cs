using System;
using System.Data;
using System.Data.Entity;
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
using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaActions
    {
        public FPrincipal Fprpal { get; set; }
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }
        public ConfigApp ConfigApp { get; set; }

        public FachadaActions(IPerfilActions perfilActions, ConfigApp configApp)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
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
                                    UserID = PerfilActions.CurrentUser.UserID,
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
                                DataRow d = PerfilActions
                                    .GetDataRow(TypeDocumentHeader.PR, Convert.ToInt32(headerDR["RequisitionHeaderID"]));
                                VerItemHtml(d);
                            }
                            break;
                        case EPerfiles.UPR:
                            break;
                        case EPerfiles.VAL:
                            if (headerDR["RequisitionHeaderID"] != DBNull.Value)
                            {
                                DataRow e = PerfilActions
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
                                var listaPath = new HtmlManipulate(ConfigApp).ReemplazarDatos(headerDR, PerfilActions.CurrentUser, po);
                                var a = await new HtmlManipulate(ConfigApp).ConvertHtmlToPdf(listaPath, headerID.ToString());
                                //! Adjuntos
                                //TODO  QUITAR ESTO Y USAR LA REFERENCIA?
                                po.Attaches.ToList().RemoveAll(c => c.Modifier == 0);
                                //! Email To Supplier
                                var send = new SendEmailTo(ConfigApp);
                                var asunto = $"Orden de Compra {po.Code} ({po.CompanyID})";
                                await send.SendEmailToSupplier(a, asunto, PerfilActions.CurrentUser, supp, po.Attaches.ToList(), po.OrderHeaderID);
                                //! Update the PO
                                po.StatusID = 4;
                                PerfilActions.UpdateItemHeader<OrderHeader>(po);

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
                            PerfilActions.UpdateItemHeader<OrderHeader>(po);
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
                            PerfilActions.UpdateItemHeader<OrderHeader>(po);
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
                            PerfilActions.UpdateItemHeader<OrderHeader>(po);
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
                            PerfilActions.UpdateItemHeader<OrderHeader>(po);
                        }
                    }
                    break;
            }
            Fprpal.LlenarGrid();
            Fprpal.ClearControles();
            Fprpal.SetControles();
            Fprpal.CargarDashboard();
        }
    }
}
