using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Fachadas;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Formularios
{
    public partial class FHitos : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public DataRow Current { get; set; }
        public Users CurrentUser { get; set; }
        public iGRow GuardarElPrevioCurrent { get; set; }

        public FHitos(PerfilFachada rFachada, DataRow dr)
        {
            this.rFachada = rFachada;
            Current = dr;
            InitializeComponent();
        }

        public bool ValidarControles()
        {
            if (string.IsNullOrEmpty(TxtDescription.Text))
            {
                return false;
            }
            else if (CboDays.SelectedIndex == -1)
            {
                return false;
            }
            else if (TrackBar.Value == 0)
            {
                return false;
            }
            return true;
        }

        public void ClearControles()
        {
            TxtDescription.Text = string.Empty;
            LblMensaje.Text = $"{TrackBar.Value} %";
            CboDays.SelectedIndex = -1;

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
                        BtnNewDetail.Enabled = false;
                        CboDays.Enabled = false;
                        TxtDescription.ReadOnly = true;
                        TrackBar.Enabled = false;
                    }
                    break;
                default:
                    break;
            }

        }
        public iGrid GetGrid()
        {
            return Grid;
        }
        private void FAttachment_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            SetControles();
            //! Combobox días
            CboDays.DataSource = new List<short>() { 0, 5, 10, 15, 30, 45, 60, 90, 120, 150, 180 };


            //TrackBar.TickFrequency = 5;
            //TrackBar.SmallChange = 5;
            //TrackBar.LargeChange = 5;

            //! Grid Principal
            rFachada.CargarGrid(Grid);
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
                DataTable vista = rFachada.FachadaViewForm.GetVistaHitos(Current);
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //!Data Bound * **!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                int porcent = 0;
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    Grid.Rows[i].Cells["nro"].Value = $"Hito {i + 1}";
                    porcent += Convert.ToByte(Grid.Rows[i].Cells["Porcent"].Value);
                }
                TrackBar.Value = TrackBar.Minimum;
                TrackBar.Maximum = 100 - porcent;
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
            var resultado = rFachada.FachadaHitos.UpdateHito(e.NewValue, current, Current, Grid.Cols[e.ColIndex].Key);
            if (resultado == "OK")
            {
                LlenarGrid();
                SetControles();
                ((FPrincipal)Owner).LlenarGrid();
                ((FPrincipal)Owner).SetControles();
                ((FPrincipal)Owner).GetGrid().CurRow = GuardarElPrevioCurrent;
            }
            else
            {
                e.Result = iGEditResult.Cancel;
                ((FPrincipal)Owner).Msg(resultado, MsgProceso.Warning);
            }

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnNewHito_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                var h = new OrderHitos
                {
                    Description = UCase.ToTitleCase(TxtDescription.Text.Trim().ToLower()),
                    Days = Convert.ToByte(CboDays.SelectedValue),
                    Porcent = Convert.ToByte(TrackBar.Value)

                };
                var resultado = rFachada.FachadaHitos.InsertHito(h, Current);
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
                var resultado = rFachada.FachadaHitos.DeleteHito(current, Current);
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

        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            LblMensaje.Text = $"{TrackBar.Value} %";
        }


    }
}
