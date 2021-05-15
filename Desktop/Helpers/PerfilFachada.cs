using System;
using System.Data;
using System.Diagnostics;
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
        protected PerfilUPR perfilPr;
        protected PerfilUPO perfilPo;
        protected PerfilVAL perfilVal;
        private readonly Users user;

        public PerfilFachada(PerfilUPR perfilPr, PerfilUPO perfilPo, PerfilVAL perfilVal, Users userDB)
        {
            this.perfilPr = perfilPr;
            this.perfilPo = perfilPo;
            this.perfilVal = perfilVal;
            user = userDB;
        }

        public DataTable GetVistaPrincipal()
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return perfilPo.GetVistaFPrincipal(user);
                case "UPR":
                    return perfilPr.GetVistaFPrincipal(user);
                case "VAL":
                    return perfilVal.GetVistaFPrincipal(user);
            }
            return null;
        }
        public DataTable GetVistaDetalles(DataRow dataRow)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                //perfilPr.GetDetailsItem(ItemID, typeDocHeader);
                //return perfilPr.TableDetails;
                case "UPR":
                    perfilPr.Current = dataRow;
                    return perfilPr.GetVistaDetalles();
                case "VAL":
                    //return perfilVal.GetVista(userDB);
                    break;
            }
            return null;
        }
        public DataTable GetVistaAttaches(int ItemID)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    //return perfilPo.GetVista(userDB);
                    break;
                case "UPR":
                    return perfilPr.GetVistaAttaches(ItemID);
                case "VAL":
                    //return perfilVal.GetVista(userDB);
                    break;
            }
            return null;
        }
        public Users GetUser()
        {
            return user;
        }

        public DataTable GetVistaSuppliers()
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return perfilPo.GetVistaFSupplier();
                case "UPR":
                    return perfilPr.GetVistaFSupplier();
                case "VAL":
                    return perfilVal.GetVistaFSupplier();
            }
            return null;
        }

        public iGrid CargarGrid(iGrid grid, string form)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Grid = grid;
                    perfilPo.User = user;
                    perfilPo.PintarGrid();
                    switch (form)
                    {
                        case "FPrincipal":
                            perfilPo.CargarColumnasFPrincipal();
                            break;
                        case "FDetails":
                            perfilPo.CargarColumnasFDetail();
                            break;
                        case "FAttach":
                            perfilPo.CargarColumnasFAttach();
                            break;
                        case "FSupplier":
                            perfilPo.CargarColumnasFSupplier();
                            break;
                    }
                    return perfilPo.Grid;
                case "UPR":
                    perfilPr.Grid = grid;
                    perfilPr.User = user;
                    perfilPr.PintarGrid();
                    switch (form)
                    {
                        case "FPrincipal":
                            perfilPr.CargarColumnasFPrincipal();
                            break;
                        case "FDetails":
                            perfilPr.CargarColumnasFDetail();
                            break;
                        case "FAttach":
                            perfilPr.CargarColumnasFAttach();
                            break;
                    }
                    return perfilPr.Grid;
                case "VAL":
                    break;

            }
            return null;
        }

        public iGrid FormatearGrid(iGrid grid)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Grid = grid;
                    perfilPo.Formatear();
                    return perfilPo.Grid;
                case "UPR":
                    perfilPr.Grid = grid;
                    perfilPr.Formatear();
                    return perfilPr.Grid;
                case "VAL":
                    break;
            }
            return null;
        }

        public void InsertItem(Companies company, OrderType type)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    var po = new OrderHeader
                    {
                        Type = (byte)type,
                        StatusID = 1,
                        Description = string.Empty,
                        CompanyID = company.CompanyID,
                        Net = 0,
                        Exent = 0
                    };
                    perfilPo.DocumentPO = po;
                    perfilPo.InsertItemHeader(user);
                    break;
                case "UPR":
                    var pr = new RequisitionHeader
                    {
                        Type = (byte)type,
                        StatusID = 1,
                        Description = string.Empty,
                        CompanyID = company.CompanyID
                    };
                    perfilPr.DocumentPR = pr;
                    perfilPr.InsertItemHeader(user);
                    break;
                case "VAL":
                    break;
            }
        }

        public bool UpdateItem(object newValue, DataRow dr, string campo)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Current = dr;
                    switch (campo)
                    {
                        case "Description":
                            if (Convert.ToInt32(dr["StatusID"]) == 1)
                            {
                                perfilPo.UpdateItemHeader(user, newValue, campo);
                                return true;
                            }
                            break;
                        case "Type":
                            if (Convert.ToInt32(dr["StatusID"]) == 1)
                            {
                                perfilPo.UpdateItemHeader(user, newValue, campo);
                                return true;
                            }
                            break;
                        case "StatusID":
                            if (user.UserID == dr["UserID"].ToString())
                            {
                                if (ValidarOrderHeader(dr))
                                {
                                    perfilPo.UpdateItemHeader(user, newValue, campo);
                                    return true;
                                }
                            }
                            break;
                    }
                    break;
                case "UPR":
                    perfilPr.Current = dr;
                    switch (campo)
                    {
                        case "Description":
                            if (Convert.ToInt32(dr["StatusID"]) == 1)
                            {
                                perfilPr.UpdateItemHeader(user, newValue, campo);
                                return true;
                            }
                            break;
                        case "Type":
                            if (Convert.ToInt32(dr["StatusID"]) == 1)
                            {
                                perfilPr.UpdateItemHeader(user, newValue, campo);
                                return true;
                            }
                            break;
                        case "StatusID":
                            if (ValidarOrderHeader(dr))
                            {
                                perfilPr.UpdateItemHeader(user, newValue, campo);
                                return true;
                            }
                            break;
                    }
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        private bool ValidarOrderHeader(DataRow dataRow)
        {
            var res = dataRow["Description"].ToString();
            if (string.IsNullOrEmpty(dataRow["Description"].ToString()))
            {
                return false;
            }
            return true;
        }

        public bool DeleteItem(DataRow dr)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Current = dr;
                    if (user.UserID == dr["UserID"].ToString())
                    {
                        if (Convert.ToInt32(dr["StatusID"]) == 1)
                        {
                            perfilPo.DeleteItemHeader();
                            return true;
                        }
                    }
                    break;
                case "UPR":
                    perfilPr.Current = dr;
                    if (Convert.ToInt32(dr["StatusID"]) == 1) // Puede eliminar sólo en 1
                    {
                        perfilPr.DeleteItemHeader();
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool ConvertirDoc(DataRow dr)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Current = dr;
                    perfilPo.DeleteItemHeader();
                    return true;


                case "UPR":
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool DeleteDetail(DataRow drD, DataRow drH)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    //if (orderHeader.StatusID < 6)
                    //{
                    //    perfilPo.DeleteOrderDetail(orderHeader, idDetail, userDB);
                    //    return true;
                    //}
                    break;

                case "UPR":
                    if (Convert.ToInt32(drH["StatusID"]) <= 2)
                    {
                        perfilPr.Current = drD;
                        perfilPr.DeleteDetail(user);
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }

            return false;
        }

        public bool DeleteAttach(DataRow dataRow, int status, int header)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    //if (status.StatusID < 6)
                    //{
                    //    var id = Convert.ToInt32(dataRow["DetailID"]);
                    //    perfilPo.DeleteDetail(orderHeader, idDetail, userDB);
                    //    return true;
                    //}
                    break;
                case "UPR":
                    if (status < 6)
                    {
                        Attaches att = new Attaches
                        {
                            AttachID = Convert.ToInt32(dataRow["AttachID"])
                        };
                        perfilPr.DeleteAttache(header, user, att);
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }

            return false;
        }

        public bool InsertDetail(object item, int ItemId)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    break;
                case "UPR":
                    var detail = (RequisitionDetails)item;
                    perfilPr.InsertRequisitionDetail(detail, user, ItemId);
                    return true;
                case "VAL":
                    break;
            }
            return false;

        }

        public bool OpenSupplierForm(DataRow dataRow)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    var f = new FSupplier(this)
                    {
                        ShowInTaskbar = false,
                        StartPosition = FormStartPosition.CenterScreen,
                        ItemStatus = Convert.ToInt32(dataRow["StatusID"])
                    };
                    if (dataRow["SupplierID"].ToString().Length > 0)
                    {
                        f.HeaderID = Convert.ToInt32(dataRow["SupplierID"]);
                    }
                    perfilPo.Grid = f.GetGrid();
                    perfilPo.PintarGrid();
                    f.ShowDialog();
                    break;
                case "UPR":
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public void OPenDetailForm(DataRow dr)
        {
            FDetails f = new FDetails(this, dr)
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Grid = f.GetGrid();
                    perfilPo.PintarGrid();
                    f.ShowDialog();
                    break;
                case "UPR":
                    perfilPr.Grid = f.GetGrid();
                    perfilPr.PintarGrid();
                    f.ShowDialog();
                    break;
                case "VAL":
                    break;
            }

        }

        public bool OpenAttachForm(DataRow dataRow)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    break;
                case "UPR":
                    var att = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    var f = new FAttach(this);
                    perfilPr.Grid = f.GetGrid();
                    perfilPr.PintarGrid();
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.HeaderID = Convert.ToInt32(dataRow["RequisitionHeaderID"]);
                    f.ItemStatus = Convert.ToInt32(dataRow["StatusID"]);
                    f.ShowDialog();
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool InsertAttach(Attaches item, int id)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    break;
                case "UPR":
                    perfilPr.InsertAttach(id, item, user);
                    return true;
                case "VAL":
                    break;
            }
            return false;
        }

        public string VerItemHtml(DataRow dataRow)
        {
            switch (user.UserProfiles.ProfileID)
            {

                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    if (dataRow["TypeDocumentHeader"].ToString() == "PR")
                    {
                        //var detailsPR = perfilPo.GetDetailsRequisition(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        //if (detailsPR.Count == 0)
                        //{
                        //    return "This requisition has no products.";
                        //}
                        //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPR));
                        //return "Opening...";
                    }
                    else if (dataRow["TypeDocumentHeader"].ToString() == "PO")
                    {
                        //var detailsPR = perfilPo.GetDetailsOrder(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        //if (detailsPR.Count == 0)
                        //{
                        //    return "This Order has no products.";
                        //}
                        //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPR));
                        //return "Opening...";
                    }
                    break;
                case "UPR":
                //var detailsPO = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                //if (detailsPO.Count == 0)
                //{
                //    return "This requisition has no products.";
                //}
                //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPO));
                //return "Opening...";
                case "VAL":
                    break;
            }
            return string.Empty;

        }

        public string VerAttach(DataRow dataRow)
        {
            Process.Start(dataRow["FileName"].ToString());
            return "Opening...";
        }

        public async Task<string> SendItem(DataRow dataRow)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    break;
                case "UPR":
                //var details = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                //if (details.Count == 0)
                //{
                //    return "NODETAILS";
                //}
                // var attaches = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                //var path = new HtmlManipulate().ReemplazarDatos(dataRow, userDB, details);
                //var send = new SendEmailTo(Properties.Settings.Default.Email, Properties.Settings.Default.Password);
                //var x = await send.SendEmail(path, asunto: "Purchase Manager: Requisition document", userDB);

                // return send.MessageResult;

                case "VAL":
                    break;
            }
            return null;
        }

        public void CargarDashBoard(iGrid grid, BunifuPieChart chart1, BunifuPieChart chart2,
             BunifuChartCanvas chartCanvas1, BunifuChartCanvas chartCanvas2, Label label1, Label label2, Label label3)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Grid = grid;
                    perfilPo.GetFunciones();
                    perfilPo.CargarDatos(chart1, chart2, chartCanvas1, chartCanvas2, label1, label2, label3);
                    break;
                case "UPR":
                    perfilPr.Grid = grid;
                    perfilPr.GetFunciones();
                    perfilPr.CargarDatos(chart1, chart2, chartCanvas1, chartCanvas2, label1, label2, label3);
                    break;
                case "VAL":
                    break;
            }
        }

        public async Task<string> CargarBanner()
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return await new HtmlManipulate().ReemplazarDatos();
                case "UPR":
                    return await new HtmlManipulate().ReemplazarDatos();
                case "VAL":
                    break;
            }
            return string.Empty;
        }

        public iGDropDownList CargarComBox(DataRow dataRow)
        {
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return perfilPo.GetStatusItem(dataRow);
                case "UPR":
                    break;
                case "VAL":
                    break;
            }
            return null;
        }
    }

}
