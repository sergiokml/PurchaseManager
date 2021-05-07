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
        public UserProfiles UserProfiles { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public List<ufnGetReqGroupByCost_Result> ReqGroupByCost_Results { get; set; }
        public List<ufnGetOrderGroupByStatus_Result> OrderGroupByStatus_Results { get; set; }

        public enum OrderType
        {
            Materiales = 1,
            Servicios = 2,
            SubContratos = 3
        }

        private ImageList ListaImagenes()
        {
            ImageList lista = new ImageList();
            lista.Images.Add(Properties.Resources.search_15px); // Ver Html 
            lista.Images.Add(Properties.Resources.delete_bin_15px); // Remove
            lista.Images.Add(Properties.Resources.secured_letter_15px); // Send
            return lista;
        }

        public void PintarGrid()
        {
            //! General
            Grid.BackColor = Color.FromArgb(37, 37, 38); // Negro de fondo
            Grid.HScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
            Grid.Font = new Font(new FontFamily("Tahoma"), 8.25f);
            Grid.ForeColor = Color.White; // texto en general
            Grid.RowMode = true;
            Grid.ImageList = ListaImagenes();
            Grid.BorderStyle = iGBorderStyle.None;
            Grid.DefaultAutoGroupRow.Expanded = false;
            //Grid.DefaultRow.Height = Grid.GetPreferredRowHeight(false, true);
            //Grid.EllipsisButtonGlyph = Grid.ImageList.Images[0];
            Grid.DefaultRow.Height = 22;
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
            Grid.GroupBox.ColHdrBorderColor = Color.FromArgb(45, 45, 48);  // Color borde arrastrar celda al group
            Grid.GridLines.GroupRows.Color = Color.FromArgb(45, 45, 48);
            Grid.GridLines.GroupRows.Width = 2;
            Grid.GroupRowLevelStyles = new iGCellStyle[1];
            Grid.GroupRowLevelStyles[0] = new iGCellStyle
            {
                BackColor = Color.FromArgb(37, 37, 38)

            };
            //Grid.GroupRowLevelStyles = new iGCellStyle[1];
            //Grid.GroupRowLevelStyles[0] = new iGCellStyle
            //{
            //    BackColor = Color.FromArgb(37, 37, 38),
            //    ForeColor = Color.Red
            //};




            //! Header        
            Grid.Header.ControlsForeColor = Color.FromArgb(154, 196, 85);
            Grid.Header.SortInfoColor = Color.FromArgb(154, 196, 85);
            Grid.Header.Appearance = iGControlPaintAppearance.StyleFlat;
            Grid.Header.ForeColor = Color.White;
            Grid.Header.UseXPStyles = false;
            Grid.Header.HGridLinesStyle = lineasStyle;
            Grid.Header.VGridLinesStyle = lineasStyle;
            Grid.Header.SeparatingLine = lineasStyle;
            Grid.Header.BackColor = Color.FromArgb(37, 37, 38);
            Grid.Header.HotTrackForeColor = Color.FromArgb(154, 196, 85); // verde al seleccionar el header


            //iGCellStyle myCellStyle = new iGCellStyle
            //{
            //    //0:#,0.00
            //    FormatString = "{0:dd MMMM yyyy}"
            //};
            //Grid.Cols["DateLast"].CellStyle = myCellStyle;
            //Grid.Cols["Tax"].CellStyle = myCellStyle;
            //Grid.Cols["Total"].CellStyle = myCellStyle;

        }

        public void Formatear()
        {

            iGCellStyle myCellStyle = new iGCellStyle
            {
                //0:#,0.00
                FormatString = "{0:d}"
            };

            Grid.Cols["DateLast"].CellStyle = myCellStyle;

        }

        public void CargarColumnas()
        {
            iGCol iGCol;
            switch (UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    break;
                case "UPR":
                    //! Order State
                    iGDropDownList cboStates = new iGDropDownList();
                    DataTable tablePr = new DataTable();
                    tablePr.Columns.Add("Id");
                    tablePr.Columns.Add("Name");
                    foreach (var item in new RequisitionStatus().GetList())
                    {
                        DataRow row = tablePr.NewRow();
                        row[0] = (int)item.StatuID;
                        row[1] = item.Description.ToString();
                        tablePr.Rows.Add(row);
                    }
                    cboStates.FillWithData(tablePr, "Id", "Name");

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

                    //! Cols
                    Grid.GroupBox.Visible = true;
                    Grid.Header.Height = 20;

                    iGCol = Grid.Cols.Add("RequisitionHeaderID", "ID", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("Code", "Code", 53);
                    iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("Description", "Description", 196);
                    //iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    //iGCol.IncludeInSelect = false;

                    iGCol = Grid.Cols.Add("send", "", 22);
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    //iGCol.IncludeInSelect = false;


                    iGCol = Grid.Cols.Add("CompanyID", "Company", 58);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("CompanyName", "Company Name", 175);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("Type", "Type", 93);
                    iGCol.CellStyle.DropDownControl = cbotype;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;

                    iGCol = Grid.Cols.Add("StatusID", "Status", 111);
                    iGCol.CellStyle.DropDownControl = cboStates;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;

                    iGCol = Grid.Cols.Add("UserID", "User ID", 58);
                    //iGCol.CellStyle.FormatString = "{0:d}";
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("CostID", "CC", 27);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("DateLast", "Creation", 70);
                    //iGCol.CellStyle.FormatString = "{0:d}";
                    iGCol.CellStyle.ValueType = typeof(DateTime);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    //! Header
                    foreach (iGColHdr item in Grid.Header.Cells)
                    {
                        item.TextAlign = iGContentAlignment.MiddleCenter;
                    }


                    break;
            }
        }


        //public iGrid FactoryGrid(iGrid igrid, EtapasGrid etapas, List<OrderStatus> status = null)
        //{

        //    Grid = igrid;
        //    switch (etapas)
        //    {
        //        case EtapasGrid.PintarGrid:
        //            //! General
        //            Grid.BackColor = Color.FromArgb(37, 37, 38);
        //            Grid.HScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
        //            Grid.Font = new Font(new FontFamily("Tahoma"), 8.25f);
        //            Grid.ForeColor = Color.White; // texto en general
        //            Grid.RowMode = true;
        //            Grid.ImageList = ListaImagenes();
        //            Grid.BorderStyle = iGBorderStyle.None;
        //            //Grid.EllipsisButtonGlyph = Grid.ImageList.Images[0];
        //            //grid.DefaultRow.Height = 21;

        //            // Now we can use this call to get the optimal height for the rows with buttons.
        //            Grid.DefaultRow.Height = Grid.GetPreferredRowHeight(true, false);

        //            //! Lineas
        //            iGPenStyle lineasStyle = new iGPenStyle
        //            {
        //                Color = Color.FromArgb(45, 45, 48), // lineas del grid
        //                Width = 2,
        //                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        //            };
        //            Grid.GridLines.Horizontal = lineasStyle;
        //            Grid.GridLines.HorizontalLastRow = lineasStyle;
        //            Grid.GridLines.Vertical = lineasStyle;
        //            Grid.GridLines.VerticalLastCol = lineasStyle;

        //            //! Celda seleccionada
        //            Grid.FocusRectColor1 = Color.FromArgb(45, 45, 48);
        //            Grid.FocusRectColor2 = Color.FromArgb(45, 45, 48); // evita la linea punteada que bordea la celda
        //                                                               //! Fila seleccionada focus
        //            Grid.SelRowsBackColor = Color.FromArgb(154, 196, 85); // verde            
        //            Grid.SelRowsForeColor = Color.Black;
        //            //! Fila seleccionada no focus
        //            Grid.SelRowsBackColorNoFocus = Color.FromArgb(154, 196, 85);
        //            //! Group row
        //            Grid.GroupBox.BackColor = Color.FromArgb(45, 45, 48);
        //            Grid.GroupBox.HintBackColor = Color.FromArgb(37, 37, 38);

        //            //! Header        
        //            Grid.Header.ControlsForeColor = Color.Red;
        //            Grid.Header.SortInfoColor = Color.Red;
        //            Grid.Header.Appearance = iGControlPaintAppearance.StyleFlat;
        //            Grid.Header.ForeColor = Color.White;
        //            Grid.Header.UseXPStyles = false;
        //            Grid.Header.HGridLinesStyle = lineasStyle;
        //            Grid.Header.VGridLinesStyle = lineasStyle;
        //            Grid.Header.SeparatingLine = lineasStyle;
        //            Grid.Header.BackColor = Color.FromArgb(37, 37, 38);
        //            Grid.Header.HotTrackForeColor = Color.FromArgb(154, 196, 85); // verde al seleccionar el header
        //                                                                          //! Columnas         
        //            for (int i = 0; i < Grid.Cols.Count; i++)
        //            {
        //                //grid.Cols[i].CellStyle.TextAlign = iGContentAlignment.BottomLeft;
        //            }

        //            iGCellStyle myCellStyle = new iGCellStyle
        //            {
        //                //0:#,0.00
        //                FormatString = "{0:#,0.0}"
        //            };
        //            Grid.Cols["Net"].CellStyle = myCellStyle;
        //            Grid.Cols["Tax"].CellStyle = myCellStyle;
        //            Grid.Cols["Total"].CellStyle = myCellStyle;


        //            break;
        //        case EtapasGrid.ControlesGris:
        //            //! Order State
        //            iGDropDownList cboStates = new iGDropDownList();
        //            DataTable tablePr = new DataTable();
        //            tablePr.Columns.Add("Id");
        //            tablePr.Columns.Add("Name");
        //            foreach (var item in status)
        //            {
        //                DataRow row = tablePr.NewRow();
        //                row[0] = (int)item.StatuID;
        //                row[1] = item.Description.ToString();
        //                tablePr.Rows.Add(row);
        //            }
        //            cboStates.FillWithData(tablePr, "Id", "Name");
        //            Grid.Cols["StatusID"].CellStyle.DropDownControl = cboStates;

        //            //! Order Type            
        //            iGDropDownList cbotype = new iGDropDownList();
        //            tablePr = new DataTable();
        //            tablePr.Columns.Add("Id");
        //            tablePr.Columns.Add("Name");
        //            foreach (OrderType myType in Enum.GetValues(typeof(OrderType)))
        //            {
        //                DataRow row = tablePr.NewRow();
        //                row[0] = (int)myType;
        //                row[1] = myType.ToString();
        //                tablePr.Rows.Add(row);
        //            }
        //            cbotype.FillWithData(tablePr, "id", "Name");
        //            Grid.Cols["Type"].CellStyle.DropDownControl = cbotype;
        //            break;
        //        case EtapasGrid.LLenarGrid:
        //            break;
        //        case EtapasGrid.DecorarGrid:
        //            break;
        //    }
        //    return Grid;
        //}

    }
}
