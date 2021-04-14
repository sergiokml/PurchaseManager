using System;
using System.Data;
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

        public FPrincipal(PerfilFachada rFachada, FSupplier fSupplier)
        {
            FSupplier = fSupplier;
            this.rFachada = rFachada;
            InitializeComponent();
        }

        private void BtnCloseFrm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMinFrm_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            //! Validar Controles
            if (ValidarControles())
            {
                rFachada.InsertOrderHeader((OrderCompanies)CboCompany.SelectedItem
                    , (OrderType)CboType.SelectedItem);
                LlenarGrid();
                //Grid.Rows[0].EnsureVisible();
                ClearControles();
                //Grid.CurRow = Grid.Rows[0];
            }
        }

        private void FPrincipal_Load(object sender, EventArgs e)
        {
            //Icon = Desktop.Properties.Resources.icons8_sort_window;
            //! Company
            CboCompany.DataSource = new OrderCompanies().GetList();
            CboCompany.DisplayMember = "Name";
            CboCompany.SelectedIndex = -1;
            CboCompany.ValueMember = "CompanyID";

            //! Type
            CboType.DataSource = Enum.GetValues(typeof(OrderType));
            CboType.SelectedIndex = -1;

            //! Events
            Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CellMouseDown += Grid_CellMouseDown;
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;

            //! Grid Principal
            Grid = rFachada.PintarGrid(Grid);
            Grid = rFachada.ControlesGrid(Grid, new OrderStatus().GetList());
            LlenarGrid();

        }

        private void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {

                var vista = rFachada.GetVista(Grid);
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //! Data Bound  ***!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                Grid.Refresh();

                LblMsg.Text = $"Se ha cargado el Perfil: {rFachada.MsgTest()}  {Grid.Rows.Count}";
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
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            CboCompany.SelectedValue = current.ItemArray[9].ToString();
            CboType.SelectedIndex = Convert.ToInt32(current.ItemArray[7]) - 1;
            LblMsg.Text = $"Ha Seleccionado el ID:{current["OrderHeaderID"].ToString()}";

            SetControles();
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
            var cbo = (OrderCompanies)CboCompany.SelectedItem;
            //TxtCompanyName.Text = cbo.Name;

            //Grid.BeginUpdate();
            //FillGrid();
            //Grid.EndUpdate();
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            //if (e.ColIndex == Grid.Cols["state"].Index)
            //{
            //    if (beforevaluestatus != Convert.ToInt32(Grid.Cols["state"].Cells[e.RowIndex].Value))
            //    {
            //        Grid.ReadOnly = true;
            //        rContext.InsertTransaction(
            //            Convert.ToInt32(Grid.Cols["state"].Cells[e.RowIndex].Value));
            //        ClearControles();
            //        FillGrid();
            //        Grid.ReadOnly = false;
            //    }
            //}
            //else if (e.ColIndex == Grid.Cols["typereq"].Index)
            //{
            //    if (beforevaluetypereq != Convert.ToInt32(Grid.Cols["typereq"].Cells[e.RowIndex].Value))
            //    {
            //        Grid.ReadOnly = true;
            //        rContext.UpdatePrHeader(Convert.ToInt32(Grid.Cols["typereq"].Cells[e.RowIndex].Value), "typereq");
            //        ClearControles();
            //        FillGrid();
            //        Grid.ReadOnly = false;
            //    }
            //}
            //else if (Grid.Cols["description"].Cells[e.RowIndex].Value != null)
            //{
            //    if (beforevaluedescription != Grid.Cols["description"].Cells[e.RowIndex].Value.ToString())
            //    {
            //        DialogResult result = MessageBox.Show("Hay cambios, desea guardar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result == DialogResult.Yes)
            //        {
            //            rContext.UpdatePrHeader(Grid.Cols["description"].Cells[e.RowIndex].Value, "description");
            //            ClearControles();
            //            FillGrid();
            //        }
            //    }
            //}
            //else
            //{
            //    Grid.Cols["description"].Cells[e.RowIndex].Value = beforevaluedescription;
            //}
        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (!current[Grid.Cols[e.ColIndex].Key].Equals(e.NewValue))
            {
                if (rFachada.UpdateOrderHeader(e.NewValue, current, Grid.Cols[e.ColIndex].Key))
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

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        private void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == 16)
            {
                if (rFachada.DeleteOrderHeader(current))
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
            else if (Grid.Cols["supplier"].Index == e.ColIndex)
            {
                rFachada.EditarSupplier(FSupplier);
            }
        }
    }
}
