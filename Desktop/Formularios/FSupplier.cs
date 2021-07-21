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
    public partial class FSupplier : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;

        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public DataRow Current { get; set; }
        public iGRow GuardarElPrevioCurrent { get; set; }

        public FSupplier(PerfilFachada rFachada, DataRow headerDR)
        {
            this.rFachada = rFachada;
            Current = headerDR;
            InitializeComponent();
        }

        private void FSupplier_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            //! Bank
            CboBanks.DataSource = new SupplierBanks().GetList();
            CboBanks.DisplayMember = "Description";
            CboBanks.SelectedIndex = -1;
            CboBanks.ValueMember = "BankID";
            //! Countries
            CboCountries.DataSource = new SupplierCountries().GetList();
            CboCountries.DisplayMember = "Description";
            CboCountries.SelectedIndex = -1;
            CboCountries.ValueMember = "CountryID";

            CboTypeAccount.DataSource = Enum.GetValues(typeof(TypeAccount));
            CboTypeAccount.SelectedIndex = -1;
            //CboCountries.ValueMember = "TypeAccount";

            //! Grid Principal
            rFachada.CargarGrid(Grid);
            LlenarGrid();


            //! Set Supplier 
            if (Current["SupplierID"] != DBNull.Value)
            {
                var rut = Current["SupplierID"].ToString();
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    if (Current["SupplierID"].ToString() == Grid.Rows[myRowIndex].Cells["SupplierID"].Value.ToString())
                    {
                        Grid.SetCurRow(myRowIndex);
                        Grid.Rows[myRowIndex].EnsureVisible();
                    }
                }
            }


            //! Eventos
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;
            Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;
            Grid.CellMouseDown += Grid_CellMouseDown;
            //Grid.CellMouseUp += Grid_CellMouseUp;
            Grid.CellEllipsisButtonClick += Grid_CellEllipsisButtonClick;


        }

        public void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                rFachada.DeleteSupplier(current["SupplierID"].ToString());
                Grid.Focus();

                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            Grid.DrawAsFocused = false;
        }

        public void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {
                var vista = rFachada.GetVistaSuppliers(Current);
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //! Data Bound  ***!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                    Grid.Rows[myRowIndex].Cells["nro"].Value = myRowIndex + 1;
                    //! 
                    if (Current["SupplierID"] != DBNull.Value && Current["SupplierID"].ToString() == Grid.Rows[myRowIndex].Cells["SupplierID"].Value.ToString())
                    {
                        Grid.Rows[myRowIndex].EnsureVisible();
                        Grid.SetCurRow(myRowIndex);
                    }
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



        private void Grid_ColDividerDoubleClick(object sender, TenTec.Windows.iGridLib.iGColDividerDoubleClickEventArgs e)
        {

            //Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool ValidarControles()
        {

            if (!IsValidEmail(TxtEmail.Text))
            {
                return false;
            }
            //else if (!new ValidadorRut().ValidaRut(TxtRut.Text))
            //{
            //    return false;
            //}
            else if (string.IsNullOrEmpty(TxtRut.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(TxtName.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(TxtNroAccount.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(TxtAddress.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(TxtGiro.Text))
            {
                return false;
            }
            else if (CboCountries.SelectedIndex == -1)
            {
                return false;
            }
            else if (CboTypeAccount.SelectedIndex == -1)
            {
                return false;
            }
            else if (CboBanks.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void ClearControles()
        {
            TxtName.Text = string.Empty;
            TxtNroAccount.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            TxtRut.Text = string.Empty;
            TxtGiro.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtContact.Text = string.Empty;
            CboBanks.SelectedIndex = -1;
            CboCountries.SelectedIndex = -1;
            CboTypeAccount.SelectedIndex = -1;
        }

        public void SetControles()
        {
            //! Set Supplier 
            if (Current["SupplierID"] != DBNull.Value)
            {
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    if (TxtRut.Text.Trim() == Grid.Rows[myRowIndex].Cells["SupplierID"].Value.ToString())
                    {
                        Grid.SetCurRow(myRowIndex);
                        Grid.Rows[myRowIndex].EnsureVisible();
                    }
                }
            }
        }

        public iGrid GetGrid()
        {
            return Grid;
        }

        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (current != null)
            {
                TxtName.Text = UCase.ToTitleCase(current["Name"].ToString().ToLower());
                TxtRut.Text = current["SupplierID"].ToString();
                TxtAddress.Text = current["Address"].ToString();
                TxtGiro.Text = current["Giro"].ToString();
                CboCountries.SelectedValue = current["CountryID"].ToString();
                TxtNroAccount.Text = current["Account"].ToString();
                if (current["TypeAccount"] != DBNull.Value)
                {
                    CboTypeAccount.SelectedIndex = Convert.ToByte(current["TypeAccount"]) - 1;
                }
                CboBanks.SelectedValue = current["BankID"];
                TxtEmail.Text = current["Email"].ToString();
                TxtPhone.Text = current["Phone"].ToString();
                TxtContact.Text = current["ContactName"].ToString();
            }
        }

        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            throw new NotImplementedException();
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
            if (e.ColIndex == Grid.Cols["delete"].Index)
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

        private void Grid_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
        {
            if (Grid.CurCell != null && Grid.CurCell.ColIndex > -1)
            {
                var current = (DataRow)Grid.Rows[e.RowIndex].Tag;
                if (current != null)
                {
                    rFachada.UpdateItem(current["SupplierID"], Current, "SupplierID");
                    LlenarGrid();
                    ClearControles();
                    SetControles();
                    ((FPrincipal)Owner).GetGrid().CurRow = GuardarElPrevioCurrent;
                    //! Cerrar el Form al seleccionar el Cliente.
                    Close();
                }
            }
        }

        private void BtnNewSupplier_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                var s = new Suppliers
                {
                    Name = UCase.ToTitleCase(TxtName.Text.Trim().ToLower()),
                    Account = TxtNroAccount.Text.Trim(),
                    TypeAccount = Convert.ToByte(CboTypeAccount.SelectedIndex + 1),
                    Email = TxtEmail.Text.Trim(),
                    Address = UCase.ToTitleCase(TxtAddress.Text.Trim()),
                    BankID = CboBanks.SelectedValue.ToString(),
                    CountryID = CboCountries.SelectedValue.ToString(),
                    Giro = UCase.ToTitleCase(TxtGiro.Text.Trim()),
                    Phone = TxtPhone.Text,
                    ContactName = UCase.ToTitleCase(TxtContact.Text.Trim().ToLower()),
                    SupplierID = TxtRut.Text.Split('-')[0]
                };
                rFachada.InsertSupplier(s, this);
            }
        }
    }
}
