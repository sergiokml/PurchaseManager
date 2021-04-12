using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using PurchaseCtrl.DataBase.DataAccess;
using PurchaseCtrl.Desktop.Interfaces;
using PurchaseCtrl.Desktop.Utils;

using TenTec.Windows.iGridLib;

namespace PurchaseCtrl.Desktop.Profiles
{
    public class PerfilUPR : PerfilAbstract, IPerfilActions
    {
        private readonly PurchaseCtrlEntities rContext;

        public PerfilUPR(PurchaseCtrlEntities rContext)
        {
            this.rContext = rContext;
        }
        //todo ACTIONS PERFIL PR
        public DataTable GetVista(UserDB userDB)
        {
            //  1   Pre PRequisition
            //  2   Active PRequisition
            //  3   Pre POrden
            //  4   Active POrder
            //  5   Valid POrder
            //  6   POrder on Supplier
            //  7   Agree by Supplier
            //  8   POrder in Process
            //  9   POrder Complete 
            //rContext.Entry(rContext.ViewOderByActions).Reload();
            //var algo = rContext.ViewOderByActions.Where(c => c.User_ID == userDB.UserDBID);
            //using (var con = new PurchaseCtrlEntities())
            //{
            var l = rContext.ViewOderByActions
                   .Where(c => c.User_ID == userDB.UserDBID && c.Status_ID < 3)
                   .OrderByDescending(c => c.Date_Last).ToList();
            return this.ToDataTable<ViewOderByAction>(l);
            // }

        }

        public iGrid SetGridBeging(iGrid grid, List<OrderStatu> status)
        {
            CargarBefore(grid, status);
            return Grid;
        }

        public void GuardarCambios(int wait = 0)
        {
            // TODO ESTO ELIMINARLO? PUEDE ESTAR EN LA CLASE GridCustom, PERO HABRÁ DIFERENVCIA ENTRE GUARDAR CAMBIOS PO VS PR?
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(wait);
                rContext.SaveChanges();
                Cursor.Current = Cursors.Default;
            }
            catch (DbEntityValidationException e)
            {
                foreach (DbEntityValidationResult eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void InsertOrderHeader(Company company, OrderType type, UserDB userDB)
        {
            var pr = new OrderHeader
            {
                IDCompany = company.CompanyID,
                OrderType = (byte)type,
                IDOrderStatus = 1,   //  1   Pre PRequisition
                OrderDescription = string.Empty
            };
            ;
            pr.TranHistories.Add(InsertTranHistory(pr, "CREATE_PR", userDB));
            rContext.OrderHeaders.Add(pr);
            GuardarCambios();
        }

        public TranHistory InsertTranHistory(OrderHeader order, string evento, UserDB userDB)
        {
            var tran = new TranHistory
            {
                TranHistoyDescription = evento,
                IDUserDB = userDB.UserDBID,
                IDOrderStatus = order.IDOrderStatus,
                TranHistoyDate = DateTime.Now
            };
            return tran;
        }

        public void UpdateOrderHeader(UserDB userDB, int id, object valor, string campo)
        {
            var pr = rContext.OrderHeaders.Find(id);
            pr.TranHistories.Add(InsertTranHistory(pr, "UPDATE_PR", userDB));
            switch (campo)
            {
                case "Order_Description":
                    pr.OrderDescription = UCase.ToTitleCase(valor.ToString().ToLower());
                    GuardarCambios();
                    break;
                case "Order_Type":
                    pr.OrderType = Convert.ToByte(valor);
                    GuardarCambios();
                    break;
                case "Status_Id":
                    pr.IDOrderStatus = Convert.ToByte(valor);
                    GuardarCambios();
                    break;
                case "IDCompany":
                    pr.IDCompany = valor.ToString();
                    GuardarCambios();
                    break;
                case "IDCurrency":
                    pr.IDCurrency = valor.ToString();
                    GuardarCambios();
                    break;
                case "IDSupplier":
                    pr.IDSupplier = valor.ToString();
                    GuardarCambios();
                    break;
                default:
                    break;
            }


        }
    }

    //public abstract class PerfilAbstract
    //{
    //    public PerfilAbstract()
    //    {
    //        //! Esto se carga por herencia, el grid es NULL aquí.
    //    }
    //    public iGrid Grid { get; set; }
    //    public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

    //    public void CargarBefore(iGrid grid, List<OrderStatu> status)
    //    {
    //        //! General
    //        grid.BackColor = Color.FromArgb(34, 34, 34);
    //        grid.HScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
    //        grid.Font = new Font(new FontFamily("Tahoma"), 8.25f);
    //        grid.ForeColor = Color.White; // texto en general
    //        grid.RowMode = true;
    //        grid.ImageList = ListaImagenes();
    //        grid.BorderStyle = iGBorderStyle.None;
    //        //grid.DefaultRow.Height = 21;

    //        // Now we can use this call to get the optimal height for the rows with buttons.
    //        grid.DefaultRow.Height = grid.GetPreferredRowHeight(true, false);

    //        //! Lineas
    //        iGPenStyle lineasStyle = new iGPenStyle
    //        {
    //            Color = Color.FromArgb(45, 45, 48), // lineas del grid
    //            Width = 2,
    //            DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
    //        };
    //        grid.GridLines.Horizontal = lineasStyle;
    //        grid.GridLines.HorizontalLastRow = lineasStyle;
    //        grid.GridLines.Vertical = lineasStyle;
    //        grid.GridLines.VerticalLastCol = lineasStyle;

    //        //! Celda seleccionada
    //        grid.FocusRectColor1 = Color.FromArgb(45, 45, 48);
    //        grid.FocusRectColor2 = Color.FromArgb(45, 45, 48); // evita la linea punteada que bordea la celda
    //        //! Fila seleccionada focus
    //        grid.SelRowsBackColor = Color.FromArgb(154, 196, 85); // verde            
    //        grid.SelRowsForeColor = Color.Black;
    //        //! Fila seleccionada no focus
    //        grid.SelRowsBackColorNoFocus = Color.FromArgb(154, 196, 85);
    //        //! Group row
    //        grid.GroupBox.BackColor = Color.FromArgb(45, 45, 48);
    //        grid.GroupBox.HintBackColor = Color.FromArgb(37, 37, 38);

    //        //! Header        
    //        grid.Header.ControlsForeColor = Color.Red;
    //        grid.Header.SortInfoColor = Color.Red;
    //        grid.Header.Appearance = iGControlPaintAppearance.StyleFlat;
    //        grid.Header.ForeColor = Color.White;
    //        grid.Header.UseXPStyles = false;
    //        grid.Header.HGridLinesStyle = lineasStyle;
    //        grid.Header.VGridLinesStyle = lineasStyle;
    //        grid.Header.SeparatingLine = lineasStyle;
    //        grid.Header.BackColor = Color.FromArgb(37, 37, 38);
    //        grid.Header.HotTrackForeColor = Color.FromArgb(154, 196, 85); // verde al seleccionar el header
    //        //! Columnas         
    //        for (int i = 0; i < grid.Cols.Count; i++)
    //        {
    //            grid.Cols[i].CellStyle.TextAlign = iGContentAlignment.BottomLeft;
    //        }

    //        //! Order State
    //        iGDropDownList cboStates = new iGDropDownList();
    //        DataTable tablePr = new DataTable();
    //        tablePr.Columns.Add("Id");
    //        tablePr.Columns.Add("Name");
    //        foreach (var item in status)
    //        {
    //            DataRow row = tablePr.NewRow();
    //            row[0] = (int)item.OrderStatusID;
    //            row[1] = item.OrderStatusDescription.ToString();
    //            tablePr.Rows.Add(row);
    //            //    cboStates.
    //        }
    //        cboStates.FillWithData(tablePr, "Id", "Name");
    //        grid.Cols["Status_ID"].CellStyle.DropDownControl = cboStates;

    //        //! Order Type            
    //        iGDropDownList cbotype = new iGDropDownList();
    //        tablePr = new DataTable();
    //        tablePr.Columns.Add("Id");
    //        tablePr.Columns.Add("Name");
    //        foreach (OrderType myType in Enum.GetValues(typeof(OrderType)))
    //        {
    //            DataRow row = tablePr.NewRow();
    //            row[0] = (int)myType;
    //            row[1] = myType.ToString();
    //            tablePr.Rows.Add(row);
    //        }
    //        cbotype.FillWithData(tablePr, "id", "Name");
    //        grid.Cols["Order_Type"].CellStyle.DropDownControl = cbotype;

    //        grid.Cols["delete"].CellStyle.TypeFlags = iGCellTypeFlags.HasEllipsisButton;
    //        grid.Cols["attach"].CellStyle.TypeFlags = iGCellTypeFlags.HasEllipsisButton;
    //        Grid = grid;
    //    }

    //    public enum OrderType
    //    {
    //        Materiales = 1,
    //        Servicios = 2,
    //        SubContratos = 3
    //    }

    //    private ImageList ListaImagenes()
    //    {
    //        ImageList lista = new ImageList();
    //        lista.Images.Add(Properties.Resources.icons8_attach);
    //        lista.Images.Add(Properties.Resources.icons8_delete_view); // Grid Principal
    //        lista.Images.Add(Properties.Resources.icons8_delete_file); // Grid Details
    //        return lista;
    //    }
    //}
}
