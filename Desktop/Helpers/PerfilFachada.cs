using System;
using System.Collections.Generic;
using System.Data;

using PurchaseData.DataModel;

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
        public DataTable GetVista(iGrid grid) //? PODRIA HACER VISTAS AHRA POR PERFILES!!!?
        {
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

        public iGrid CargarBefore(iGrid grid, List<OrderStatus> status)
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
                    return perfilPo.SetGridBeging(grid, status);
                case "UPR":
                    status.RemoveRange(2, status.Count - 2); // remove 3PRE ORDER A AL FINAL
                    return perfilPr.SetGridBeging(grid, status);
                case "VAL":
                    status.RemoveRange(0, 3); // remove 1PRE ORDER A 3PRE ORDER
                    status.RemoveRange(2, status.Count - 2); // remove 
                    return perfilPr.SetGridBeging(grid, status);
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

        public bool UpdateOrderHeader(object valor, DataRow dataRow, string campo)
        {
            //todo PRIMERO DETECTAR QUE FILA FUE LA DEL CAMBIO!!!!! ACÁ SE PASA EL UPDATE DE CUALQUIER COLUMA DEL GRID Y DE CUALQUIER USUARIO.!            
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    // todo este usuario no puede modificar el estatus de una que no hizo el.
                    if (campo == "Type" || campo == "Description")
                    {
                        if (userDB.UserID == dataRow["UserID"].ToString())
                        {
                            if (Convert.ToInt32(dataRow["StatusID"]) < 7)// Puede modificar hasta 6
                            {
                                perfilPo.UpdateOrderHeader(userDB, Convert.ToInt32(dataRow["OrderHeaderID"]), valor, campo);
                                return true;
                            }
                        }
                    }
                    return false;
                case "UPR":
                    if (Convert.ToInt32(dataRow["StatusID"]) < 3) // Puede modificar hasta 2
                    {
                        perfilPr.UpdateOrderHeader(userDB, Convert.ToInt32(dataRow["OrderHeaderID"]), valor, campo);
                    }
                    break;
                case "VAL":
                    break;
            }
            return false;
        }


        public bool DeleteOrderHeader(DataRow dataRow)
        {
            switch (userDB.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    if (Convert.ToInt32(dataRow["StatusID"]) == 3)// Puede eliminar en 3 (de momento, podría elliminar en cualquier fase con advertencias)
                    {
                        perfilPo.DeleteOrderHeader(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        return true;
                    }

                    break;
                case "UPR":
                    if (Convert.ToInt32(dataRow["StatusID"]) == 1) // Puede eliminar sólo en 1
                    {
                        perfilPr.DeleteOrderHeader(Convert.ToInt32(dataRow["OrderHeaderID"]));
                        return true;
                    }
                    break;
                case "VAL":
                    break;
            }
            return false;
        }

        #endregion
    }
}
