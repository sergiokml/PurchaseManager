using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Formularios
{
    public partial class FDetails : Form, IControles
    {
        private readonly PerfilFachada rFachada;

        public TextInfo UCase { get; set; }
        public int ItemID { get; set; }

        private OrderHeader OrderHeader { get; set; }

        public List<RequisitionDetails> RequisitionDetails { get; set; }

        public FDetails(PerfilFachada rFachada, List<RequisitionDetails> lista)
        {
            //! Este constructor vienen así.
            this.rFachada = rFachada;
            RequisitionDetails = lista;
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FDetails_Load(object sender, EventArgs e)
        {
            SetControles();
            //! Grid Principal
            Grid = rFachada.CargarGrid(Grid, "FDetails");
            LlenarGrid();
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
                rFachada.InsertDetail(PrDetail, ItemID);
                LlenarGrid();
                ClearControles();
            }
        }

        private void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {
                var vista = rFachada.GetVistaDetalles(ItemID);
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);

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
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {


                if (rFachada.DeleteOrderDetail(OrderHeader,
                    Convert.ToInt32(Grid.Cols["DetailID"].Cells[e.RowIndex].Value)))
                {
                    LlenarGrid();
                    ClearControles();
                    Grid.Focus();
                    Grid.DrawAsFocused = false;
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
    }
}
