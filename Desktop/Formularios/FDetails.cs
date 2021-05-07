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
            //OrderHeader = new OrderHeader().GetById(ItemID); //todo Y EL REQ?
            LlenarGrid();
        }

        private void BtnNewDetail_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                //var detail = new OrderDetails
                //{
                //    //AccountID = ((OrderAccounts)CboAccount.SelectedItem).AccountID,
                //    Qty = Convert.ToInt32(TxtQty.Text),
                //    NameProduct = TxtName.Text,
                //    DescriptionProduct = TxtName.Text
                //};
                //new OrderDetails().AddDetail(detail, OrderHeader);

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
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.FromArgb(0, 120, 215), ButtonBorderStyle.Solid);
        //}
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


                //int nro = 1;
                //var total = RequisitionDetails.Count;
                //foreach (var item in RequisitionDetails)
                //{
                //    iGRow myRow = Grid.Rows.Add();
                //    myRow.Cells["nro"].Value = nro;
                //    myRow.Cells["DetailID"].Value = item.DetailID;
                //    myRow.Cells["Qty"].Value = item.Qty;
                //    myRow.Cells["NameProduct"].Value = item.NameProduct;
                //    myRow.Cells["AccountID"].Value = item.AccountID;
                //    nro++;
                //    myRow.Cells["delete"].ImageIndex = 2;
                //}
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


    }
}
