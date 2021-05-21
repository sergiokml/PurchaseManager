using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Formularios
{
    public partial class FAttach : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;
        public TextInfo UCase { get; set; }
        public DataRow Current { get; set; }
        public Users CurrentUser { get; set; }

        private string FilePath { get; set; }

        public FAttach(PerfilFachada rFachada, DataRow dr)
        {
            this.rFachada = rFachada;
            Current = dr;
            InitializeComponent();
        }

        public bool ValidarControles()
        {
            if (string.IsNullOrEmpty(TxtPathFile.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(TxtNameFile.Text))
            {
                return false;
            }
            else if (CboTypeFile.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }

        public void ClearControles()
        {
            TxtPathFile.Text = string.Empty;
            TxtNameFile.Text = string.Empty;
            CboTypeFile.SelectedIndex = -1;
        }

        public void SetControles()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            DataRow row = dt.NewRow();
            row[0] = 1;
            row[1] = "Public";
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = 2;
            row[1] = "Private";
            dt.Rows.Add(row);


            CboTypeFile.DisplayMember = "Name";
            CboTypeFile.ValueMember = "Id";
            CboTypeFile.DataSource = dt;
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
            rFachada.CargarGrid(Grid, "FAttach");
            LlenarGrid();

            //! Eventos
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;

        }


        public void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {
                DataTable vista = rFachada.GetVistaAttaches(Current);
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
            throw new NotImplementedException();
        }

        public void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnNewDetail_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                string serverfolder = Properties.Settings.Default.FolderApp;
                serverfolder += $"{Convert.ToInt32(Current["HeaderID"])}";

                if (!Directory.Exists(serverfolder))
                {
                    Directory.CreateDirectory(serverfolder);
                }
                string ext = Path.GetExtension(FilePath); //.pdf
                Attaches att = new Attaches
                {
                    Description = TxtNameFile.Text.Trim(),
                    FileName = $"{serverfolder}{@"\"}{TxtNameFile.Text.Trim()}{ext}",
                    Modifier = Convert.ToByte(CboTypeFile.SelectedValue)
                };
                rFachada.InsertAttach(att, Convert.ToInt32(Current["HeaderID"]));
                File.Copy(FilePath, $"{serverfolder}{@"\"}{TxtNameFile.Text.Trim()}{ext}", true);
                LlenarGrid();
                ClearControles();
            }
        }

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        private void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            DataRow current = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                if (rFachada.BorrarAdjunto(current, Current))
                {
                    LlenarGrid();
                    ClearControles();
                    Grid.Focus();
                    Grid.DrawAsFocused = false;
                }
            }
            else if (Grid.Cols["view"].Index == e.ColIndex)
            {
                rFachada.VerAttach(current);
                Grid.Focus();
                Grid.DrawAsFocused = false;
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
                    Grid.ImageList.Draw(e.Graphics, myBounds.Location, 3);
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

        private void TxtPathFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openF = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "All files (*.*)|*.*|pdf files (*.pdf)|*.pdf",
                //Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openF.ShowDialog() == DialogResult.OK)
            {
                FilePath = openF.FileName.Trim();
                TxtPathFile.Text = FilePath;
            }
        }
    }
}
