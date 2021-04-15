using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Helpers
{
    public class PerfilAbstract
    {
        public PerfilAbstract()
        {
            //! Esto se carga por herencia, el grid es NULL aquí.
        }
        public iGrid Grid { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public enum OrderType
        {
            Materiales = 1,
            Servicios = 2,
            SubContratos = 3
        }

        private ImageList ListaImagenes()
        {
            ImageList lista = new ImageList();
            lista.Images.Add(Properties.Resources.icons8_clear_symbol);
            //lista.Images.Add(Properties.Resources.icons8_delete_view); // Grid Principal
            //lista.Images.Add(Properties.Resources.icons8_delete_file); // Grid Details
            return lista;
        }


        public iGrid FactoryGrid(iGrid igrid, EtapasGrid etapas, List<OrderStatus> status = null)
        {

            Grid = igrid;
            switch (etapas)
            {
                case EtapasGrid.PintarGrid:
                    //! General
                    Grid.BackColor = Color.FromArgb(34, 34, 34);
                    Grid.HScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
                    Grid.Font = new Font(new FontFamily("Tahoma"), 8.25f);
                    Grid.ForeColor = Color.White; // texto en general
                    Grid.RowMode = true;
                    Grid.ImageList = ListaImagenes();
                    Grid.BorderStyle = iGBorderStyle.None;
                    //Grid.EllipsisButtonGlyph = Grid.ImageList.Images[0];
                    //grid.DefaultRow.Height = 21;

                    // Now we can use this call to get the optimal height for the rows with buttons.
                    Grid.DefaultRow.Height = Grid.GetPreferredRowHeight(true, false);

                    //! Lineas
                    iGPenStyle lineasStyle = new iGPenStyle
                    {
                        Color = Color.FromArgb(45, 45, 48), // lineas del grid
                        Width = 2,
                        DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
                    };
                    Grid.GridLines.Horizontal = lineasStyle;
                    Grid.GridLines.HorizontalLastRow = lineasStyle;
                    Grid.GridLines.Vertical = lineasStyle;
                    Grid.GridLines.VerticalLastCol = lineasStyle;

                    //! Celda seleccionada
                    Grid.FocusRectColor1 = Color.FromArgb(45, 45, 48);
                    Grid.FocusRectColor2 = Color.FromArgb(45, 45, 48); // evita la linea punteada que bordea la celda
                                                                       //! Fila seleccionada focus
                    Grid.SelRowsBackColor = Color.FromArgb(154, 196, 85); // verde            
                    Grid.SelRowsForeColor = Color.Black;
                    //! Fila seleccionada no focus
                    Grid.SelRowsBackColorNoFocus = Color.FromArgb(154, 196, 85);
                    //! Group row
                    Grid.GroupBox.BackColor = Color.FromArgb(45, 45, 48);
                    Grid.GroupBox.HintBackColor = Color.FromArgb(37, 37, 38);

                    //! Header        
                    Grid.Header.ControlsForeColor = Color.Red;
                    Grid.Header.SortInfoColor = Color.Red;
                    Grid.Header.Appearance = iGControlPaintAppearance.StyleFlat;
                    Grid.Header.ForeColor = Color.White;
                    Grid.Header.UseXPStyles = false;
                    Grid.Header.HGridLinesStyle = lineasStyle;
                    Grid.Header.VGridLinesStyle = lineasStyle;
                    Grid.Header.SeparatingLine = lineasStyle;
                    Grid.Header.BackColor = Color.FromArgb(37, 37, 38);
                    Grid.Header.HotTrackForeColor = Color.FromArgb(154, 196, 85); // verde al seleccionar el header
                                                                                  //! Columnas         
                    for (int i = 0; i < Grid.Cols.Count; i++)
                    {
                        //grid.Cols[i].CellStyle.TextAlign = iGContentAlignment.BottomLeft;
                    }

                    break;
                case EtapasGrid.ControlesGris:
                    //! Order State
                    iGDropDownList cboStates = new iGDropDownList();
                    DataTable tablePr = new DataTable();
                    tablePr.Columns.Add("Id");
                    tablePr.Columns.Add("Name");
                    foreach (var item in status)
                    {
                        DataRow row = tablePr.NewRow();
                        row[0] = (int)item.StatuID;
                        row[1] = item.Description.ToString();
                        tablePr.Rows.Add(row);
                    }
                    cboStates.FillWithData(tablePr, "Id", "Name");
                    Grid.Cols["StatusID"].CellStyle.DropDownControl = cboStates;

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
                    Grid.Cols["Type"].CellStyle.DropDownControl = cbotype;
                    break;
                case EtapasGrid.LLenarGrid:
                    break;
                case EtapasGrid.DecorarGrid:
                    break;
            }
            return Grid;
        }
        public enum EtapasGrid
        {

            PintarGrid = 1,     // Primera vez
            ControlesGris = 2,  // Llenar controles
            LLenarGrid = 3,     // LLenat gris con datos
            DecorarGrid = 4     // Otros
        }
    }
}
