using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Fachadas;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Formularios
{
    public partial class FDeliverys : Form, IControles, IGridCustom
    {
        private readonly Fachada rFachada;
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public DataRow Current { get; set; }
        public Users CurrentUser { get; set; }
        public iGRow GuardarElPrevioCurrent { get; set; }

        public FDeliverys(Fachada rFachada, DataRow dr)
        {
            this.rFachada = rFachada;
            Current = dr;
            InitializeComponent();
        }

        public bool ValidarControles()
        {

            if (string.IsNullOrEmpty(TxtTitle.Text))
            {
                return false;
            }

            return true;
        }

        public void ClearControles()
        {
            TxtTitle.Text = string.Empty;
        }

        public void SetControles()
        {
            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            int status = Convert.ToInt32(Current["StatusID"]);
            switch (td)
            {
                case TypeDocumentHeader.PR:

                    break;
                case TypeDocumentHeader.PO:
                    if (status >= 2)
                    {
                        BtnNewNote.Enabled = false;
                        DatePicker.Enabled = false;
                        TxtTitle.ReadOnly = true;
                    }
                    break;
                default:
                    break;
            }
            //DatePicker.Format = DateTimePickerFormat.Custom;
            //DatePicker.CustomFormat = "dd/MM/yyyy";
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Id");
            //dt.Columns.Add("Name");
            //DataRow row = dt.NewRow();
            //row[0] = 1;
            //row[1] = "Public";
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[0] = 2;
            //row[1] = "Private";
            //dt.Rows.Add(row);


            //CboTypeFile.DisplayMember = "Name";
            //CboTypeFile.ValueMember = "Id";
        }
        public iGrid GetGrid()
        {
            return Grid;
        }
        private void FAttachment_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            SetControles();
            //! Grid Principal
            rFachada.FachadaGrid.CargarGrid(Grid);
            LlenarGrid();

            //! Eventos
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;
            Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;
            Grid.CellEllipsisButtonClick += Grid_CellEllipsisButtonClick;
        }


        public void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {
                DataTable vista = rFachada.FachadaViewForm.GetVistaDelivery(Current);
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //!Data Bound * **!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    Grid.Rows[i].Cells["nro"].Value = i + 1;
                }
                rFachada.FachadaGrid.FormatearGrid();
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
            throw new NotImplementedException();
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            //if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
            //{
            //    e.Result = iGEditResult.Cancel;
            //    return;
            //}
            //if (Equals(e.NewValue, Grid.Cells[e.RowIndex, e.ColIndex].Value))
            //{
            //    e.Result = iGEditResult.Cancel;
            //    return;
            //}
            ////! Update solo si cambió el dato.
            //System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            //var resultado = rFachada.UpdateNote(e.NewValue, current, Current, Grid.Cols[e.ColIndex].Key);
            //if (resultado == "OK")
            //{
            //    LlenarGrid();
            //    SetControles();
            //}
            //else
            //{
            //    e.Result = iGEditResult.Cancel;
            //    ((FPrincipal)Owner).Msg(resultado, MsgProceso.Warning);
            //}

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        public void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            Grid.Focus();
            DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                var resultado = rFachada.FachadaDeliverys.DeleteDelivery(current, Current);
                if (resultado == "OK")
                {
                    LlenarGrid();
                    ClearControles();
                    SetControles();
                }
                else
                {
                    ((FPrincipal)Owner).Msg(resultado, MsgProceso.Warning);
                }
            }
            //Grid.Focus();
            Grid.DrawAsFocused = false;
            System.Windows.Forms.Cursor.Current = Cursors.Default;
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

        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnNewNote_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                OrderDelivery del = new OrderDelivery
                {
                    Description = TxtTitle.Text.Trim(),
                    Date = DatePicker.Value,
                };
                var resultado = rFachada.FachadaDeliverys.InsertDelivery(del, Current);
                if (resultado == "OK")
                {
                    LlenarGrid();
                    ClearControles();
                    SetControles();
                    ((FPrincipal)Owner).LlenarGrid();
                    ((FPrincipal)Owner).SetControles();
                    ((FPrincipal)Owner).GetGrid().CurRow = GuardarElPrevioCurrent;
                }
                else
                {
                    ((FPrincipal)Owner).Msg(resultado, MsgProceso.Warning);
                }
            }
        }
    }
}
