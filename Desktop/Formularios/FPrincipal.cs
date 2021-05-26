using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Formularios
{
    public partial class FPrincipal : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;

        public TextInfo UCase { get; set; }

        public bool IsSending { get; set; }

        public DataRow Current { get; set; }

        public FPrincipal(PerfilFachada rFachada)
        {
            this.rFachada = rFachada;
            InitializeComponent();
        }

        public FPrincipal()
        {
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            //! Validar Controles
            if (ValidarControles())
            {
                rFachada.InsertItem((Companies)CboCompany.SelectedItem
                    , (DocumentType)CboType.SelectedItem);
                LlenarGrid();
                CargarDashboard();
                //Grid.Rows[0].EnsureVisible();
                ClearControles();
                SetControles();
            }
        }

        private void CargarDashboard()
        {
            rFachada.CargarDashBoard(Grid, PieChart1, PieChart2, ChartCanvas1, ChartCanvas2, Lbl1, Lbl2, Lbl3);
        }

        private async void FPrincipal_Load(object sender, EventArgs e)
        {

            Icon = Properties.Resources.icons8_survey;
            //! Company
            CboCompany.DataSource = new Companies().GetList();
            CboCompany.DisplayMember = "Name";
            CboCompany.SelectedIndex = -1;
            CboCompany.ValueMember = "CompanyID";



            //! Eventos
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;
            Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;
            Grid.CellMouseDown += Grid_CellMouseDown;
            Grid.CellMouseUp += Grid_CellMouseUp;

            //! Type
            CboType.DataSource = Enum.GetValues(typeof(DocumentType));
            CboType.SelectedIndex = -1;

            //! Grid Principal
            rFachada.CargarGrid(Grid, "FPrincipal"); // Pintar y cargar columnas
            LlenarGrid();

            //! Otros
            Users user = rFachada.CurrentUser();
            LblUser.Text = $"User: {user.FirstName} {user.LastName} | Profile: {user.ProfileID} | Email: {user.Email}";

            //! Banner
            //string s = await rFachada.CargarBanner();
            //WBrowserBanner.Navigate(s);



            Timer TimerMsg = new Timer
            {
                Interval = 10 // 2 seconds
            };
            TimerMsg.Tick += (object d, EventArgs f) =>
            {
                //! DashBoard
                CargarDashboard();
                SetControles();
                PanelHechizo.Visible = false;
                PanelHechizoBanner.Visible = false;
                TimerMsg.Stop();
            }; TimerMsg.Start();

            LabelPanel.Text = "Purshase Manager V1.0";
            LabelPanel.TextAlign = ContentAlignment.MiddleLeft;
            LabelPanel.Visible = true;
        }


        public void LlenarGrid()
        {
            Grid.BeginUpdate();
            try
            {
                DataTable vista = rFachada.GetVistaFPrincipal();
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //! Data Bound  ***!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                Grid = rFachada.FormatearGrid(Grid, "FPrincipal");
                Grid.Refresh();
                Msg($"You have {vista.Rows.Count} documents issued and showing.", MsgProceso.Informacion);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Grid.EndUpdate();
            }
        }

        public bool ValidarControles()
        {
            //! Company
            if (CboCompany.SelectedIndex == -1)
            {
                return false;
            }
            else if (CboType.SelectedIndex == -1)
            {
                return false;
            }
            //else if (string.IsNullOrEmpty(TxtId.Text))
            //{
            //    return false;
            //}
            return true;
        }

        public void ClearControles()
        {
            CboCompany.SelectedIndex = -1;
            CboCompany.Select(1, 0);
            CboType.SelectedIndex = -1;
            CboType.Select(1, 0);

        }

        public void SetControles()
        {
            //! Company
            //var cbo = (Companies)CboCompany.SelectedItem;
            //if (rFachada.GetUser().ProfileID == "UPO")
            //{
            if (Grid.Rows.Count > 0)
            {
                int nPO = 0;
                int nPR = 0;
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    if (Grid.Cols["TypeDocumentHeader"].Cells[myRowIndex].Value.ToString() == "PR")
                    {
                        nPR++;
                    }
                    else if (Grid.Cols["TypeDocumentHeader"].Cells[myRowIndex].Value.ToString() == "PO")
                    {
                        nPO++;
                    }
                }
                if (nPO > 0 && nPR > 0)
                {
                    Grid.DefaultAutoGroupRow.Expanded = true;
                    Grid.GroupObject.Add("TypeDocumentHeader");
                    //if (Grid.GroupObject.Count > 1)
                    //{

                    Grid.Group();
                }
                else
                {
                    Grid.GroupObject.Clear();
                    Grid.Group();
                }


                //}

            }
            //}

        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                e.Result = iGEditResult.Cancel;
                return;
            }
            if (Equals(e.NewValue, Grid.Cells[e.RowIndex, e.ColIndex].Value))
            {
                e.Result = iGEditResult.Cancel;
                return;
            }
            //! Update solo si cambió el dato.
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            var resultado = rFachada.UpdateItem(e.NewValue, current, Grid.Cols[e.ColIndex].Key);
            if (resultado == "OK")
            {
                LlenarGrid();
                if (!Grid.Cols[e.ColIndex].Key.Equals("Description") && !Grid.Cols[e.ColIndex].Key.Equals("Type"))
                {
                    CargarDashboard();
                }
                SetControles();
            }
            else
            {
                e.Result = iGEditResult.Cancel;
                Msg("This document cannot be updated.", MsgProceso.Error);
            }
        }

        private async void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                var resultado = rFachada.DeleteItem(current);
                if (resultado == "OK")
                {
                    LlenarGrid();
                    CargarDashboard();
                    ClearControles();
                    SetControles();
                    Grid.Focus();
                }
                else
                {
                    Msg(resultado, MsgProceso.Error);
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            else if (Grid.Cols["view"].Index == e.ColIndex)
            {
                if (!rFachada.VerItemHtml(current))
                {
                    Grid.Focus();
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                    Msg("Cannot open the document.", MsgProceso.Error);
                }

            }
            else if (Grid.Cols["send"].Index == e.ColIndex)
            {
                if (!IsSending)
                {
                    IsSending = true;
                    LblMsg.ImageAlign = ContentAlignment.MiddleLeft;
                    LblMsg.Image = Properties.Resources.loading;
                    string respuesta = await rFachada.SendItem(current);
                    if (respuesta == null)
                    {
                        Msg("This requisition has no products.", MsgProceso.Error);
                        IsSending = false;
                    }
                    else
                    {
                        Msg(respuesta, MsgProceso.Send);
                        IsSending = false;
                    }
                }
            }
            Grid.DrawAsFocused = false;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        public void Grid_CustomDrawCellEllipsisButtonBackground(object sender, iGCustomDrawEllipsisButtonEventArgs e)
        {
            if (e.ColIndex == Grid.Cols["delete"].Index || e.ColIndex == Grid.Cols["view"].Index || e.ColIndex == Grid.Cols["send"].Index)
            {
                Rectangle myBounds = e.Bounds;
                myBounds.Inflate(2, 1);
                LinearGradientBrush myBrush;
                switch (e.State)
                {
                    case iGControlState.HotPressed:
                        myBrush = new LinearGradientBrush(e.Bounds, Color.FromArgb(37, 37, 38), Color.FromArgb(37, 37, 38), 1);
                        e.Graphics.FillRectangle(myBrush, myBounds);
                        break;
                    case iGControlState.Hot:
                        myBrush = new LinearGradientBrush(e.Bounds, Color.FromArgb(154, 196, 85), Color.FromArgb(154, 196, 85), 1);
                        e.Graphics.FillRectangle(myBrush, myBounds);
                        break;
                }
                e.DoDefault = false;
            }
        }

        public void Grid_CustomDrawCellEllipsisButtonForeground(object sender, iGCustomDrawEllipsisButtonEventArgs e)
        {
            if (e.ColIndex == Grid.Cols["delete"].Index)
            {
                Rectangle myBounds = e.Bounds;
                if (myBounds.Width > 0 || myBounds.Height > 0)
                {
                    Grid.ImageList.Draw(e.Graphics, myBounds.Location, 1);
                    e.DoDefault = false;
                }
            }
            else if (e.ColIndex == Grid.Cols["view"].Index)
            {
                Rectangle myBounds = e.Bounds;
                if (myBounds.Width > 0 || myBounds.Height > 0)
                {
                    Grid.ImageList.Draw(e.Graphics, myBounds.Location, 0);
                    e.DoDefault = false;
                }
            }
            else if (e.ColIndex == Grid.Cols["send"].Index)
            {
                Rectangle myBounds = e.Bounds;
                if (myBounds.Width > 0 || myBounds.Height > 0)
                {
                    Grid.ImageList.Draw(e.Graphics, myBounds.Location, 2);
                    e.DoDefault = false;
                }
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                FDetails f = new FDetails(rFachada, current);
                f.FormClosed += F_FormClosed;
                rFachada.OPenDetailForm(f, this);
            }
        }

        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            LlenarGrid();
            SetControles();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

        }

        public void Msg(string msg, MsgProceso proceso)
        {
            Timer TimerMsg = new Timer
            {
                Interval = 5000 // 5 seconds
            };

            TimerMsg.Tick += (object sender, EventArgs e) =>
            {
                LblMsg.Text = string.Empty;
                LblMsg.Image = null;
                TimerMsg.Stop();
            }; TimerMsg.Start();


            string Espaciosimage = "       ";
            LblMsg.Text = $"{Espaciosimage}{msg}";
            LblMsg.ImageAlign = ContentAlignment.MiddleLeft;

            switch (proceso)
            {
                case MsgProceso.Informacion:
                    LblMsg.Image = Properties.Resources.checked_18px;
                    break;
                case MsgProceso.Error:
                    LblMsg.Image = Properties.Resources.error_18px;
                    break;
                case MsgProceso.Send:
                    LblMsg.Image = Properties.Resources.send_18px;
                    break;
                case MsgProceso.Empty:
                    LblMsg.Image = null;
                    break;
            }
        }

        public enum MsgProceso
        {
            Informacion = 1,
            Error = 2,
            Send = 3,
            Empty = 4
        }

        public iGrid GetGrid()
        {
            return Grid;
        }

        private void BtnAttach_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                FAttach f = new FAttach(rFachada, current);
                var resultado = rFachada.OpenAttachForm(f, this);
                if (resultado == "OK")
                {

                }
                else
                {
                    Msg(resultado, FPrincipal.MsgProceso.Error);
                }
            }
        }

        private void BtnSupplier_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                FSupplier f = new FSupplier(rFachada, current);
                f.FormClosed += F_FormClosed;
                var resultado = rFachada.OpenSupplierForm(f, this);
                if (resultado == "OK")
                {

                }
                else
                {
                    Msg(resultado, FPrincipal.MsgProceso.Error);
                }
            }
        }



        private void Grid_CurCellChangeRequest(object sender, iGCurCellChangeRequestEventArgs e)
        {
            if (Grid.CurCell != null)
            {
                //  Current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                //var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                ////    Grid.Cols["StatusID"].Cells[e.RowIndex].DropDownControl = rFachada.CargarComBox(current);
                ////}

                //if (current["TypeDocumentHeader"].ToString() == "PR")
                //{
                //    e.DoDefault = false;
                //}
            }
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void Grid_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
        {
            if (Grid.CurCell != null && Grid.CurCell.ColIndex > -1)
            {
                var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                if (current != null)
                {
                    var resultado = rFachada.GridDobleClick(Current);
                    if (resultado == "OK")
                    {
                        LlenarGrid();
                        CargarDashboard();
                        ClearControles();
                        SetControles();
                    }
                    else
                    {
                        Msg(resultado, FPrincipal.MsgProceso.Error);
                    }

                }
            }
        }



        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            if (e.ColIndex > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                    if (rFachada.CargarContextMenuStrip(CtxMenu, Current))
                    {
                        CtxMenu.Show(Grid, e.MousePos);
                    }
                }
            }

        }
        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (e.ColIndex > 0)
            {
                Current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                if (e.Button == MouseButtons.Right)
                {
                    Grid.SetCurCell(e.RowIndex, e.ColIndex);
                    e.DoDefault = false;
                }
            }
        }


        private void Grid_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
        {
            iGCell myGroupRowCell = Grid.RowTextCol.Cells[e.AutoGroupRowIndex];
            iGCell myFirstCellInGroup = Grid.Cells[e.GroupedRowIndex, e.GroupedColIndex];

            if (Grid.Cols[e.GroupedColIndex].Key == "TypeDocumentHeader")
            {
                if (myFirstCellInGroup.Value.ToString() == "PO")
                {
                    myGroupRowCell.Value = "Purchase  Orders";

                }
                else if (myFirstCellInGroup.Value.ToString() == "PR")
                {
                    myGroupRowCell.Value = "Purchase  Requisitions";
                }
            }

        }

        private void CtxMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Convert To PO")
            {
                if (rFachada.SeleccionarContextMenuStrip(Current, "Convert To PO"))
                {
                    LlenarGrid();
                    CargarDashboard();
                    ClearControles();
                    SetControles();
                }
            }
        }
    }
}
