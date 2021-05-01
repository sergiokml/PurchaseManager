using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.PerfilAbstract;

namespace PurchaseDesktop.Formularios
{
    public partial class FPrincipal : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;

        public TextInfo UCase { get; set; }
        public FSupplier FSupplier { get; set; }

        public FDetails FDetails { get; set; }
        public FOrderAttach FAttach { get; set; }
        public FPrincipal(PerfilFachada rFachada, FSupplier fSupplier, FDetails fDetails, FOrderAttach fAttach)
        {
            FAttach = fAttach;
            FDetails = fDetails;
            FSupplier = fSupplier;
            this.rFachada = rFachada;
            InitializeComponent();
        }

        private void BtnCloseFrm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            //! Validar Controles
            if (ValidarControles())
            {
                rFachada.InsertItem((Companies)CboCompany.SelectedItem
                    , (OrderType)CboType.SelectedItem);
                LlenarGrid();
                //Grid.Rows[0].EnsureVisible();
                ClearControles();

                LblMsg.Text = $" Se ha cargado el Perfil: {rFachada.MsgTest()}  {Grid.Rows.Count}";
                //Grid.CurRow = Grid.Rows[0];
            }
        }

        private void FPrincipal_Load(object sender, EventArgs e)
        {
            //Icon = Desktop.Properties.Resources.icons8_sort_window;
            //! Company
            CboCompany.DataSource = new Companies().GetList();
            CboCompany.DisplayMember = "Name";
            CboCompany.SelectedIndex = -1;
            CboCompany.ValueMember = "CompanyID";

            //! Type
            CboType.DataSource = Enum.GetValues(typeof(OrderType));
            CboType.SelectedIndex = -1;

            //! Events
            // Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CellMouseDown += Grid_CellMouseDown;
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;

            //! Grid Principal
            Grid = rFachada.CargarGrid(Grid);
            // Grid = rFachada.ControlesGrid(Grid, new OrderStatus().GetList());
            LlenarGrid();


            List<Color> bgColors = new List<Color>
            {
                Color.FromArgb(3, 121, 213),
                 Color.FromArgb(53, 146, 171),
                  Color.FromArgb(77, 158, 150),
                   Color.FromArgb(106, 172, 125),
                    Color.FromArgb(130, 184, 105),
                     Color.FromArgb(150, 194, 89)
            };





            ChartCanvas1.XAxesGridLines = false;
            ChartCanvas1.ShowXAxis = false;
            ChartCanvas1.YAxesGridLines = false;
            ChartCanvas1.ShowYAxis = false;
            ChartCanvas1.BackColor = Color.FromArgb(34, 34, 34);
            ChartCanvas1.LegendDisplay = false;

            ChartCanvas2.XAxesGridLines = false;
            ChartCanvas2.YAxesGridLines = false;
            ChartCanvas2.ShowXAxis = false;
            ChartCanvas2.ShowYAxis = false;
            ChartCanvas2.BackColor = Color.FromArgb(34, 34, 34);



            PolarAreaChart1.BackgroundColor = bgColors;
            PolarAreaChart1.BorderColor = new List<Color>() { Color.FromArgb(45, 45, 48) };
            PolarAreaChart2.BackgroundColor = bgColors;
            PolarAreaChart2.BorderColor = new List<Color>() { Color.FromArgb(45, 45, 48) };

            rFachada.CargarDashBoard(PolarAreaChart1, PolarAreaChart2, ChartCanvas1);

        }

        private void LlenarGrid()
        {
            Grid.BeginUpdate();
            try
            {
                var vista = rFachada.GetVistaPrincipal();
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //! Data Bound  ***!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                Grid.Refresh();
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

        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (e.ColIndex > 0)
            {
                var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                CboCompany.SelectedValue = current["CompanyID"].ToString();
                CboType.SelectedIndex = Convert.ToByte(current["Type"]) - 1;
                // LblMsg.Text = $"Ha Seleccionado el ID:{current["OrderHeaderID"]}";

                SetControles();
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
            var cbo = (Companies)CboCompany.SelectedItem;
            //TxtCompanyName.Text = cbo.Name;

            //Grid.BeginUpdate();
            //FillGrid();
            //Grid.EndUpdate();
        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                return;
            }
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (!current[Grid.Cols[e.ColIndex].Key].Equals(e.NewValue))
            {
                if (rFachada.UpdateItem(e.NewValue, current, Grid.Cols[e.ColIndex].Key))
                {
                    LlenarGrid();
                }
                else
                {
                    e.Result = iGEditResult.Cancel;
                    LblMsg.Text = "No se puede actualizar el dato.";
                }

            }
        }

        private void CboCompany_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //SetControles();
        }


        private void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                if (rFachada.DeleteItem(current))
                {
                    LlenarGrid();
                    ClearControles();
                    Grid.Focus();
                    Grid.DrawAsFocused = false;
                }
                else
                {
                    LblMsg.Text += "No se puede eliminar";
                }
            }
            else if (Grid.Cols["view"].Index == e.ColIndex)
            {
                rFachada.VerItemHtml(current);
                Grid.Focus();
                Grid.DrawAsFocused = false;
            }
            else if (Grid.Cols["supplier"].Index == e.ColIndex)
            {
                rFachada.EditarSupplier(FSupplier);
            }
            else if (Grid.Cols["details"].Index == e.ColIndex)
            {
                rFachada.EditarDetails(FDetails, current);
            }
            else if (Grid.Cols["attach"].Index == e.ColIndex)
            {
                rFachada.EditarAttach(FAttach);
            }
            else if (Grid.Cols["hitos"].Index == e.ColIndex)
            {
                //rFachada.EditarSupplier(FSupplier);
            }

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        private void Grid_CustomDrawCellEllipsisButtonBackground(object sender, iGCustomDrawEllipsisButtonEventArgs e)
        {
            if (e.ColIndex == Grid.Cols["view"].Index)
            {
                #region Determine the colors of the background
                Color myColor1, myColor2;
                Rectangle myBounds = e.Bounds;
                switch (e.State)
                {
                    case iGControlState.Pressed:
                        myColor1 = SystemColors.ControlDark;
                        myColor2 = SystemColors.ControlLightLight;
                        break;
                    case iGControlState.Hot:
                        e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                        //myColor1 = SystemColors.ControlLightLight;
                        //myColor2 = SystemColors.ControlDark;
                        break;
                    default:
                        myColor1 = Color.FromArgb(34, 34, 34);
                        myColor2 = Color.FromArgb(34, 34, 34);
                        break;
                }
                #endregion

                #region Draw the background
                Grid.ImageList.Draw(e.Graphics, myBounds.Location, 0);
                //LinearGradientBrush myBrush = new LinearGradientBrush(e.Bounds, myColor1, myColor2, 45);
                //e.Graphics.FillRectangle(myBrush, e.Bounds);
                //e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                #endregion

                #region Notify the grid that the background has been drawn and there is no need to draw it
                e.DoDefault = false;
                #endregion                
            }
            if (e.ColIndex == Grid.Cols["delete"].Index)
            {
                #region Determine the colors of the background
                Color myColor1, myColor2;
                Rectangle myBounds = e.Bounds;
                switch (e.State)
                {
                    case iGControlState.Pressed:
                        myColor1 = SystemColors.ControlDark;
                        myColor2 = SystemColors.ControlLightLight;
                        break;
                    case iGControlState.Hot:
                        e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                        //myColor1 = SystemColors.ControlLightLight;
                        //myColor2 = SystemColors.ControlDark;
                        break;
                    default:
                        myColor1 = Color.FromArgb(34, 34, 34);
                        myColor2 = Color.FromArgb(34, 34, 34);
                        break;
                }
                #endregion

                #region Draw the background
                Grid.ImageList.Draw(e.Graphics, myBounds.Location, 1);
                //LinearGradientBrush myBrush = new LinearGradientBrush(e.Bounds, myColor1, myColor2, 45);
                // e.Graphics.FillRectangle(myBrush, e.Bounds);
                //e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                #endregion

                #region Notify the grid that the background has been drawn and there is no need to draw it
                e.DoDefault = false;
                #endregion                
            }
        }

        private void Grid_CustomDrawCellEllipsisButtonForeground(object sender, iGCustomDrawEllipsisButtonEventArgs e)
        {
            if (e.ColIndex == Grid.Cols["delete"].Index)
            {
                #region Determine the bounds of the foreground
                Rectangle myBounds = e.Bounds;
                //myBounds.Inflate(-1, -1);
                #endregion

                if (myBounds.Width > 0 || myBounds.Height > 0)
                {
                    #region Determine the colors of the foreground
                    Color myColor1, myColor2;
                    switch (e.State)
                    {
                        case iGControlState.Hot:
                            myColor1 = Color.FromArgb(255, 205, 205);
                            myColor2 = Color.FromArgb(255, 0, 0);
                            break;
                        default:
                            myColor1 = Color.FromArgb(205, 205, 205);
                            myColor2 = Color.FromArgb(205, 0, 0);
                            break;
                    }
                    #endregion

                    #region Draw the foreground
                    Grid.ImageList.Draw(e.Graphics, myBounds.Location, 1);
                    //LinearGradientBrush myBrush = new LinearGradientBrush(myBounds, myColor1, myColor2, 45);
                    //e.Graphics.FillRectangle(myBrush, myBounds);
                    //e.Graphics.DrawRectangle(Pens.DarkRed, myBounds.X, myBounds.Y, myBounds.Width - 1, myBounds.Height - 1);
                    #endregion

                    #region Notify the grid that the foreground has been drawn, and there is no need to draw it
                    e.DoDefault = false;
                    #endregion
                }
            }
            if (e.ColIndex == Grid.Cols["view"].Index)
            {
                #region Determine the bounds of the foreground
                Rectangle myBounds = e.Bounds;
                // myBounds.Inflate(-1, -1);
                #endregion

                if (myBounds.Width > 0 || myBounds.Height > 0)
                {
                    #region Determine the colors of the foreground
                    Color myColor1, myColor2;
                    switch (e.State)
                    {
                        case iGControlState.Hot:
                            myColor1 = Color.FromArgb(255, 205, 205);
                            myColor2 = Color.FromArgb(255, 0, 0);
                            break;
                        default:
                            myColor1 = Color.FromArgb(205, 205, 205);
                            myColor2 = Color.FromArgb(205, 0, 0);
                            break;
                    }
                    #endregion

                    #region Draw the foreground
                    Grid.ImageList.Draw(e.Graphics, myBounds.Location, 0);
                    //LinearGradientBrush myBrush = new LinearGradientBrush(myBounds, myColor1, myColor2, 45);
                    //e.Graphics.FillRectangle(myBrush, myBounds);
                    //e.Graphics.DrawRectangle(Pens.DarkRed, myBounds.X, myBounds.Y, myBounds.Width - 1, myBounds.Height - 1);
                    #endregion

                    #region Notify the grid that the foreground has been drawn, and there is no need to draw it
                    e.DoDefault = false;
                    #endregion
                }
            }
        }
    }
}
