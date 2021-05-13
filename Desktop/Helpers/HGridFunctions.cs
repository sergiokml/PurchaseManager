﻿using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Helpers
{
    public partial class HFunctions
    {
        public HFunctions()
        {
            //! Esto se carga por herencia, el grid es NULL aquí.
        }
        public iGrid Grid { get; set; }
        // public UserProfiles UserProfiles { get; set; }
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
            lista.Images.Add(Properties.Resources.icons8_html_filetype); // Ver Html 
            lista.Images.Add(Properties.Resources.icons8_erase); // Remove
            lista.Images.Add(Properties.Resources.icons8_envelope); // Send
            lista.Images.Add(Properties.Resources.icons8_pdf); // Pdf
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
            Grid.DefaultRow.Height = Grid.GetPreferredRowHeight(false, true);
            //Grid.DefaultRow.Height = 22;
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




        }

        public void Formatear()
        {
            if (User.ProfileID == "UPR")
            {
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    var date = Convert.ToDateTime(Grid.Rows[i].Cells["DateLast"].Value).ToString("dd-MM-yyyy");
                    Grid.Rows[i].Cells["DateLast"].Value = date;

                }
            }
            else if (User.ProfileID == "UPO")
            {
                //! Order State
                iGDropDownList cboStates = new iGDropDownList();
                //DataTable tablePr;
                //tablePr = new DataTable();
                //tablePr.Columns.Add("Id");
                //tablePr.Columns.Add("Name");
                //foreach (var item in new OrderStatus().GetList())
                //{
                //    DataRow row = tablePr.NewRow();
                //    row[0] = (int)item.StatuID;
                //    row[1] = item.Description.ToString();
                //    tablePr.Rows.Add(row);
                //}
                //cboStates.FillWithData(tablePr, "Id", "Name");

                foreach (var item in new OrderStatus().GetList())
                {
                    cboStates.Items.Add(item.Description, item.Description.ToString());
                }

                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    var date = Convert.ToDateTime(Grid.Rows[i].Cells["DateLast"].Value).ToString("dd-MM-yyyy");
                    Grid.Rows[i].Cells["DateLast"].Value = date;

                    decimal net = Convert.ToDecimal(Grid.Rows[i].Cells["Net"].Value);
                    if (net > 0)
                    {
                        Grid.Rows[i].Cells["Net"].Value = net.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    }
                    else
                    {
                        Grid.Rows[i].Cells["Net"].Value = "";
                    }
                    decimal exent = Convert.ToDecimal(Grid.Rows[i].Cells["Exent"].Value);
                    if (exent > 0)
                    {
                        Grid.Rows[i].Cells["Exent"].Value = exent.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL")); ;
                    }
                    else
                    {
                        Grid.Rows[i].Cells["Exent"].Value = "";
                    }
                    decimal tax = Convert.ToDecimal(Grid.Rows[i].Cells["Tax"].Value);
                    if (tax > 0)
                    {
                        Grid.Rows[i].Cells["Tax"].Value = tax.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    }
                    else
                    {
                        Grid.Rows[i].Cells["Tax"].Value = "";
                    }
                    decimal total = Convert.ToDecimal(Grid.Rows[i].Cells["Total"].Value);
                    if (total > 0)
                    {
                        Grid.Rows[i].Cells["Total"].Value = total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                    }
                    else
                    {
                        Grid.Rows[i].Cells["Total"].Value = "";
                    }

                    var tipo = Grid.Rows[i].Cells["TypeDocumentHeader"].Value.ToString();
                    if (tipo == "PR")
                    {
                        //Grid.Rows[i].Cells["StatusID"].DropDownControl = cboStates;
                        //Grid.Rows[i].Cells["StatusID"].TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    }
                    else if (tipo == "PO")
                    {
                        Grid.Rows[i].Cells["StatusID"].DropDownControl = cboStates;
                        Grid.Rows[i].Cells["StatusID"].TypeFlags |= iGCellTypeFlags.NoTextEdit;

                    }
                }
            }

            //iGCellStyle myCellStyle = Grid.Cols["Total"].CellStyle;
            //myCellStyle.TextAlign = iGContentAlignment.TopCenter;
            //myCellStyle.FormatString = "{0:$#,##0}";
            //myCellStyle.Font = new Font("Arial", 8, FontStyle.Italic);
            //myCellStyle.ForeColor = Color.Blue;
            //iGCellStyle myCellStyle = new iGCellStyle
            //{
            //    //0:#,0.00
            //    FormatString = "{0:#,0.00}"
            //};
            //Grid.Cols["Tax"].CellStyle = myCellStyle;
            //Grid.Cols["Total"].CellStyle = myCellStyle;


            //Grid.Cols["Net"].CellStyle.FormatString = "{0:#,0.00}";
            //iGCellStyle style = Grid.Cols["DateLast"].CellStyle;
            //style.FormatString = "{0:dd-MM-yyyy}";
            //style.ForeColor = Color.Red;
        }

        public void CargarColumnasFPrincipal()
        {
            iGCol iGCol;
            iGDropDownList cboStates = new iGDropDownList();
            iGDropDownList cbotype = new iGDropDownList();
            DataTable tablePr;


            switch (User.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    ////! Order State                    
                    //tablePr = new DataTable();
                    //tablePr.Columns.Add("Id");
                    //tablePr.Columns.Add("Name");
                    //foreach (var item in new OrderStatus().GetList())
                    //{
                    //    DataRow row = tablePr.NewRow();
                    //    row[0] = (int)item.StatuID;
                    //    row[1] = item.Description.ToString();
                    //    tablePr.Rows.Add(row);
                    //}
                    //cboStates.FillWithData(tablePr, "Id", "Name");
                    //! Order Type  
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
                    iGCol = Grid.Cols.Add("OrderHeaderID", "ID", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Code", "Code", 53);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Description", "Description", 196);
                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("send", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("CompanyID", "Company", 58);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    //iGCol = Grid.Cols.Add("CompanyName", "Company Name", 175);
                    //iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Type", "Type", 93);
                    iGCol.CellStyle.DropDownControl = cbotype;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol = Grid.Cols.Add("StatusID", "Status", 111);
                    // iGCol.CellStyle.DropDownControl = cboStates;
                    // iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol = Grid.Cols.Add("SupplierID", "Supplier", 58);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("Net", "Net", 55);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);
                    iGCol = Grid.Cols.Add("Exent", "Exent", 55);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);

                    iGCol = Grid.Cols.Add("Tax", "Tax", 55);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);
                    iGCol = Grid.Cols.Add("Total", "Total", 64);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);

                    iGCol = Grid.Cols.Add("UserID", "User ID", 58);
                    //iGCol.CellStyle.FormatString = "{0:d}";
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("CostID", "CC", 30);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("DateLast", "Creation", 66);
                    //iGCol.CellStyle.FormatString = "{0:d}";
                    //iGCol.CellStyle.ValueType = typeof(DateTime);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    iGCol = Grid.Cols.Add("TypeDocumentHeader", "TD", 22);
                    //iGCol.Visible = false;

                    break;
                case "UPR":
                    //! Order State                   
                    tablePr = new DataTable();
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
                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("send", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
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
                    iGCol = Grid.Cols.Add("CostID", "CC", 30);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("DateLast", "Creation", 66);
                    //iGCol.CellStyle.FormatString = "{0:d}";
                    //iGCol.CellStyle.ValueType = typeof(DateTime);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    break;
            }
            //! Header
            foreach (iGColHdr item in Grid.Header.Cells)
            {
                item.TextAlign = iGContentAlignment.MiddleCenter;
            }
            //! Cols
            foreach (iGCol item in Grid.Cols)
            {
                item.AllowSizing = false;
            }
        }

        public void CargarColumnasFDetail()
        {
            iGCol iGCol;
            switch (User.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    //! Cols     
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("nro", "N°", 21);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("DetailID", "");
                    iGCol.Visible = false;

                    iGCol = Grid.Cols.Add("RequisitionHeaderID", "");
                    iGCol.Visible = false;

                    iGCol = Grid.Cols.Add("Qty", "Qty", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("NameProduct", "Product", 286);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    //iGCol = Grid.Cols.Add("DescriptionProduct", "Description", 175);
                    //iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("AccountID", "Account", 106);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    break;
                case "UPR":
                    //! Cols     
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("nro", "N°", 21);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("DetailID", "");
                    iGCol.Visible = false;

                    iGCol = Grid.Cols.Add("RequisitionHeaderID", "");
                    iGCol.Visible = false;

                    iGCol = Grid.Cols.Add("Qty", "Qty", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("NameProduct", "Product", 286);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    //iGCol = Grid.Cols.Add("DescriptionProduct", "Description", 175);
                    //iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("AccountID", "Account", 106);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    //! Header
                    foreach (iGColHdr item in Grid.Header.Cells)
                    {
                        item.TextAlign = iGContentAlignment.MiddleCenter;
                    }

                    foreach (iGCol item in Grid.Cols)
                    {
                        item.AllowSizing = false;
                    }

                    break;
            }
        }

        public void CargarColumnasFAttach()
        {
            iGCol iGCol;
            switch (User.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    break;
                case "UPR":
                    //! Cols     
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("nro", "N°", 21);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("AttachID", "");
                    iGCol.Visible = false;

                    iGCol = Grid.Cols.Add("Description", "Description", 315);
                    iGCol.CellStyle.ReadOnly = iGBool.True;


                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    //! Header
                    foreach (iGColHdr item in Grid.Header.Cells)
                    {
                        item.TextAlign = iGContentAlignment.MiddleCenter;
                    }
                    foreach (iGCol item in Grid.Cols)
                    {
                        item.AllowSizing = false;
                    }
                    break;
            }
        }
        public void CargarColumnasFSupplier()
        {
            iGCol iGCol;
            switch (User.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    break;
                case "UPR":
                    //! Cols     
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("nro", "N°", 21);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("SupplierID", "RUT");
                    iGCol.Visible = false;

                    iGCol = Grid.Cols.Add("Name", "Name", 315);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;

                    //! Header
                    foreach (iGColHdr item in Grid.Header.Cells)
                    {
                        item.TextAlign = iGContentAlignment.MiddleCenter;
                    }
                    foreach (iGCol item in Grid.Cols)
                    {
                        item.AllowSizing = false;
                    }
                    break;
            }

        }


    }
}
