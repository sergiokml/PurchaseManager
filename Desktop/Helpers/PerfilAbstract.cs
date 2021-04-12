using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PurchaseCtrl.Desktop.Profiles
{
    public class PerfilAbstract
    {
        public PerfilAbstract()
        {
            //! Esto se carga por herencia, el grid es NULL aquí.
        }
        public iGrid Grid { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public void CargarBefore(iGrid grid, List<OrderStatu> status)
        {
            //! General
            grid.BackColor = Color.FromArgb(34, 34, 34);
            grid.HScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
            grid.Font = new Font(new FontFamily("Tahoma"), 8.25f);
            grid.ForeColor = Color.White; // texto en general
            grid.RowMode = true;
            grid.ImageList = ListaImagenes();
            grid.BorderStyle = iGBorderStyle.None;
            //grid.DefaultRow.Height = 21;

            // Now we can use this call to get the optimal height for the rows with buttons.
            grid.DefaultRow.Height = grid.GetPreferredRowHeight(true, false);

            //! Lineas
            iGPenStyle lineasStyle = new iGPenStyle
            {
                Color = Color.FromArgb(45, 45, 48), // lineas del grid
                Width = 2,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };
            grid.GridLines.Horizontal = lineasStyle;
            grid.GridLines.HorizontalLastRow = lineasStyle;
            grid.GridLines.Vertical = lineasStyle;
            grid.GridLines.VerticalLastCol = lineasStyle;

            //! Celda seleccionada
            grid.FocusRectColor1 = Color.FromArgb(45, 45, 48);
            grid.FocusRectColor2 = Color.FromArgb(45, 45, 48); // evita la linea punteada que bordea la celda
            //! Fila seleccionada focus
            grid.SelRowsBackColor = Color.FromArgb(154, 196, 85); // verde            
            grid.SelRowsForeColor = Color.Black;
            //! Fila seleccionada no focus
            grid.SelRowsBackColorNoFocus = Color.FromArgb(154, 196, 85);
            //! Group row
            grid.GroupBox.BackColor = Color.FromArgb(45, 45, 48);
            grid.GroupBox.HintBackColor = Color.FromArgb(37, 37, 38);

            //! Header        
            grid.Header.ControlsForeColor = Color.Red;
            grid.Header.SortInfoColor = Color.Red;
            grid.Header.Appearance = iGControlPaintAppearance.StyleFlat;
            grid.Header.ForeColor = Color.White;
            grid.Header.UseXPStyles = false;
            grid.Header.HGridLinesStyle = lineasStyle;
            grid.Header.VGridLinesStyle = lineasStyle;
            grid.Header.SeparatingLine = lineasStyle;
            grid.Header.BackColor = Color.FromArgb(37, 37, 38);
            grid.Header.HotTrackForeColor = Color.FromArgb(154, 196, 85); // verde al seleccionar el header
            //! Columnas         
            for (int i = 0; i < grid.Cols.Count; i++)
            {
                //grid.Cols[i].CellStyle.TextAlign = iGContentAlignment.BottomLeft;
            }

            //! Order State
            iGDropDownList cboStates = new iGDropDownList();
            DataTable tablePr = new DataTable();
            tablePr.Columns.Add("Id");
            tablePr.Columns.Add("Name");
            foreach (var item in status)
            {
                DataRow row = tablePr.NewRow();
                row[0] = (int)item.OrderStatusID;
                row[1] = item.OrderStatusDescription.ToString();
                tablePr.Rows.Add(row);
            }
            cboStates.FillWithData(tablePr, "Id", "Name");
            grid.Cols["Status_ID"].CellStyle.DropDownControl = cboStates;

            //! Order Type            
            iGDropDownList cbotype = new iGDropDownList();
            tablePr = new DataTable();
            tablePr.Columns.Add("Id");
            tablePr.Columns.Add("Name");
            foreach (OrderType myType in Enum.GetValues(typeof(OrderType)))
            {
                DataRow row = tablePr.NewRow();
                row[0] = (int)myType;
                row[1] = myType.ToString();
                tablePr.Rows.Add(row);
            }
            cbotype.FillWithData(tablePr, "id", "Name");
            grid.Cols["Order_Type"].CellStyle.DropDownControl = cbotype;

            Grid = grid;
        }

        public enum OrderType
        {
            Materiales = 1,
            Servicios = 2,
            SubContratos = 3
        }

        private ImageList ListaImagenes()
        {
            ImageList lista = new ImageList();
            lista.Images.Add(Properties.Resources.icons8_attach);
            lista.Images.Add(Properties.Resources.icons8_delete_view); // Grid Principal
            lista.Images.Add(Properties.Resources.icons8_delete_file); // Grid Details
            return lista;
        }
    }
}
