using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bunifu.Charts.WinForms;
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
            configApp = new ConfigApp().GetUnique();
            perfilPr = upr;
            perfilPo = upo;
            perfilVal = val;
            Enum.TryParse(user.ProfileID, out Perfiles p);
            currentPerfil = p;
            upr.CurrentUser = user;
            upo.CurrentUser = user;
            val.CurrentUser = user;
        }

        #region Cargar Controles

        public void CargarGrid(iGrid grid, string form)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = grid;
                    perfilPo.PintarGrid();
                    switch (form)
                    {
                        case "FPrincipal":
                            perfilPo.CargarColumnasFPrincipal(Perfiles.UPO);
                            break;
                        case "FDetails":
                            perfilPo.CargarColumnasFDetail(Perfiles.UPO);
                            break;
                        case "FAttach":
                            perfilPo.CargarColumnasFAttach(Perfiles.UPO);
                            break;
                        case "FSupplier":
                            perfilPo.CargarColumnasFSupplier(Perfiles.UPO);
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = grid;
                    perfilPr.PintarGrid();
                    switch (form)
                    {
                        case "FPrincipal":
                            perfilPr.CargarColumnasFPrincipal(Perfiles.UPR);
                            break;
                        case "FDetails":
                            perfilPr.CargarColumnasFDetail(Perfiles.UPR);
                            break;
                        case "FAttach":
                            perfilPr.CargarColumnasFAttach(Perfiles.UPR);
                            break;
                        case "FSupplier":
                            perfilPr.CargarColumnasFSupplier(Perfiles.UPR);
                            break;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
        }

        public bool CargarContextMenuStrip(ContextMenuStrip context, DataRow dr)
        {
            Enum.TryParse(dr["TypeDocumentHeader"].ToString(), out TypeDocumentHeader typeDoc);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (typeDoc)
                    {
                        case TypeDocumentHeader.PR:
                            perfilPo.CtxMenu = context;
                            perfilPo.LLenarMenuContext();
                            perfilPo.CtxMenu.Items.RemoveAt(0);
                            return true;
                        case TypeDocumentHeader.PO:
                            perfilPo.CtxMenu = context;
                            perfilPo.LLenarMenuContext();
                            perfilPo.CtxMenu.Items.RemoveAt(1);
                            return true;
                    }
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }

        public void CargarDashBoard(iGrid grid, BunifuPieChart c1, BunifuPieChart c2, BunifuChartCanvas cc1, BunifuChartCanvas cc2, Label label1, Label label2, Label label3)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = grid;
                    perfilPo.SetResultFunctions();
                    perfilPo.CargarDatos(perfilPo.CurrentUser, c1, c2, cc1, cc2, label1, label2, label3);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = grid;
                    perfilPr.SetResultFunctions();
                    perfilPr.CargarDatos(perfilPr.CurrentUser, c1, c2, cc1, cc2, label1, label2, label3);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
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

        public bool OpenAttachForm(DataRow headerDR, FPrincipal fPrincipal)
        {
            FAttach f = new FAttach(this, headerDR)
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterParent
            };
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = f.GetGrid();
                    perfilPr.PintarGrid();
                    f.ShowDialog(fPrincipal);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }
        public bool OpenSupplierForm(FSupplier fSupplier, FPrincipal fPrincipal)
        {
            fSupplier.ShowInTaskbar = false;
            fSupplier.StartPosition = FormStartPosition.CenterParent;
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = fSupplier.GetGrid();
                    perfilPo.PintarGrid();
                    fSupplier.ShowDialog(fPrincipal);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = fSupplier.GetGrid();
                    perfilPr.PintarGrid();
                    fSupplier.ShowDialog(fPrincipal);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
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
                    break;
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
                    return perfilPo.VistaFDetalles(td, Convert.ToInt32(headerDR["HeaderID"]));
                case Perfiles.UPR:
                    return perfilPr.VistaFDetalles(td, Convert.ToInt32(headerDR["HeaderID"]));
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
                    return perfilPo.VistaFAdjuntos(td, Convert.ToInt32(headerDR["HeaderID"]));
                case Perfiles.UPR:
                    return perfilPr.VistaFAdjuntos(td, Convert.ToInt32(headerDR["HeaderID"]));
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
                    return perfilPo.VistaFProveedores(td, Convert.ToInt32(headerDR["HeaderID"]));
                case Perfiles.UPR:
                    return perfilPr.VistaFProveedores(td, Convert.ToInt32(headerDR["HeaderID"]));
                case Perfiles.VAL:
                    break;
            }
            return null;
        }

        #endregion

        #region Métodos

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
                    break;
                default:
                    break;
            }
            return null;
        }
        private bool ValidarOrderHeader(DataRow headerDR)
        {
            string res = headerDR["Description"].ToString();
            if (string.IsNullOrEmpty(headerDR["Description"].ToString()))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region IGrid
        public iGrid FormatearGrid(iGrid grid)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = grid;
                    perfilPo.Formatear(Perfiles.UPO);
                    return perfilPo.Grid;
                case Perfiles.UPR:
                    perfilPr.Grid = grid;
                    perfilPr.Formatear(Perfiles.UPR);
                    return perfilPr.Grid;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return grid;
        }

        #endregion

        #region Header CRUD

        public bool InsertItem(Companies company, DocumentType type)
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
                        Type = (byte)type,
                        StatusID = 1,
                        Net = 0,
                        Exent = 0,
                        CompanyID = company.CompanyID
                    };
                    perfilPo.InsertPOHeader(po);
                    return true;
                case Perfiles.UPR:
                    var pr = new RequisitionHeader
                    {
                        Type = (byte)type,
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

        public bool UpdateItem(object newValue, DataRow headerDR, string campo)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var HeaderID = Convert.ToInt32(headerDR["HeaderID"]);
            RequisitionHeader pr;
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
                            pr = new RequisitionHeader().GetById(HeaderID);
                            switch (campo)
                            {
                                case "Type":
                                    pr.Type = Convert.ToByte(newValue);
                                    break;
                            }
                            perfilPo.UpdateItemHeader<RequisitionHeader>(td, pr, HeaderID);
                            return true;
                        case TypeDocumentHeader.PO:
                            OrderHeader po = new OrderHeader().GetById(HeaderID);
                            switch (campo)
                            {
                                case "Description":
                                    po.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                                    break;
                                case "Type":
                                    po.Type = Convert.ToByte(newValue);
                                    break;
                                case "Net":
                                    po.Net = Convert.ToInt32(newValue);
                                    break;
                                case "SupplierID":
                                    po.SupplierID = newValue.ToString();
                                    break;
                                case "CurrencyID":
                                    po.CurrencyID = newValue.ToString();
                                    break;
                            }
                            perfilPo.UpdateItemHeader<OrderHeader>(td, po, HeaderID);
                            return true;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    pr = new RequisitionHeader().GetById(HeaderID);
                    if (status == 1)
                    {
                        switch (campo)
                        {
                            case "Description":
                                pr.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                                break;
                            case "Type":
                                pr.Type = Convert.ToByte(newValue);
                                break;
                            default:
                                break;
                        }
                        perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, Convert.ToInt32(headerDR["HeaderID"]));
                        return true;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }

        public bool DeleteItem(DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (perfilPo.CurrentUser.UserID == headerDR["UserID"].ToString())
                    {
                        if (status == 1)
                        {
                            //if (headerDR["RequisitionHeaderID"] != DBNull.Value)
                            //{
                            //    //! Volver a poner en estado 2 la PR
                            //    RequisitionHeader pr = new RequisitionHeader().GetById(Convert.ToInt32(headerDR["RequisitionHeaderID"]));
                            //    pr.StatusID = 2;
                            //    perfilPo.UpdateItemHeader<RequisitionHeader>(TypeDocumentHeader.PR, pr, pr.RequisitionHeaderID);
                            //}
                            perfilPo.DeleteItemHeader(td, Convert.ToInt32(headerDR["HeaderID"]));
                            return true;
                        }
                    }
                    break;
                case Perfiles.UPR:
                    if (perfilPr.CurrentUser.UserID == headerDR["UserID"].ToString())
                    {
                        if (status == 1)
                        {
                            perfilPr.DeleteItemHeader(td, Convert.ToInt32(headerDR["HeaderID"]));
                            return true;
                        }
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }

        #endregion

        #region Details CRUD

        public bool InsertDetail(object item, DataRow headerDR)
        {
            var HeaderID = Convert.ToInt32(headerDR["HeaderID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.InsertDetail(item, HeaderID);
                    return true;
                case Perfiles.UPR:
                    perfilPr.InsertDetail(item, HeaderID);
                    return true;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;

        }

        public bool DeleteDetail(DataRow detailDR, DataRow headerDR)
        {
            //! Acá se define primero si es PO o PR pero igualmente se implementa la funcion en perilPX
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (Convert.ToInt32(headerDR["StatusID"]) <= 2)
                    {
                        switch (td)
                        {
                            case TypeDocumentHeader.PR:
                                return false;
                            case TypeDocumentHeader.PO:
                                perfilPo.DeleteDetail(td, Convert.ToInt32(headerDR["HeaderID"]), Convert.ToInt32(detailDR["DetailID"]));
                                return true;
                            default:
                                break;
                        }
                    }
                    break;
                case Perfiles.UPR:
                    if (Convert.ToInt32(headerDR["StatusID"]) <= 2)
                    {
                        perfilPr.DeleteDetail(td, Convert.ToInt32(headerDR["HeaderID"]), Convert.ToInt32(detailDR["DetailID"]));
                        return true;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }

        #endregion

        #region Attach CRUD

        public bool InsertAttach(Attaches item, int headerDR, string path)
        {
            if (!Directory.Exists(configApp.FolderApp + headerDR))
            {
                Directory.CreateDirectory(configApp.FolderApp + headerDR);
            }
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.InsertAttach(item, headerDR);
                    File.Copy(path, $"{configApp.FolderApp}{@"\"}{@"\"}{item.FileName}", true);
                    return true;
                case Perfiles.UPR:
                    perfilPr.InsertAttach(item, headerDR);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
        }

        public bool BorrarAdjunto(DataRow attachDR, DataRow headerDR)
        {
            int status = Convert.ToInt32(headerDR["StatusID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (status == 1)
                    {
                        perfilPo.DeleteAttach(Convert.ToInt32(headerDR["HeaderID"]), Convert.ToInt32(attachDR["AttachID"]));
                        return true;
                    }
                    break;
                case Perfiles.UPR:
                    if (status == 1)
                    {
                        perfilPr.DeleteAttach(Convert.ToInt32(headerDR["HeaderID"]), Convert.ToInt32(attachDR["AttachID"]));
                        return true;
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }


            return false;
        }

        public string VerAttach(DataRow attachDR)
        {
            try
            {
                string serverfolder = configApp.FolderApp;
                serverfolder += attachDR["FileName"].ToString();
                Process.Start(serverfolder);
                return "Opening...";
            }
            catch (Exception)
            {
                return "The system cannot find the requested path.";
            }
        }

        #endregion

        #region Acciones Varias

        public bool VerItemHtml(DataRow headerDR)
        {
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
                            if (Convert.ToInt32(headerDR["DetailsCount"]) > 0)
                            {
                                var details = new RequisitionDetails().GetListByID(Convert.ToInt32(headerDR["HeaderID"]));
                                Process.Start(new HtmlManipulate(configApp)
                                    .ReemplazarDatos(headerDR, perfilPo.CurrentUser, details));
                                return true;
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            if (Convert.ToInt32(headerDR["DetailsCount"]) > 0 && headerDR["SupplierID"] != DBNull.Value)
                            {
                                var details = new OrderDetails().GetListByID(Convert.ToInt32(headerDR["HeaderID"]));
                                Process.Start(new HtmlManipulate(configApp)
                                    .ReemplazarDatos(headerDR, perfilPo.CurrentUser, details));
                                return true;
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
                            if (Convert.ToInt32(headerDR["DetailsCount"]) > 0)
                            {
                                var details = new RequisitionDetails().GetListByID(Convert.ToInt32(headerDR["HeaderID"]));
                                Process.Start(new HtmlManipulate(configApp)
                                    .ReemplazarDatos(headerDR, perfilPr.CurrentUser, details));

                            }
                            return true;
                        case TypeDocumentHeader.PO:
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
            return false;
        }

        public async Task<string> SendItem(DataRow headerDR)
        {
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
                            if (Convert.ToInt32(headerDR["DetailsCount"]) > 0)
                            {
                                var details = new RequisitionDetails().GetListByID(Convert.ToInt32(headerDR["HeaderID"]));
                                //var attaches = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                                var path = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPr.CurrentUser, details);
                                var send = new SendEmailTo(configApp.Email, configApp.Password);
                                var x = await send.SendEmail(path, asunto: "Purchase Manager: Requisition document", perfilPr.CurrentUser);

                                return send.MessageResult;
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            break;
                        default:
                            break;
                    }
                    break;
                case Perfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            if (Convert.ToInt32(headerDR["DetailsCount"]) > 0)
                            {
                                var details = new RequisitionDetails().GetListByID(Convert.ToInt32(headerDR["HeaderID"]));
                                //var attaches = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                                var path = new HtmlManipulate(configApp).ReemplazarDatos(headerDR, perfilPr.CurrentUser, details);
                                var send = new SendEmailTo(configApp.Email, configApp.Password);
                                var x = await send.SendEmail(path, asunto: "Purchase Manager: Requisition document", perfilPr.CurrentUser);

                                return send.MessageResult;
                            }
                            break;
                        case TypeDocumentHeader.PO:
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
            return null;
        }

        public bool GridDobleClick(DataRow headerDR)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var HeaderID = Convert.ToInt32(headerDR["HeaderID"]);
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
                    if (td == TypeDocumentHeader.PO)
                    {
                        po = new OrderHeader().GetById(HeaderID);
                        var od = new OrderDetails().GetListByID(po.OrderHeaderID);
                        if (od.Count > 0)
                        {
                            if (Convert.ToSByte(headerDR["StatusID"]) == 1)
                            {
                                if (ValidarOrderHeader(headerDR))
                                {
                                    po.StatusID = 2;
                                    perfilPo.UpdateItemHeader<OrderHeader>(td, po, HeaderID);
                                    return true;
                                }
                            }
                            else if (Convert.ToSByte(headerDR["StatusID"]) == 2)
                            {
                                po.StatusID = 1;
                                perfilPo.UpdateItemHeader<OrderHeader>(td, po, HeaderID);
                                return true;
                            }
                        }
                    }
                    break;
                case Perfiles.UPR:
                    //! Update Status
                    if (td == TypeDocumentHeader.PR)
                    {
                        pr = new RequisitionHeader().GetById(HeaderID);
                        var details = new RequisitionDetails().GetListByID(pr.RequisitionHeaderID);
                        if (details.Count > 0)
                        {
                            if (Convert.ToSByte(headerDR["StatusID"]) == 1)
                            {
                                if (ValidarOrderHeader(headerDR))
                                {
                                    pr.StatusID = 2;
                                    perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, HeaderID);
                                    return true;
                                }
                            }
                            else if (Convert.ToSByte(headerDR["StatusID"]) == 2)
                            {
                                pr.StatusID = 1;
                                perfilPr.UpdateItemHeader<RequisitionHeader>(td, pr, HeaderID);
                                return true;
                            }
                        }
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }

            return false;
        }

        public bool SeleccionarContextMenuStrip(DataRow headerDR, string action)
        {
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            RequisitionHeader pr;
            var HeaderID = Convert.ToInt32(headerDR["HeaderID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (action == "Convert To PO")
                    {
                        //! Crear una PO desde una PR
                        //! Cambio el estado de la PR a 3
                        pr = new RequisitionHeader().GetById(HeaderID);
                        pr.StatusID = 3;
                        perfilPo.UpdateItemHeader(td, pr, HeaderID);
                        //! Insert PO
                        var po = new OrderHeader
                        {
                            Description = $"[From PR] {pr.Description}",
                            Type = Convert.ToByte(headerDR["Type"]),
                            StatusID = 1,
                            Net = 0,
                            Exent = 0,
                            RequisitionHeaderID = pr.RequisitionHeaderID,
                            CompanyID = pr.CompanyID,
                            Discount = 0
                        };
                        //! Traspasar los detalles
                        var details = new RequisitionDetails().GetListByID(Convert.ToInt32(headerDR["HeaderID"]));
                        foreach (var item in details)
                        {
                            var detail = new OrderDetails
                            {
                                Qty = item.Qty,
                                AccountID = item.AccountID,
                                DescriptionProduct = $"[From PR] {item.DescriptionProduct}",
                                NameProduct = $"[From PR] {item.NameProduct}",
                                MedidaID = item.MedidaID
                            };
                            po.OrderDetails.Add(detail);
                        }

                        perfilPo.InsertPOHeader(po);
                    }
                    return true;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
            return false;
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
                    break;
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
