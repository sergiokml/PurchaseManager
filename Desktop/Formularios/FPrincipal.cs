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

        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public bool IsSending { get; set; }
        public DataRow Current { get; set; }
        public iGRow GuardarElPrevioCurrent { get; set; }

        #region Constructor

        public FPrincipal(PerfilFachada rFachada)
        {
            this.rFachada = rFachada;
            rFachada.Fprpal = this;
            InitializeComponent();
        }
        public FPrincipal()
        {
        }

        #endregion

        #region Métodos Privados
        public void CargarDashboard()
        {
            rFachada.CargarDashBoard(Grid, PieChart1, PieChart2, PanelDash);
        }

        private async void FPrincipal_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            //! Company
            CboCompany.DataSource = new Companies().GetList();
            CboCompany.DisplayMember = "Name";
            CboCompany.SelectedIndex = -1;
            CboCompany.ValueMember = "CompanyID";
            //! Type
            CboType.DataSource = new TypeDocument().GetList();
            CboType.DisplayMember = "Description";
            CboType.SelectedIndex = -1;
            CboType.ValueMember = "TypeID";

            //! Eventos
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;
            Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;
            Grid.CellMouseDown += Grid_CellMouseDown;
            Grid.CellMouseUp += Grid_CellMouseUp;
            Grid.CellEllipsisButtonClick += Grid_CellEllipsisButtonClick;

            //! Grid Principal
            rFachada.CargarGrid(Grid, "FPrincipal", Current); // Pintar y cargar columnas
            LlenarGrid();

            //! Otros
            Users user = rFachada.CurrentUser();
            LblUser.Text = $"User: {user.FirstName} {user.LastName} | Profile: {user.ProfileID} | Email: {user.Email}";

            SetControles();

            //! Banner
            string s = await rFachada.CargarBanner();
            try
            {
                WBrowserBanner.Navigate(s);
            }
            catch (Exception)
            {
                Msg(s, MsgProceso.Error);
            }
            Timer TimerMsg = new Timer
            {
                Interval = 10 // No eliminar!
            };
            TimerMsg.Tick += (object d, EventArgs f) =>
            {
                //! DashBoard
                CargarDashboard();
                PanelHechizo.Visible = false;
                PanelHechizoBanner.Visible = false;
                TimerMsg.Stop();
            }; TimerMsg.Start();
        }

        private void FPrincipal_Shown(object sender, EventArgs e)
        {
            LabelPanel.Text = "Purchase Manager V1.0";
            LabelPanel.TextAlign = ContentAlignment.MiddleLeft;
            LabelPanel.Visible = true;
        }

        #endregion

        #region Botones Form

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                rFachada.InsertItem((Companies)CboCompany.SelectedItem
                    , (TypeDocument)CboType.SelectedItem);
                if (Grid.Rows.Count < 1)
                {
                    return;
                }
                if (Grid.GroupObject.Count > 0)
                {
                    Grid.CurRow = Grid.Rows[1];
                }
                else
                {
                    Grid.CurRow = Grid.Rows[0];
                }
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Eventos Grid Principal 

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                e.Result = iGEditResult.Cancel; return;
            }
            if (Equals(e.NewValue, Grid.Cells[e.RowIndex, e.ColIndex].Value))
            {
                e.Result = iGEditResult.Cancel; return;
            }
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //! En caso de hacer update desde otra fila, no posicionándose en ella.
            GuardarElPrevioCurrent = Grid.Rows[e.RowIndex];
            DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            rFachada.UpdateItem(e.NewValue, current, Grid.Cols[e.ColIndex].Key);
            //! Al hacer update se pierde el foco:
            Grid.CurRow = GuardarElPrevioCurrent;
            if (IsSending)
            {
                e.Result = iGEditResult.Cancel;
                IsSending = false;
            }
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Default;
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

        public async void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                rFachada.DeleteItem(current);
                Grid.Focus();
            }
            else if (Grid.Cols["view"].Index == e.ColIndex)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                rFachada.VerItemHtml(current);
                Grid.Focus();
            }
            else if (Grid.Cols["send"].Index == e.ColIndex)
            {
                if (!IsSending)
                {
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    await rFachada.SendItem(current);
                    Grid.Focus();
                }
            }
            Grid.DrawAsFocused = false;
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            if (Grid.CurCell != null && Grid.CurCell.ColIndex > -1 && Grid.Cols[e.ColIndex].Key != "delete")
            {
                var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                if (current != null)
                {
                    rFachada.GridDobleClick(Current);
                    Grid.CurRow = GuardarElPrevioCurrent;
                }
            }
            Grid.Focus();
            Grid.DrawAsFocused = false;
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


        #region Menú Contextual

        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            if (e.ColIndex > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                    rFachada.CargarContextMenuStrip(CtxMenu, Current);
                    CtxMenu.Show(Grid, e.MousePos);
                }
            }
        }

        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (e.ColIndex > 0)
            {
                Current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                GuardarElPrevioCurrent = Grid.CurRow;
                if (e.Button == MouseButtons.Right)
                {
                    Grid.SetCurCell(e.RowIndex, e.ColIndex);
                    e.DoDefault = false;
                }
            }
        }

        private async void CtxMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Grid.DrawAsFocused = true;
            await rFachada.SeleccionarContextMenuStripAsync(Current, e.ClickedItem.Name);
            Grid.Focus();
            Grid.DrawAsFocused = false;
        }


        #endregion

        #endregion

        #region Interfaz IControles

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
                    if (rFachada.CurrentUser().ProfileID == "UPO")
                    {
                        Grid.GroupObject.Add("TypeDocumentHeader").SortOrder = iGSortOrder.Ascending;
                    }
                    else
                    {

                        Grid.GroupObject.Add("TypeDocumentHeader").SortOrder = iGSortOrder.Descending;
                    }



                    //Grid.SortObject.Add("TypeDocumentHeader");
                    //Grid.SortObject[0].SortType = iGSortType.ByValue;
                    //Grid.SortObject[0].SortOrder = iGSortOrder.Descending;

                    Grid.Group();




                }
                else
                {
                    Grid.GroupObject.Clear();
                    Grid.Group();
                }




            }


        }

        public iGrid GetGrid()
        {
            return Grid;
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
                Grid = rFachada.FormatearGrid(Grid, "FPrincipal", Current);
                Grid.Refresh();
                //Msg($"You have {vista.Rows.Count} documents issued and showing.", MsgProceso.Informacion);
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

        #endregion

        #region Métodos Públicos
        public void Msg(string msg, MsgProceso proceso)
        {
            Timer TimerMsg = new Timer
            {
                Interval = 3000 // 5 seconds
            };

            TimerMsg.Tick += (object sender, EventArgs e) =>
            {
                LblMsg.Text = string.Empty;
                LblMsg.Image = null;
                TimerMsg.Stop();
            }; TimerMsg.Start();
            LblMsg.Text = $"{new string(' ', 10)}{msg}";
            LblMsg.ImageAlign = ContentAlignment.MiddleLeft;

            switch (proceso)
            {
                case MsgProceso.Informacion:
                    LblMsg.Image = Properties.Resources.good_pincode_18px;
                    break;
                case MsgProceso.Warning:
                    LblMsg.Image = Properties.Resources.error_18px;
                    break;
                case MsgProceso.Send:
                    LblMsg.Image = Properties.Resources.received_18px;
                    break;
                case MsgProceso.Empty:
                    LblMsg.Image = null;
                    break;
                case MsgProceso.Error:
                    LblMsg.Image = Properties.Resources.cancel_18px;
                    break;
            }
        }



        #endregion

        #region Open Formularios

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                if (current != null)
                {
                    FDetails f = new FDetails(rFachada, current);
                    rFachada.OPenDetailForm(f);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAttach_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                if (current != null)
                {
                    FAttach f = new FAttach(rFachada, current);
                    rFachada.OpenAttachForm(f);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupplier_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                if (current != null)
                {
                    FSupplier f = new FSupplier(rFachada, current);
                    rFachada.OpenSupplierForm(f);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHitos_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                if (current != null)
                {
                    FHitos f = new FHitos(rFachada, current);
                    rFachada.OpenHitosForm(f);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNotes_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                if (current != null)
                {
                    FNotes f = new FNotes(rFachada, current);
                    rFachada.OpenNotesForm(f);
                }
            }
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            var f = new FConfig(rFachada.CurrentUser())
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterParent
            };
            f.ShowDialog(this);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelivery_Click(object sender, EventArgs e)
        {
            if (Grid.CurCell != null)
            {
                DataRow current = (DataRow)Grid.CurRow.Tag;
                if (current != null)
                {
                    FDeliverys f = new FDeliverys(rFachada, current);
                    rFachada.OpenDeliveryForm(f);
                }
            }
        }

        #endregion

    }
}
