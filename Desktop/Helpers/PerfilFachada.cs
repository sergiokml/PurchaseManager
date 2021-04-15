using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using PurchaseData.DataModel;

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

        private readonly OrderUsers userDB;

        public PerfilFachada(PerfilUPR perfilPr, PerfilUPO perfilPo, PerfilVAL perfilVal, OrderUsers userDB)
        {
            this.perfilPr = perfilPr;
            this.perfilPo = perfilPo;
            this.perfilVal = perfilVal;
            this.userDB = userDB;
        }

        #region Functions
        public DataTable GetVistaPrincipal(iGrid grid)
        {
            if (grid is null)
            {
                throw new ArgumentNullException(nameof(grid));
            }
            //ADM   Admin user
            //BAS   Basic
            //UPO   PO user
            //UPR   PR user
            //VAL   Validator  
            //! Los parámetros pasados son la única vía para llegar el User, status, etc.
            //! Acá va la lpogica de definir qupe hacer según los perfiles.
            // todo LA LPOGICA DE SEPARAR LAS VISTA SES ACÁ. NO INICIALIZE OBJETOS ANTES.
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

        public string MsgTest()
        {
            return userDB.ProfileID;
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

        public iGrid PintarGrid(iGrid grid)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    return perfilPo.FactoryGrid(grid, PerfilAbstract.EtapasGrid.PintarGrid);
                case "UPR":
                    return perfilPr.FactoryGrid(grid, PerfilAbstract.EtapasGrid.PintarGrid);
                case "VAL":
                    return perfilVal.FactoryGrid(grid, PerfilAbstract.EtapasGrid.PintarGrid);
            }
            return null;
        }

        public iGrid ControlesGrid(iGrid grid, List<OrderStatus> status)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    status.RemoveRange(0, 1); // remove 1PRE REQ
                    //status.RemoveRange(3,1); // Sí ve las validadas
                    return perfilPo.FactoryGrid(grid, PerfilAbstract.EtapasGrid.ControlesGris, status);
                case "UPR":
                    status.RemoveRange(2, status.Count - 2); // remove 3PRE ORDER A AL FINAL
                    return perfilPr.FactoryGrid(grid, PerfilAbstract.EtapasGrid.ControlesGris, status);
                case "VAL":
                    status.RemoveRange(0, 3); // remove 1PRE ORDER A 3PRE ORDER
                    status.RemoveRange(2, status.Count - 2); // remove 
                    return perfilVal.FactoryGrid(grid, PerfilAbstract.EtapasGrid.ControlesGris, status);
            }
            return null;
        }

        public void InsertOrderHeader(OrderCompanies company, PerfilAbstract.OrderType type)
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
                    perfilPr.InsertOrderHeader(company, type, userDB);
                    break;
                case "VAL":
                    break;
            }
        }

        public bool UpdateOrderHeader(object nuevovalor, DataRow dataRow, string campo)
        {
            var idHeader = Convert.ToInt32(dataRow["OrderHeaderID"]);
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
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
                        // No es de PO
                        {
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
                    return false;
                case "UPR":
                    if (campo == "Description" && nuevovalor != null)
                    {
                        //! No UPDATE en estado 2
                        if (Convert.ToInt32(dataRow["StatusID"]) == 1)
                        {
                            perfilPr.UpdateOrderHeader(userDB, idHeader, nuevovalor, campo);
                            return true;
                        }
                    }
                    else if (campo == "Type" || campo == "StatusID")
                    {
                        if (ValidarOrderHeader(dataRow))
                        {
                            perfilPr.UpdateOrderHeader(userDB, idHeader, nuevovalor, campo);
                            return true;
                        }
                    }
                    return false;
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

        public bool DeleteOrderHeader(DataRow dataRow)
        {
            var idHeader = Convert.ToInt32(dataRow["OrderHeaderID"]);
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    if (Convert.ToInt32(dataRow["StatusID"]) == 3)// Puede eliminar en 3 (de momento, podría elliminar en cualquier fase con advertencias)
                    {
                        perfilPo.DeleteOrderHeader(idHeader);
                        return true;
                    }

                    break;
                case "UPR":
                    if (Convert.ToInt32(dataRow["StatusID"]) == 1) // Puede eliminar sólo en 1
                    {
                        perfilPr.DeleteOrderHeader(idHeader);
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
        #endregion
    }
}
