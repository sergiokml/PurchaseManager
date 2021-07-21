using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using PurchaseData.DataModel;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Helpers
{
    public class FuncGrid
    {
        public FuncGrid()
        {
            //! Esto se carga por herencia, el grid es NULL aquí.
        }

        public iGrid Grid { get; set; }
        public iGDropDownList ComboBox { get; set; } = new iGDropDownList();
        public ContextMenuStrip CtxMenu { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        private ImageList ListaImagenes()
        {
            ImageList lista = new ImageList();
            lista.Images.Add(Properties.Resources.icons8_html_filetype);            //0 Ver Html 
            lista.Images.Add(Properties.Resources.icons8_erase);                    //1 Remove
            lista.Images.Add(Properties.Resources.icons8_envelope);                 //2 Send
            lista.Images.Add(Properties.Resources.icons8_pdf);                      //3 Pdf
            lista.Images.Add(Properties.Resources.icons8_left_arrow);               //4 draft icon
            lista.Images.Add(Properties.Resources.icons8_us_dollar);                //5 currency icon
            lista.Images.Add(Properties.Resources.icons8_doc);                      //6 word
            lista.Images.Add(Properties.Resources.icons8_xls);                      //7 Excel
            return lista;
        }

        public void LLenarMenuContext(EPerfiles perfil, DataRow headerDR)
        {
            CtxMenu.Items.Clear();
            CtxMenu.BackColor = Color.FromArgb(37, 37, 38);
            CtxMenu.ForeColor = Color.White;
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            ToolStripItem item;
            switch (perfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID == 2)
                            {
                                item = CtxMenu.Items.Add("Convert To PO");  // index 1
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_change.ToBitmap();
                                item.BackColor = Color.FromArgb(37, 37, 38);
                                item.Name = "CONVERTREQ";
                            }
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.RequisitionHeaderID > 0)
                            {
                                item = CtxMenu.Items.Add($"Open Requisition {headerDR["RequisitionHeaderID"]}");
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_html_filetype.ToBitmap();
                                item.Name = "OPENREQ";
                            }
                            if (po.StatusID == 3)
                            {
                                item = CtxMenu.Items.Add("Send To Supplier"); // index 0
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_envelope.ToBitmap();
                                item.BackColor = Color.FromArgb(37, 37, 38);
                                item.Name = "SEND";
                            }
                            else if (po.StatusID == 4)
                            {
                                item = CtxMenu.Items.Add("Accepted By Supplier"); // index 0
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_envelope.ToBitmap();
                                item.BackColor = Color.FromArgb(37, 37, 38);
                                item.Name = "ACCEPTED";
                            }
                            else if (po.StatusID == 5)
                            {
                                item = CtxMenu.Items.Add("Complete"); // index 0
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_envelope.ToBitmap();
                                item.BackColor = Color.FromArgb(37, 37, 38);
                                item.Name = "COMPLETE";
                            }
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    //! No hay opciones para User PR
                    break;
                case EPerfiles.VAL:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.RequisitionHeaderID > 0)
                            {
                                item = CtxMenu.Items.Add($"Open Requisition {headerDR["RequisitionHeaderID"]}");
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_html_filetype.ToBitmap();
                                item.Name = "OPENREQ";
                            }
                            if (po.StatusID == 2)
                            {
                                item = CtxMenu.Items.Add("Validate PO");  // index 1
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_change.ToBitmap();
                                item.BackColor = Color.FromArgb(37, 37, 38);
                                item.Name = "VALIDATED"; //3

                                item = CtxMenu.Items.Add("Reject PO");  // index 1
                                item.Font = new Font("Tahoma", 8, FontStyle.Regular);
                                item.Image = Properties.Resources.icons8_change.ToBitmap();
                                item.BackColor = Color.FromArgb(37, 37, 38);
                                item.Name = "REJECTED"; //7
                            }
                            break;
                    }
                    break;
            }
        }

        public void LLenarCombo(DataTable table)
        {
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
            ComboBox.FillWithData(table, "StatuID", "Description");
        }

        private void PintarGrid()
        {
            //! General
            Grid.BackColor = Color.FromArgb(37, 37, 38); // Negro de fondo
            Grid.HScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
            Grid.VScrollBar.Visibility = iGScrollBarVisibility.Hide; // nunca lo muestra
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
            switch (Grid.Parent.Name)
            {
                case "FPrincipal":
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        DataRow row = (DataRow)Grid.Rows[i].Tag;
                        Grid.Rows[i].Cells["DateLast"].Value = Convert.ToDateTime(row["DateLast"]).ToString("dd-MM-yyyy");
                        //! Total , solo para PO
                        if (Grid.Cols.KeyExists("Total"))
                        {
                            decimal total = Convert.ToDecimal(row["Total"]);
                            if (total > 0)
                            {
                                Grid.Rows[i].Cells["Total"].Value = total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"));
                            }
                            else
                            {
                                Grid.Rows[i].Cells["Total"].Value = "";
                            }
                        }
                        var res = Convert.ToByte(row["StatusID"]);
                        if (res == 1)
                        {
                            Grid.Rows[i].Cells["Status"].ImageIndex = 4;
                            Grid.Rows[i].Cells["Status"].ImageAlign = iGContentAlignment.BottomRight;
                        }
                    }
                    break;
                case "FDetails":
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        decimal total = Convert.ToDecimal(Grid.Rows[i].Cells["Qty"].Value);
                        if (total > 0)
                        {
                            Grid.Rows[i].Cells["Qty"].Value = total.
                                ToString("#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                        }
                        if (Grid.Cols.KeyExists("Price") && Grid.Rows[i].Cells["Price"].Value != null)
                        {
                            decimal.TryParse(Grid.Rows[i].Cells["Price"].Value.ToString(), out decimal price);
                            if (price > 0)
                            {
                                Grid.Rows[i].Cells["Price"].Value = price.
                                    ToString("$#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                            }
                        }
                    }
                    break;
                case "FAttach":
                    break;
                case "FNotes":
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        Grid.Rows[i].Height = 40;
                    }
                    break;
                case "FDelivery":
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        DataRow row = (DataRow)Grid.Rows[i].Tag;
                        Grid.Rows[i].Cells["Date"].Value = Convert.ToDateTime(row["Date"]).ToString("dd-MM-yyyy");
                    }
                    break;
            }
        }

        public void CargarColumnasFPrincipal(EPerfiles perfil)
        {
            PintarGrid();
            iGCol iGCol;
            iGDropDownList cbotype = new iGDropDownList();
            // iGDropDownList cbocurrency = new iGDropDownList();
            DataTable tablePr;
            //! Order Type  
            tablePr = new DataTable();
            tablePr.Columns.Add("Id");
            tablePr.Columns.Add("Name");
            foreach (var myType in new TypeDocument().GetList())
            {
                DataRow row = tablePr.NewRow();
                row[0] = myType.TypeID;
                row[1] = myType.Description;
                tablePr.Rows.Add(row);
            }
            cbotype.FillWithData(tablePr, "id", "Name");

            //! Currency
            //tablePr = new DataTable();
            //tablePr.Columns.Add("Id");
            //tablePr.Columns.Add("Name");
            //foreach (var myType in new Currencies().GetList())
            //{
            //    DataRow row = tablePr.NewRow();
            //    row[0] = myType.CurrencyID;
            //    row[1] = myType.Description;
            //    tablePr.Rows.Add(row);
            //}
            //cbocurrency.FillWithData(tablePr, "id", "id");
            //Grid.DefaultCol.Width = 10;
            //Grid.Cols.Add().CellStyle.CustomDrawFlags = iGCustomDrawFlags.Foreground;
            switch (perfil)
            {
                case EPerfiles.ADM:
                    #region Cols

                    //! Cols
                    Grid.GroupBox.Visible = true;
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("HeaderID", "ID", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Code", "Code", 53);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Description", "Description", 196);
                    iGCol.CellStyle.MaxInputLength = 100;
                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("send", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("CompanyID", "Company", 58);
                    iGCol.Visible = false;
                    iGCol = Grid.Cols.Add("CompanyName", "Company Name", 175);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Type", "Type", 97);
                    iGCol.CellStyle.DropDownControl = cbotype;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol = Grid.Cols.Add("Status", "Status", 67);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("SupplierID", "Supplier", 58);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("CurrencyID", "", 52);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    //iGCol.CellStyle.DropDownControl = cbocurrency;
                    //iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol.ColHdrStyle.ImageList = ListaImagenes();
                    iGCol.ImageIndex = 5;
                    iGCol.ColHdrStyle.ImageAlign = iGContentAlignment.BottomCenter;

                    iGCol = Grid.Cols.Add("Total", "Total", 74);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);

                    iGCol = Grid.Cols.Add("NameUserID", "User ID", 103);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    iGCol = Grid.Cols.Add("CostID", "CC", 32);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("DateLast", "Creation", 66);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("TypeDocumentHeader", "TD", 22);
                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    #endregion
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:

                    #region Cols

                    //! Cols
                    Grid.GroupBox.Visible = true;
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("HeaderID", "ID", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Code", "Code", 53);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Description", "Description", 196);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.MaxInputLength = 100;
                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("send", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("CompanyID", "Company", 58);
                    iGCol.Visible = false;
                    iGCol = Grid.Cols.Add("CompanyName", "Company Name", 175);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Type", "Type", 97);
                    iGCol.CellStyle.DropDownControl = cbotype;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol = Grid.Cols.Add("Status", "Status", 67);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("SupplierID", "Supplier", 58);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("CurrencyID", "", 52);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    // iGCol.CellStyle.DropDownControl = cbocurrency;
                    //iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol.ColHdrStyle.ImageList = ListaImagenes();
                    iGCol.ImageIndex = 5;
                    iGCol.ColHdrStyle.ImageAlign = iGContentAlignment.BottomCenter;

                    iGCol = Grid.Cols.Add("Total", "Total", 74);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);

                    iGCol = Grid.Cols.Add("NameUserID", "User", 103);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    iGCol = Grid.Cols.Add("CostID", "CC", 32);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("DateLast", "Creation", 66);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("TypeDocumentHeader", "TD", 22);
                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    #endregion

                    break;
                case EPerfiles.UPR:
                    #region Cols
                    iGDropDownList cbouserPo = new iGDropDownList();
                    tablePr = new DataTable();
                    tablePr.Columns.Add("Id");
                    tablePr.Columns.Add("Name");
                    foreach (var myType in new Users().GetList().Where(c => c.ProfileID == "UPO").ToList())
                    {
                        DataRow row = tablePr.NewRow();
                        row[0] = myType.UserID;
                        row[1] = $"{myType.FirstName + " " + myType.LastName}";
                        tablePr.Rows.Add(row);
                    }
                    cbouserPo.FillWithData(tablePr, "id", "Name");
                    ;
                    //! Cols
                    Grid.GroupBox.Visible = true;
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("HeaderID", "ID", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Code", "Code", 53);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Description", "Description", 196);
                    iGCol.CellStyle.MaxInputLength = 100;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
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
                    iGCol = Grid.Cols.Add("Type", "Type", 97);
                    iGCol.CellStyle.DropDownControl = cbotype;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol = Grid.Cols.Add("Status", "Status", 67);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    //! Los usuarios PO para seleccionar
                    iGCol = Grid.Cols.Add("UserPO", "User PO", 135);
                    iGCol.CellStyle.DropDownControl = cbouserPo;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;

                    iGCol = Grid.Cols.Add("CostID", "CC", 32);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("DateLast", "Creation", 66);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("TypeDocumentHeader", "TD", 22);
                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    #endregion

                    break;
                case EPerfiles.VAL:
                    #region Cols

                    //! Cols
                    Grid.GroupBox.Visible = true;
                    Grid.Header.Height = 20;
                    iGCol = Grid.Cols.Add("HeaderID", "ID", 39);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Code", "Code", 53);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Description", "Description", 196);
                    iGCol.CellStyle.MaxInputLength = 100;
                    iGCol = Grid.Cols.Add("view", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("send", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    iGCol = Grid.Cols.Add("CompanyID", "Company", 58);
                    iGCol.Visible = false;
                    iGCol = Grid.Cols.Add("CompanyName", "Company Name", 175);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("Type", "Type", 97);
                    iGCol.CellStyle.DropDownControl = cbotype;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol = Grid.Cols.Add("Status", "Status", 67);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("SupplierID", "Supplier", 58);
                    iGCol.CellStyle.ReadOnly = iGBool.True;

                    iGCol = Grid.Cols.Add("CurrencyID", "", 52);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    //iGCol.CellStyle.DropDownControl = cbocurrency;
                    //iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;
                    iGCol.ColHdrStyle.ImageList = ListaImagenes();
                    iGCol.ImageIndex = 5;
                    iGCol.ColHdrStyle.ImageAlign = iGContentAlignment.BottomCenter;

                    iGCol = Grid.Cols.Add("Total", "Total", 74);
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.Font = new Font("Tahoma", 7);

                    iGCol = Grid.Cols.Add("NameUserID", "User PO", 103);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    iGCol = Grid.Cols.Add("CostID", "CC", 32);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("DateLast", "Creation", 66);
                    iGCol.CellStyle.ReadOnly = iGBool.True;
                    iGCol = Grid.Cols.Add("TypeDocumentHeader", "TD", 22);
                    iGCol = Grid.Cols.Add("delete", "", 22);
                    iGCol.AllowGrouping = false;
                    iGCol.IncludeInSelect = false;
                    iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
                    #endregion
                    break;
            }
            //! Header
            foreach (iGColHdr item in Grid.Header.Cells)
            {
                item.TextAlign = iGContentAlignment.MiddleCenter;
            }
        }

        public void CargarColumnasFDetail()
        {
            PintarGrid();
            iGCol iGCol;
            //! Cols     
            Grid.Header.Height = 20;
            iGCol = Grid.Cols.Add("nro", "N°", 21);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("DetailID", "");
            iGCol.Visible = false;
            iGCol = Grid.Cols.Add("HeaderID", "");
            iGCol.Visible = false;
            iGCol = Grid.Cols.Add("Qty", "Qty", 39);
            iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol.CellStyle.Font = new Font("Tahoma", 7);
            iGCol = Grid.Cols.Add("MedidaID", "M", 28);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("NameProduct", "Product", 352);
            iGCol = Grid.Cols.Add("DescriptionProduct", "");
            iGCol.Visible = false;
            iGCol = Grid.Cols.Add("Price", "Price", 64);
            iGCol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
            iGCol.CellStyle.Font = new Font("Tahoma", 7);
            iGCol = Grid.Cols.Add("AccountID", "Account", 84);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol.CellStyle.Font = new Font("Tahoma", 6.5f);
            iGCol = Grid.Cols.Add("delete", "", 22);
            iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.HasEllipsisButton;
            //! Header
            foreach (iGColHdr item in Grid.Header.Cells)
            {
                item.TextAlign = iGContentAlignment.MiddleCenter;
            }
            foreach (iGCol item in Grid.Cols)
            {
                //item.AllowSizing = false;
            }
        }

        public void CargarColumnasFAttach()
        {
            PintarGrid();
            iGCol iGCol;
            iGDropDownList cbotype = new iGDropDownList();
            DataTable tablePr;
            //! Order Type  
            tablePr = new DataTable();
            tablePr.Columns.Add("Id");
            tablePr.Columns.Add("Name");
            foreach (TypeAttach myType in Enum.GetValues(typeof(TypeAttach)))
            {
                DataRow row = tablePr.NewRow();
                row[0] = (int)myType;
                row[1] = myType.ToString();
                tablePr.Rows.Add(row);
            }
            cbotype.FillWithData(tablePr, "id", "Name");

            //! Cols     
            Grid.Header.Height = 20;
            iGCol = Grid.Cols.Add("nro", "N°", 21);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("AttachID", "");
            iGCol.Visible = false;
            iGCol = Grid.Cols.Add("Description", "Description", 219);
            iGCol.CellStyle.MaxInputLength = 20;
            iGCol = Grid.Cols.Add("Extension", "Ext", 35);
            iGCol.CellStyle.ReadOnly = iGBool.True;

            iGCol = Grid.Cols.Add("Modifier", "Modifier", 60);
            iGCol.CellStyle.DropDownControl = cbotype;
            iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;

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
                // item.AllowSizing = false;
            }
        }

        public void CargarColumnasFSupplier()
        {
            PintarGrid();
            iGCol iGCol;
            //! Cols     
            Grid.Header.Height = 20;
            iGCol = Grid.Cols.Add("nro", "N°", 32);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("SupplierID", "RUT", 58);
            iGCol = Grid.Cols.Add("Name", "Name", 387);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("CountryID", "", 22);
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
        }

        public void CargarColumnasFHitos()
        {
            PintarGrid();
            iGCol iGCol;
            //! Cols     
            Grid.Header.Height = 20;
            iGCol = Grid.Cols.Add("nro", "N°", 58);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("Description", "Description", 245);
            iGCol = Grid.Cols.Add("Porcent", "%", 27);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("Days", "D°", 27);
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
                //item.AllowSizing = false;
            }
        }

        public void CargarColumnasFNotes()
        {
            PintarGrid();
            iGCol iGCol;
            iGDropDownList cbotype = new iGDropDownList();
            DataTable tablePr;
            //! Order Type  
            tablePr = new DataTable();
            tablePr.Columns.Add("Id");
            tablePr.Columns.Add("Name");
            foreach (TypeAttach myType in Enum.GetValues(typeof(TypeAttach)))
            {
                DataRow row = tablePr.NewRow();
                row[0] = (int)myType;
                row[1] = myType.ToString();
                tablePr.Rows.Add(row);
            }
            cbotype.FillWithData(tablePr, "id", "Name");

            //! Cols     
            Grid.Header.Height = 20;
            iGCol = Grid.Cols.Add("nro", "N°", 21);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("OrderNoteID", "");
            iGCol.Visible = false;

            iGCol = Grid.Cols.Add("Title", "Title", 51);
            iGCol.CellStyle.ReadOnly = iGBool.True;
            iGCol = Grid.Cols.Add("Description", "Description", 225);

            iGCol = Grid.Cols.Add("Modifier", "Modifier", 60);
            iGCol.CellStyle.DropDownControl = cbotype;
            iGCol.CellStyle.TypeFlags |= iGCellTypeFlags.NoTextEdit;


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
                // item.AllowSizing = false;
            }
        }

        public void CargarColumnasFDelivery()
        {
            PintarGrid();
            iGCol iGCol;

            //! Cols     
            Grid.Header.Height = 20;
            iGCol = Grid.Cols.Add("nro", "N°", 21);
            iGCol.CellStyle.ReadOnly = iGBool.True;

            iGCol = Grid.Cols.Add("Description", "Description", 270);
            iGCol.CellStyle.ReadOnly = iGBool.True;

            iGCol = Grid.Cols.Add("Date", "Date", 66);
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
                // item.AllowSizing = false;
            }
        }

    }
}
