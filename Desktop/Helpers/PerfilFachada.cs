using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bunifu.Charts.WinForms;
using Bunifu.Charts.WinForms.ChartTypes;

using PurchaseData.DataModel;

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

        public PerfilFachada(UserProfileUPR upr, UserProfileUPO upo, UserProfileVAL val, Users user)
        {
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
                    }
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
        }

        public bool ContextMenuStrip(ContextMenuStrip context, DataRow dr)
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
                            return false;
                        case TypeDocumentHeader.PO:
                            perfilPo.CtxMenu = context;
                            perfilPo.LLenarMenuContext(Perfiles.UPO);
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
                    //perfilPo.Grid = grid;
                    //perfilPo.SetResultFunctions();
                    //perfilPo.CargarDatos(c1, c2, cc1, cc2, label1, label2, label3);
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = grid;
                    perfilPr.SetResultFunctions();
                    perfilPr.CargarDatos(c1, c2, cc1, cc2, label1, label2, label3);
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Abrir Formularios
        public void OPenDetailForm(DataRow dr)
        {
            var f = new FDetails(this, dr)
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    perfilPo.Grid = f.GetGrid();
                    perfilPo.PintarGrid();
                    f.ShowDialog();
                    break;
                case Perfiles.UPR:
                    perfilPr.Grid = f.GetGrid();
                    perfilPr.PintarGrid();
                    f.ShowDialog();
                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }
        }
        public bool OpenAttachForm(DataRow dr)
        {
            var f = new FAttach(this, dr)
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen
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
                    f.ShowDialog();
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
                    break;
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
                    break;
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
                    break;
                case Perfiles.UPR:
                    return perfilPr.VistaFDetalles(td, Convert.ToInt32(headerDR["HeaderID"]));
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
                    break;
                case Perfiles.UPR:
                    return perfilPr.VistaFDetalles(td, Convert.ToInt32(headerDR["HeaderID"]));
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
                    break;
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
            var res = headerDR["Description"].ToString();
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
                    //var po = new OrderHeader
                    //{
                    //    Type = (byte)type,
                    //    StatusID = 1,
                    //    Description = string.Empty,
                    //    CompanyID = company.CompanyID,
                    //    Net = 0,
                    //    Exent = 0
                    //};
                    //perfilPo.DocumentPO = po;
                    //perfilPo.InsertItemHeader(user);
                    break;
                case Perfiles.UPR:
                    perfilPr.InsertItemHeader(company, type);
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
            var status = Convert.ToInt32(headerDR["StatusID"]);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                //perfilPo.Current = dr;
                //switch (campo)
                //{
                //    case "Description":
                //        if (Convert.ToInt32(dr["StatusID"]) == 1)
                //        {
                //            perfilPo.UpdateItemHeader(user, newValue, campo);
                //            return true;
                //        }
                //        break;
                //    case "Type":
                //        if (Convert.ToInt32(dr["StatusID"]) == 1)
                //        {
                //            perfilPo.UpdateItemHeader(user, newValue, campo);
                //            return true;
                //        }
                //        break;
                //}
                //break;

                case Perfiles.UPR:
                    var pr = new RequisitionHeader().GetById(Convert.ToInt32(headerDR["HeaderID"]));
                    if (status == 1)
                    {
                        switch (campo)
                        {
                            case "Description":
                                pr.Description = UCase
                                    .ToTitleCase(newValue.ToString().ToLower());
                                break;
                            case "Type":
                                pr.Type = Convert.ToByte(newValue);
                                break;
                            default:
                                break;
                        }
                        perfilPr.UpdateItemHeader<RequisitionHeader>(pr, pr.RequisitionHeaderID);
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
            var status = Convert.ToInt32(headerDR["StatusID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (perfilPr.CurrentUser.UserID == headerDR["UserID"].ToString())
                    {
                        if (status == 1)
                        {
                            //perfilPo.DeleteItemHeader(td);
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
        public bool DeleteDetail(DataRow drD, DataRow drH)
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":
            //        //if (orderHeader.StatusID < 6)
            //        //{
            //        //    perfilPo.DeleteOrderDetail(orderHeader, idDetail, userDB);
            //        //    return true;
            //        //}
            //        break;

            //    case "UPR":
            //        if (Convert.ToInt32(drH["StatusID"]) <= 2)
            //        {
            //            perfilPr.Current = drD;
            //            perfilPr.DeleteDetail(user);
            //            return true;
            //        }
            //        break;
            //    case "VAL":
            //        break;
            //}

            return false;
        }

        #endregion
        public bool GridDobleClick(DataRow dr)
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
                        Type = Convert.ToByte(dr["Type"]),
                        StatusID = 1,
                        Description = dr["Description"].ToString(),
                        CompanyID = dr["CompanyID"].ToString(),
                        Net = 0,
                        Exent = 0,
                        RequisitionHeaderID = Convert.ToInt32(dr["HeaderID"])
                    };


                    // perfilPo.InsertItemHeader(user);

                    //! Update PO
                    //perfilPo.UpdateItemHeader(user, 3, "StatusID");
                    if (ValidarOrderHeader(dr))
                    {
                        //perfilPr.UpdateItemHeader(user, newValue, campo);
                        return true;
                    }
                    return true;
                case Perfiles.UPR:
                    //! Update Status                    
                    if (Convert.ToSByte(dr["StatusID"]) == 1)
                    {
                        if (ValidarOrderHeader(dr))
                        {
                            // perfilPr.UpdateItemHeader(user, 2, "StatusID");
                            return true;
                        }
                    }
                    else if (Convert.ToSByte(dr["StatusID"]) == 2)
                    {
                        //perfilPr.UpdateItemHeader(user, 1, "StatusID");
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


        public bool BorrarAdjunto(DataRow dr, DataRow drHeader)
        {
            switch (currentPerfil)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    //if (status.StatusID < 6)
                    //{
                    //    var id = Convert.ToInt32(dataRow["DetailID"]);
                    //    perfilPo.DeleteDetail(orderHeader, idDetail, userDB);
                    //    return true;
                    //}
                    break;
                case Perfiles.UPR:
                    if (Convert.ToSByte(dr["StatusID"]) < 6)
                    {
                        //perfilPr.DeleteAttach(Convert.ToSByte(dr["HeaderID"]), Convert.ToInt32(dr["AttachID"]));
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

        public bool InsertDetail(object item, DataRow dr)
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":

            //        break;
            //    case "UPR":
            //        var detail = (RequisitionDetails)item;
            //        perfilPr.InsertRequisitionDetail(detail, user, Convert.ToInt32(dr["HeaderID"]));
            //        return true;
            //    case "VAL":
            //        break;
            //}
            return false;

        }

        public bool OpenSupplierForm(DataRow dataRow)
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":
            //        var f = new FSupplier(this)
            //        {
            //            ShowInTaskbar = false,
            //            StartPosition = FormStartPosition.CenterScreen,
            //            ItemStatus = Convert.ToInt32(dataRow["StatusID"])
            //        };
            //        if (dataRow["SupplierID"].ToString().Length > 0)
            //        {
            //            f.HeaderID = Convert.ToInt32(dataRow["SupplierID"]);
            //        }
            //        perfilPo.Grid = f.GetGrid();
            //        perfilPo.PintarGrid();
            //        f.ShowDialog();
            //        break;
            //    case "UPR":
            //        break;
            //    case "VAL":
            //        break;
            //}
            return false;
        }




        public bool InsertAttach(Attaches item, int id)
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":

            //        break;
            //    case "UPR":
            //        perfilPr.InsertAttach(id, item, user);
            //        return true;
            //    case "VAL":
            //        break;
            //}
            return false;
        }

        public string VerItemHtml(DataRow dataRow)
        {
            //switch (user.UserProfiles.ProfileID)
            //{

            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":
            //        if (dataRow["TypeDocumentHeader"].ToString() == "PR")
            //        {
            //            //var detailsPR = perfilPo.GetDetailsRequisition(Convert.ToInt32(dataRow["OrderHeaderID"]));
            //            //if (detailsPR.Count == 0)
            //            //{
            //            //    return "This requisition has no products.";
            //            //}
            //            //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPR));
            //            //return "Opening...";
            //        }
            //        else if (dataRow["TypeDocumentHeader"].ToString() == "PO")
            //        {
            //            //var detailsPR = perfilPo.GetDetailsOrder(Convert.ToInt32(dataRow["OrderHeaderID"]));
            //            //if (detailsPR.Count == 0)
            //            //{
            //            //    return "This Order has no products.";
            //            //}
            //            //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPR));
            //            //return "Opening...";
            //        }
            //        break;
            //    case "UPR":
            //    //var detailsPO = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
            //    //if (detailsPO.Count == 0)
            //    //{
            //    //    return "This requisition has no products.";
            //    //}
            //    //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPO));
            //    //return "Opening...";
            //    case "VAL":
            //        break;
            //}
            return string.Empty;

        }

        public string VerAttach(DataRow dataRow)
        {
            Process.Start(dataRow["FileName"].ToString());
            return "Opening...";
        }

        public async Task<string> SendItem(DataRow dataRow)
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":
            //        break;
            //    case "UPR":
            //    //var details = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
            //    //if (details.Count == 0)
            //    //{
            //    //    return "NODETAILS";
            //    //}
            //    // var attaches = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
            //    //var path = new HtmlManipulate().ReemplazarDatos(dataRow, userDB, details);
            //    //var send = new SendEmailTo(Properties.Settings.Default.Email, Properties.Settings.Default.Password);
            //    //var x = await send.SendEmail(path, asunto: "Purchase Manager: Requisition document", userDB);

            //    // return send.MessageResult;

            //    case "VAL":
            //        break;
            //}
            return null;
        }


        public async Task<string> CargarBanner()
        {
            //switch (user.UserProfiles.ProfileID)
            //{
            //    case "ADM":
            //        break;
            //    case "BAS":
            //        break;
            //    case "UPO":
            //        return await new HtmlManipulate().ReemplazarDatos();
            //    case "UPR":
            //        return await new HtmlManipulate().ReemplazarDatos();
            //    case "VAL":
            //        break;
            //}
            return string.Empty;
        }

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
