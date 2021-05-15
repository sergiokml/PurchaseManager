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

namespace PurchaseDesktop.Formularios
{
    public partial class FDetails : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;


        public TextInfo UCase { get; set; }
        private int HeaderID { get; set; }
        //public int ItemStatus { get; set; }
        public DataRow Current { get; set; }


        public FDetails(PerfilFachada rFachada, DataRow dataRow)
        {
            this.rFachada = rFachada;
            Current = dataRow;
            HeaderID = Convert.ToInt32(dataRow["HeaderID"]);
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FDetails_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            SetControles();
            //! Grid Principal
            Grid = rFachada.CargarGrid(Grid, "FDetails");
            LlenarGrid();

            //! Eventos
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;
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
        }

        public void Grid_CustomDrawCellEllipsisButtonBackground(object sender, iGCustomDrawEllipsisButtonEventArgs e)
        {
            if (e.ColIndex == Grid.Cols["delete"].Index || e.ColIndex == Grid.Cols["view"].Index)
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

        private void BtnNewDetail_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                var PrDetail = new RequisitionDetails
                {
                    AccountID = ((Accounts)CboAccount.SelectedItem).AccountID,
                    Qty = Convert.ToInt32(TxtQty.Text),
                    NameProduct = TxtName.Text.Trim(),
                    DescriptionProduct = TxtDescription.Text.Trim()
                };
                rFachada.InsertDetail(PrDetail, HeaderID);
                LlenarGrid();
                ClearControles();
            }
        }

        public void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {
                var vista = rFachada.GetVistaDetalles(Current);
                //var vista = TableDetails;
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //! Data Bound  ***!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    Grid.Rows[i].Cells["nro"].Value = i + 1;
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

        public bool ValidarControles()
        {
            if (CboAccount.SelectedIndex == -1)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(TxtName.Text))
            {
                return false;
            }

            else if (!int.TryParse(TxtQty.Text, out int parsedValue))
            {
                return false;
            }
            return true;
        }

        public void ClearControles()
        {
            TxtDescription.Text = string.Empty;
            TxtName.Text = string.Empty;
            TxtPrice.Text = string.Empty;
            TxtQty.Text = string.Empty;
        }

        public iGrid GetGrid()
        {
            return Grid;
        }
        public void SetControles()
        {
            CboAccount.DataSource = new Accounts().GetList();
            CboAccount.DisplayMember = "Description";
            CboAccount.SelectedIndex = -1;
            CboAccount.ValueMember = "AccountID";
        }

        private void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            var currentDetail = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                if (rFachada.DeleteDetail(currentDetail, Current))
                {
                    LlenarGrid();
                    ClearControles();
                    Grid.Focus();
                    Grid.DrawAsFocused = false;
                    CboAccount.SelectedIndex = -1;
                }
                else
                {
                    // LblMsg.Text += "No se puede eliminar";
                }
            }
        }

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (e.ColIndex > 0)
            {

                SetControles();
            }
        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
