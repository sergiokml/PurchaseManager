using System;
using System.Globalization;
using System.Windows.Forms;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Formularios
{
    public partial class FAttach : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;

        public TextInfo UCase { get; set; }

        public FAttach(PerfilFachada rFachada)
        {
            this.rFachada = rFachada;
            InitializeComponent();
        }

        public bool ValidarControles()
        {
            if (string.IsNullOrEmpty(TxtName.Text))
            {
                return false;
            }
            return true;
        }

        public void ClearControles()
        {
            TxtName.Text = string.Empty;
        }

        public void SetControles()
        {
            throw new NotImplementedException();
        }

        private void FAttachment_Load(object sender, EventArgs e)
        {

            // Grid.SetGridProperties();
            FillGrid();
        }

        //public void GridOpenBtn_CellButtonClick(object sender, GridAuxButton.iGCellButtonClickEventArgs e)
        //{
        //    if (e.ColIndex == Grid.Cols["open"].Index)
        //    {
        //        //Attachment att = rContext.PrCurrent.Attachments
        //        //    .FirstOrDefault(c => c.Id_Attach == Convert.ToInt32(Grid.Cols["id"].Cells[e.RowIndex].Value));
        //        //System.Diagnostics.Process.Start(att.PathFile);
        //    }
        //}

        //public void GridDeleteButton_CellButtonClick(object sender, GridAuxButton.iGCellButtonClickEventArgs e)
        //{
        //    if (e.ColIndex == Grid.Cols["delete"].Index)
        //    {
        //        //rContext.DeleteAttach(Convert.ToInt32(Grid.Cols["id"].Cells[e.RowIndex].Value));
        //        //ClearControles();
        //        // FillGrid();
        //    }
        //}
        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                OpenFileDialog openF = new OpenFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    //Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*",
                    Filter = "pdf files (*.pdf)|*.pdf",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };
                //  if (openF.ShowDialog() == DialogResult.OK)
                //{
                //    string serverfolder = @"C:\PurshaseCtrl\Files\";
                //    serverfolder += $"{rContext.PrCurrent.Id_PrHeader}";

                //    if (!Directory.Exists(serverfolder))
                //    {
                //        Directory.CreateDirectory(serverfolder);
                //    }
                //    Attachment att = new Attachment
                //    {
                //        NameFile = TxtName.Text.Trim(),
                //        PathFile = $"{serverfolder}{@"\"}{openF.SafeFileName}"
                //    };
                //    rContext.InsertAttach(att);
                //    File.Copy(openF.FileName.Trim(), att.PathFile, true);
                //    FillGrid();
                //    ClearControles();
                // }
            }
        }

        public void FillGrid()
        {
            //Grid.Rows.Clear();
            //int nro = 1;
            //foreach (Attachment item in rContext.PrCurrent.Attachments)
            //{
            //    iGRow myRow = Grid.Rows.Add();
            //    myRow.Cells["nro"].Value = nro;
            //    myRow.Cells["id"].Value = item.Id_Attach;
            //    myRow.Cells["name"].Value = item.NameFile;
            //    nro++;
            //    myRow.Cells["delete"].ImageIndex = 2;
            //}
        }

        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {

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

        //public void GridAttachBtn_CellButtonClick(object sender, GridAuxButton.iGCellButtonClickEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
