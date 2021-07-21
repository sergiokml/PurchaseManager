using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Fachadas;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

using static PurchaseDesktop.Helpers.HFunctions;

namespace PurchaseDesktop.Formularios
{
    public partial class FAttach : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public DataRow Current { get; set; }
        //public Users CurrentUser { get; set; }
        private string FilePath { get; set; }
        public iGRow GuardarElPrevioCurrent { get; set; }


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
            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            int status = Convert.ToInt32(Current["StatusID"]);
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    if (status >= 2)
                    {
                        BtnNewDetail.Enabled = false;
                        TxtNameFile.ReadOnly = true;
                        TxtPathFile.ReadOnly = true;
                        CboTypeFile.Enabled = false;


                    }
                    break;
                case TypeDocumentHeader.PO:
                    if (status >= 2)
                    {
                        BtnNewDetail.Enabled = false;
                        TxtNameFile.ReadOnly = true;
                        TxtPathFile.ReadOnly = true;
                        CboTypeFile.Enabled = false;


                    }
                    break;
                default:
                    break;
            }
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
            CboTypeFile.DataSource = Enum.GetValues(typeof(TypeAttach));
            CboTypeFile.SelectedIndex = -1;
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
                DataTable vista = rFachada.FachadaViewForm.GetVistaAttaches(Current);
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
            var resultado = rFachada.FachadaAttach.UpdateAttach(e.NewValue, current, Current, Grid.Cols[e.ColIndex].Key);
            if (resultado == "OK")
            {
                LlenarGrid();
                SetControles();
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

        private void BtnNewDetail_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                Enum.TryParse<TypeAttach>(CboTypeFile.SelectedValue.ToString(), out TypeAttach status);
                Attaches att = new Attaches
                {
                    Description = UCase.ToTitleCase(TxtNameFile.Text.Trim().ToLower()),
                    FileName = $"{@"\"}{Path.GetFileName(TxtPathFile.Text)}",
                    Modifier = (byte)status
                };
                var resultado = rFachada.FachadaAttach.InsertAttach(att, Current, FilePath);
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
                var resultado = rFachada.FachadaAttach.DeleteAttach(current, Current);
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
            else if (Grid.Cols["view"].Index == e.ColIndex)
            {
                var resultado = rFachada.FachadaAttach.OpenAttach(current, Current);
                if (resultado != "OK")
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
            else if (e.ColIndex == Grid.Cols["view"].Index)
            {
                Rectangle myBounds = e.Bounds;
                if (myBounds.Width > 0 || myBounds.Height > 0)
                {
                    var ext = Grid.Rows[e.RowIndex].Cells["Extension"].Value.ToString();
                    switch (ext)
                    {
                        case "pdf":
                            Grid.ImageList.Draw(e.Graphics, myBounds.Location, 3);
                            break;
                        case "xls":
                            Grid.ImageList.Draw(e.Graphics, myBounds.Location, 7);
                            break;
                        case "xlsx":
                            Grid.ImageList.Draw(e.Graphics, myBounds.Location, 7);
                            break;
                        case "xlsm":
                            Grid.ImageList.Draw(e.Graphics, myBounds.Location, 7);
                            break;
                        case "doc":
                            Grid.ImageList.Draw(e.Graphics, myBounds.Location, 6);
                            break;
                        case "docx":
                            Grid.ImageList.Draw(e.Graphics, myBounds.Location, 6);
                            break;
                        default:
                            break;
                    }
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


        private void TxtPathFile_TextChange(object sender, EventArgs e)
        {
            TxtPathFile.Font = new Font("Tahoma", 7, FontStyle.Regular);
            TxtPathFile.SelectionStart = 0;
        }

        private void TxtPathFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openF = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Pdf files (*.pdf)|*.pdf" +
                        "|Word files (*.doc,*.docx)|*.doc;*.docx" +
                        "|Excel files (*.xls,*.xlsx,*.xlsm)|*.xls;*.xlsx;*.xlsm"

            };
            if (openF.ShowDialog() == DialogResult.OK)
            {
                FilePath = openF.FileName.Trim();
                TxtPathFile.Text = FilePath;
            }
        }
    }
}
