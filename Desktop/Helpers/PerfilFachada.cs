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

namespace PurchaseDesktop.Helpers
{
    public class PerfilFachada
    {
        protected PerfilUPR perfilPr;
        protected PerfilUPO perfilPo;
        protected PerfilVAL perfilVal;
        private readonly Users userDB;

        public PerfilFachada(PerfilUPR perfilPr, PerfilUPO perfilPo, PerfilVAL perfilVal, Users userDB)
        {
            this.perfilPr = perfilPr;
            this.perfilPo = perfilPo;
            this.perfilVal = perfilVal;
            this.userDB = userDB;
        }

        public DataTable GetVistaPrincipal()
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return perfilPo.GetVistaFPrincipal(userDB);
                case "UPR":
                    return perfilPr.GetVistaFPrincipal(userDB);
                case "VAL":
                    return perfilVal.GetVistaFPrincipal(userDB);
            }
            return null;
        }
        public DataTable GetVistaDetalles(int ItemID)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return perfilPo.GetVistaDetalles(ItemID);
                case "UPR":
                    return perfilPr.GetVistaDetalles(ItemID);
                case "VAL":
                    //return perfilVal.GetVista(userDB);
                    break;
            }
            return null;
        }
        public DataTable GetVistaAttaches(int ItemID)
        {
            switch (userDB.UserProfiles.ProfileID)
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
            return userDB;
        }

        public DataTable GetVistaSuppliers()
        {
            switch (userDB.UserProfiles.ProfileID)
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
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.Grid = grid;
                    perfilPo.User = userDB;
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
                    }
                    return perfilPo.Grid;
                case "UPR":
                    perfilPr.Grid = grid;
                    perfilPr.User = userDB;
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
            switch (userDB.UserProfiles.ProfileID)
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

        public void InsertItem(Companies company, HFunctions.OrderType type)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.InsertItemHeader(company, type, userDB);
                    break;
                case "UPR":
                    perfilPr.InsertItemHeader(company, type, userDB);
                    break;
                case "VAL":
                    break;
            }
        }

        public bool UpdateItem(object nuevovalor, DataRow dataRow, string campo)
        {
            var idHeader = 0;
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    idHeader = Convert.ToInt32(dataRow["OrderHeaderID"]);
                    //! Puede DELETE y UPDATE de PO emitidas por éste usuario.
                    if (campo == "Type" || campo == "Description")
                    {
                        if (userDB.UserID == dataRow["UserID"].ToString())
                        {
                            if (dataRow["TypeDocumentHeader"].ToString() == "PO")
                            {
                                if (Convert.ToInt32(dataRow["StatusID"]) < 7)// Puede modificar hasta 6 LAS PROPIAS
                                {
                                    perfilPo.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                                    return true;
                                }
                            }
                            else
                            {
                                //! Es PR
                            }

                        }
                    }
                    else if (campo == "StatusID")
                    {
                        var val = Convert.ToInt32(nuevovalor);
                        if (userDB.UserID == dataRow["UserID"].ToString())
                        {
                            if (dataRow["TypeDocumentHeader"].ToString() == "PO")
                            {
                                if (val >= 4 && val <= 9) // de 3 a 9
                                {
                                    if (ValidarOrderHeader(dataRow))
                                    {
                                        perfilPo.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                                        return true;
                                    }
                                }
                                else if (val == 3)
                                {
                                    perfilPo.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                                    return true;
                                }
                            }
                            else
                            {
                                //! Es PR
                            }

                        }
                        //else
                        //{
                        //    // No es de PO
                        //    if (val >= 4) // Valido solo si paso Active Y más
                        //    {
                        //        if (ValidarOrderHeader(dataRow))
                        //        {
                        //            perfilPo.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                        //            return true;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        perfilPo.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                        //        return true;
                        //    }
                        //}
                    }
                    break;
                case "UPR":
                    idHeader = Convert.ToInt32(dataRow["RequisitionHeaderID"]);
                    if (campo == "Description" && nuevovalor != null)
                    {
                        //! No UPDATE en estado 2
                        if (Convert.ToInt32(dataRow["StatusID"]) == 1)
                        {
                            perfilPr.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                            return true;
                        }
                    }
                    else if (campo == "Type" || campo == "StatusID")
                    {
                        if (ValidarOrderHeader(dataRow))
                        {
                            perfilPr.UpdateItemHeader(userDB, idHeader, nuevovalor, campo);
                            return true;
                        }
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

        public bool DeleteItem(DataRow dataRow)
        {
            var idHeader = 0;
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    idHeader = Convert.ToInt32(dataRow["OrderHeaderID"]);
                    if (Convert.ToInt32(dataRow["StatusID"]) == 3)
                    // Puede eliminar en 3 (de momento, podría elliminar en cualquier fase con advertencias)
                    {
                        perfilPo.DeleteOrderHeader(idHeader);
                        return true;
                    }
                    break;
                case "UPR":
                    if (Convert.ToInt32(dataRow["StatusID"]) == 1) // Puede eliminar sólo en 1
                    {
                        perfilPr.DeleteRequesitionHeader(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool DeleteDetail(DataRow dataRow, int status)
        {
            switch (userDB.UserProfiles.ProfileID)
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
                    if (status < 6)
                    {
                        perfilPr.DeleteDetail(Convert.ToInt32(dataRow["RequisitionHeaderID"]), Convert.ToInt32(dataRow["DetailID"]), userDB);
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
            switch (userDB.UserProfiles.ProfileID)
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
                        perfilPr.DeleteAttache(header, userDB, att);
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
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    break;
                case "UPR":
                    var detail = (RequisitionDetails)item;
                    perfilPr.InsertRequisitionDetail(detail, userDB, ItemId);
                    return true;
                case "VAL":
                    break;
            }
            return false;

        }
        public bool OpenSupplierForm(DataRow dataRow)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    var f = new FSupplier(this);
                    perfilPo.Grid = f.GetGrid();
                    perfilPo.PintarGrid();
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.ItemID = Convert.ToInt32(dataRow["SupplierID"]);
                    f.ItemStatus = Convert.ToInt32(dataRow["StatusID"]);
                    f.ShowDialog();
                    break;
                case "UPR":
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public void OPenDetailForm(DataRow dataRow)
        {
            FDetails f;
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    if (dataRow["TypeDocumentHeader"].ToString() == "PO")
                    {
                        var ordenDetails = perfilPo.GetDetailsOrder(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        f = new FDetails(this, ordenDetails);
                        perfilPo.Grid = f.GetGrid();
                        perfilPo.PintarGrid();
                        f.StartPosition = FormStartPosition.CenterScreen;
                        f.ItemID = Convert.ToInt32(dataRow["OrderHeaderID"]);
                        f.ItemStatus = Convert.ToInt32(dataRow["StatusID"]);
                        f.ShowDialog();
                    }
                    else if (dataRow["TypeDocumentHeader"].ToString() == "PR")
                    {

                    }


                    break;
                case "UPR":
                    var rd = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    f = new FDetails(this, rd);
                    perfilPr.Grid = f.GetGrid();
                    perfilPr.PintarGrid();
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.ItemID = Convert.ToInt32(dataRow["RequisitionHeaderID"]);
                    f.ItemStatus = Convert.ToInt32(dataRow["StatusID"]);
                    f.ShowDialog();
                    break;
                case "VAL":
                    break;
            }

        }

        public bool EditarDetails(FDetails fDetails, DataRow dataRow)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    fDetails.ItemID = Convert.ToInt32(dataRow["OrderHeaderID"]);
                    fDetails.StartPosition = FormStartPosition.CenterScreen;
                    fDetails.ShowDialog();
                    break;
                case "UPR":
                    fDetails.ItemID = Convert.ToInt32(dataRow["OrderHeaderID"]);
                    fDetails.StartPosition = FormStartPosition.CenterScreen;
                    fDetails.ShowDialog();
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool OpenAttachForm(DataRow dataRow)
        {
            switch (userDB.UserProfiles.ProfileID)
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
                    f.ItemID = Convert.ToInt32(dataRow["RequisitionHeaderID"]);
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
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    break;
                case "UPR":
                    perfilPr.InsertAttach(id, item, userDB);
                    return true;
                case "VAL":
                    break;
            }
            return false;
        }

        public string VerItemHtml(DataRow dataRow)
        {

            switch (userDB.UserProfiles.ProfileID)
            {

                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":

                    if (dataRow["TypeDocumentHeader"].ToString() == "PR")
                    {
                        var detailsPR = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        if (detailsPR.Count == 0)
                        {
                            return "This requisition has no products.";
                        }
                        Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPR));
                        return "Opening...";
                    }
                    if (dataRow["TypeDocumentHeader"].ToString() == "PO")
                    {
                        var detailsPR = perfilPo.GetDetailsOrder(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        if (detailsPR.Count == 0)
                        {
                            return "This Order has no products.";
                        }
                        //Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPR));
                        return "Opening...";
                    }
                    break;
                case "UPR":
                    var detailsPO = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    if (detailsPO.Count == 0)
                    {
                        return "This requisition has no products.";
                    }
                    Process.Start(new HtmlManipulate().ReemplazarDatos(dataRow, userDB, detailsPO));
                    return "Opening...";
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
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    break;
                case "UPR":
                    var details = perfilPr.GetDetailsRequisition(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    if (details.Count == 0)
                    {
                        return "NODETAILS";
                    }
                    // var attaches = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    var path = new HtmlManipulate().ReemplazarDatos(dataRow, userDB, details);
                    var send = new SendEmailTo(Properties.Settings.Default.Email, Properties.Settings.Default.Password);
                    var x = await send.SendEmail(path, asunto: "Purchase Manager: Requisition document", userDB);

                    return send.MessageResult;

                case "VAL":
                    break;
            }
            return null;
        }

        public void CargarDashBoard(BunifuPieChart chart1, BunifuPieChart chart2,
             BunifuChartCanvas chartCanvas1, BunifuChartCanvas chartCanvas2, Label label1, Label label2, Label label3)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.GetFunciones();
                    perfilPo.CargarDatos(chart1, chart2, chartCanvas1, chartCanvas2, label1, label2, label3);
                    break;
                case "UPR":
                    perfilPr.GetFunciones();
                    perfilPr.CargarDatos(chart1, chart2, chartCanvas1, chartCanvas2, label1, label2, label3);
                    break;
                case "VAL":
                    break;
            }
        }

        public async Task<string> CargarBanner()
        {
            switch (userDB.UserProfiles.ProfileID)
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
    }

}
