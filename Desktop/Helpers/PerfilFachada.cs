﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                    return perfilPo.GetVista(userDB);
                case "UPR":
                    return perfilPr.GetVista(userDB);
                case "VAL":
                    return perfilVal.GetVista(userDB);
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
                    return perfilPo.GetVistaSuppliers();
                case "UPR":
                    return perfilPr.GetVistaSuppliers();
                case "VAL":
                    return perfilVal.GetVistaSuppliers();
            }
            return null;
        }

        //public List<OrderDetails> GetOrderDetails()
        //{
        //    switch (userDB.UserProfiles.ProfileID)
        //    {
        //        case "ADM":
        //            break;
        //        case "BAS":
        //            break;
        //        case "UPO":
        //            return perfilPo.GetVistaSuppliers();
        //        case "UPR":
        //            return perfilPr.GetVistaSuppliers();
        //        case "VAL":
        //            return perfilVal.GetVistaSuppliers();
        //    }
        //    return null;
        //}

        public iGrid CargarGrid(iGrid grid)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                // return perfilPo.CargarGrid();
                //grid = perfilPo.FactoryGrid(grid, PerfilAbstract.EtapasGrid.PintarGrid);
                //return grid;
                case "UPR":
                    perfilPr.Grid = grid;
                    perfilPr.UserProfiles = userDB.UserProfiles;
                    perfilPr.PintarGrid();
                    perfilPr.CargarColumnas();
                    return perfilPr.Grid;

                    // return perfilPr.FactoryGrid(grid, PerfilAbstract.EtapasGrid.PintarGrid);
                    // case "VAL":
                    //return perfilVal.FactoryGrid(grid, PerfilAbstract.EtapasGrid.PintarGrid);
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
                    break;
                case "UPR":
                    perfilPr.Formatear();
                    return perfilPr.Grid;

                case "VAL":
                    break;
            }
            return null;
        }

        public void InsertItem(Companies company, PerfilAbstract.OrderType type)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    perfilPo.InsertOrderHeader(company, type, userDB);
                    break;
                case "UPR":
                    perfilPr.InsertRequisition(company, type, userDB);
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
                            if (Convert.ToInt32(dataRow["StatusID"]) < 7)// Puede modificar hasta 6 LAS PROPIAS
                            {
                                perfilPo.UpdateOrderHeader(userDB,
                                    idHeader,
                                    nuevovalor,
                                    campo);
                                return true;
                            }
                        }
                    }
                    else if (campo == "StatusID")
                    {
                        var val = Convert.ToInt32(nuevovalor);
                        if (userDB.UserID == dataRow["UserID"].ToString()) // Es de PO
                        {
                            if (val >= 4 && val <= 9) // de 3 a 9
                            {
                                if (ValidarOrderHeader(dataRow))
                                {
                                    perfilPo.UpdateOrderHeader(userDB,
                                        idHeader,
                                        nuevovalor,
                                        campo);
                                    return true;
                                }
                            }
                            else if (val == 3)
                            {
                                perfilPo.UpdateOrderHeader(userDB, idHeader, nuevovalor, campo);
                                return true;
                            }
                        }
                        else
                        {
                            // No es de PO
                            if (val >= 4) // Valido solo si paso Active Y más
                            {
                                if (ValidarOrderHeader(dataRow))
                                {
                                    perfilPo.UpdateOrderHeader(userDB, idHeader, nuevovalor, campo);
                                    return true;
                                }
                            }
                            else
                            {
                                perfilPo.UpdateOrderHeader(userDB, idHeader, nuevovalor, campo);
                                return true;
                            }
                        }
                    }
                    break;
                case "UPR":
                    idHeader = Convert.ToInt32(dataRow["RequisitionHeaderID"]);
                    if (campo == "Description" && nuevovalor != null)
                    {
                        //! No UPDATE en estado 2
                        if (Convert.ToInt32(dataRow["StatusID"]) == 1)
                        {
                            perfilPr.UpdateRequisitionHeader(userDB, idHeader, nuevovalor, campo);
                            return true;
                        }
                    }
                    else if (campo == "Type" || campo == "StatusID")
                    {
                        if (ValidarOrderHeader(dataRow))
                        {
                            perfilPr.UpdateRequisitionHeader(userDB, idHeader, nuevovalor, campo);
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
                    idHeader = Convert.ToInt32(dataRow["RequisitionHeaderID"]);
                    if (Convert.ToInt32(dataRow["StatusID"]) == 1) // Puede eliminar sólo en 1
                    {
                        perfilPr.DeleteRequesitionHeader(idHeader);
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool DeleteOrderDetail(OrderHeader orderHeader, int idDetail)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    if (orderHeader.StatusID < 6)
                    {
                        perfilPo.DeleteOrderDetail(orderHeader, idDetail, userDB);
                        return true;
                    }

                    break;
                case "UPR":
                    if (orderHeader.StatusID < 6)
                    {
                        perfilPr.DeleteOrderDetail(orderHeader, idDetail, userDB);
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }

            return false;
        }

        public bool EditarSupplier(FSupplier fSupplier)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    fSupplier.StartPosition = FormStartPosition.CenterScreen;
                    fSupplier.ShowDialog();
                    break;
                case "UPR":
                    break;
                case "VAL":
                    break;
            }
            return false;
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
                    fDetails.OrderHeaderID = Convert.ToInt32(dataRow["OrderHeaderID"]);
                    fDetails.StartPosition = FormStartPosition.CenterScreen;
                    fDetails.ShowDialog();
                    break;
                case "UPR":
                    fDetails.OrderHeaderID = Convert.ToInt32(dataRow["OrderHeaderID"]);
                    fDetails.StartPosition = FormStartPosition.CenterScreen;
                    fDetails.ShowDialog();
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        public bool EditarAttach(FOrderAttach fAttach)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    fAttach.StartPosition = FormStartPosition.CenterScreen;
                    fAttach.ShowDialog();
                    break;
                case "UPR":
                    fAttach.StartPosition = FormStartPosition.CenterScreen;
                    fAttach.ShowDialog();
                    break;
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

                    break;
                case "UPR":
                    //var path = new HtmlToPdf().ConvertHtmlToPdf(content, dataRow["RequisitionHeaderID"].ToString());
                    var details = perfilPr.GetRequisitionDetails(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    var attaches = perfilPr.GetAttaches(Convert.ToInt32(dataRow["RequisitionHeaderID"]));
                    return new HtmlToPdf().ReemplazarDatos(dataRow, userDB, details, attaches);
                case "VAL":
                    break;
            }
            return null;
        }

        public void CargarDashBoard(BunifuPolarAreaChart chart1,
             BunifuPolarAreaChart chart2,
             BunifuChartCanvas chartCanvas1)
        {
            List<string> labels = new List<string>();

            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    break;
                case "UPR":
                    perfilPr.CargarDashBoard();
                    var res1 = perfilPr.ReqGroupByCost_Results;
                    var total1 = res1.Sum(c => c.Nro);
                    foreach (var item in res1)
                    {
                        chart1.Data.Add(Convert.ToDouble((item.Nro * 100) / total1));
                        labels.Add(item.Description);
                    }
                    var res2 = perfilPr.OrderGroupByStatus_Results;
                    var total2 = res2.Sum(c => c.Nro);
                    foreach (var item in res2)
                    {
                        chart2.Data.Add(Convert.ToDouble((item.Nro * 100) / total2));


                    }

                    chartCanvas1.Labels = labels.ToArray();


                    break;
                case "VAL":
                    break;
            }
        }
    }
}
