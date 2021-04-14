using System;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;

namespace PurchaseDesktop.Formularios
{
    public partial class FSupplier : Form
    {
        private readonly PerfilFachada rFachada;
        public FSupplier(PerfilFachada rFachada)
        {
            this.rFachada = rFachada;
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FSupplier_Load(object sender, EventArgs e)
        {
            CboBanks.DataSource = new SupplierBanks().GetList();
            CboBanks.DisplayMember = "Description";
            CboBanks.SelectedIndex = -1;
            CboBanks.ValueMember = "BankID";

            CboCountries.DataSource = new SupplierCountries().GetList();
            CboCountries.DisplayMember = "Description";
            CboCountries.SelectedIndex = -1;
            CboCountries.ValueMember = "CountryID";

            CboTypeAccount.DataSource = Enum.GetValues(typeof(TypeAccount));
            CboTypeAccount.SelectedIndex = -1;

            Grid = rFachada.PintarGrid(Grid);
            LlenarGrid();

        }

        private void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {

                var vista = rFachada.GetVistaSuppliers();
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

        public enum TypeAccount
        {
            Corriente = 1,
            Vista = 2,
            Ahorros = 3
        }

        private void Grid_ColDividerDoubleClick(object sender, TenTec.Windows.iGridLib.iGColDividerDoubleClickEventArgs e)
        {

            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }
    }
}
