﻿using System;
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
    public partial class FDetails : Form, IControles, IGridCustom
    {
        private readonly PerfilFachada rFachada;
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;
        public DataRow Current { get; set; }

        public FDetails(PerfilFachada rFachada, DataRow dr)
        {
            this.rFachada = rFachada;
            Current = dr;
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FDetails_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            SetControles();
            //! Grid Principal
            rFachada.CargarGrid(Grid, "FDetails", Current);
            LlenarGrid();

            //! Eventos
            Grid.BeforeCommitEdit += Grid_BeforeCommitEdit;
            Grid.AfterCommitEdit += Grid_AfterCommitEdit;
            Grid.CustomDrawCellEllipsisButtonBackground += Grid_CustomDrawCellEllipsisButtonBackground;
            Grid.CustomDrawCellEllipsisButtonForeground += Grid_CustomDrawCellEllipsisButtonForeground;
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

        private void BtnNewDetail_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                Enum.TryParse(rFachada.CurrentUser().ProfileID, out Perfiles p);
                var resultado = string.Empty;
                Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
                switch (td)
                {
                    case TypeDocumentHeader.PR:
                        var rd = new RequisitionDetails
                        {
                            AccountID = ((Accounts)CboAccount.SelectedItem).AccountID,
                            Qty = Convert.ToInt32(TxtQty.Text.Replace(".", "")),
                            NameProduct = UCase.ToTitleCase(TxtName.Text.Trim().ToLower()),
                            DescriptionProduct = UCase.ToTitleCase(TxtDescription.Text.Trim().ToLower()),
                            HeaderID = Convert.ToInt32(Current["HeaderID"]),
                            MedidaID = ((Medidas)CboMedidas.SelectedItem).MedidaID

                        };
                        resultado = rFachada.InsertDetail(rd, Current);
                        break;
                    case TypeDocumentHeader.PO:
                        var od = new OrderDetails
                        {
                            AccountID = ((Accounts)CboAccount.SelectedItem).AccountID,
                            Qty = Convert.ToInt32(TxtQty.Text.Replace(".", "")),
                            NameProduct = TxtName.Text.Trim(),
                            DescriptionProduct = TxtDescription.Text.Trim(),
                            HeaderID = Convert.ToInt32(Current["HeaderID"]),
                            Price = Convert.ToInt32(TxtPrice.Text.Replace(".", "")),
                            MedidaID = ((Medidas)CboMedidas.SelectedItem).MedidaID
                        };
                        resultado = rFachada.InsertDetail(od, Current);
                        break;
                    default:
                        break;
                }
                if (resultado == "OK")
                {
                    LlenarGrid();
                    ClearControles();
                    SetControles();
                    ((FPrincipal)Owner).LlenarGrid();
                    ((FPrincipal)Owner).SetControles();
                }
                else
                {
                    ((FPrincipal)Owner).Msg(resultado, FPrincipal.MsgProceso.Warning);
                }
            }
        }

        public void LlenarGrid()
        {
            //! Grid Principal
            Grid.BeginUpdate();
            try
            {
                var vista = rFachada.GetVistaDetalles(Current);
                Grid.Rows.Clear();
                Grid.FillWithData(vista, true);
                //! Data Bound  ***!
                for (int myRowIndex = 0; myRowIndex < Grid.Rows.Count; myRowIndex++)
                {
                    Grid.Rows[myRowIndex].Tag = vista.Rows[myRowIndex];
                }
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    Grid.Rows[i].Cells["nro"].Value = i + 1;
                }
                Grid = rFachada.FormatearGrid(Grid, "FDetails", Current);
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
            Enum.TryParse(rFachada.CurrentUser().ProfileID, out Perfiles p);
            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            if (CboAccount.SelectedIndex == -1)
            {
                return false;
            }
            if (CboMedidas.SelectedIndex == -1)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(TxtName.Text))
            {
                return false;
            }
            else if (!double.TryParse(TxtQty.Text, out double parsedValue))
            {
                return false;
            }
            switch (td)
            {
                case TypeDocumentHeader.PR:
                    break;
                case TypeDocumentHeader.PO:
                    if (!int.TryParse(TxtPrice.Text.Replace(".", ""), out int parsedValue))
                    {
                        return false;
                    }
                    break;
                default:
                    break;
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
            Enum.TryParse(rFachada.CurrentUser().ProfileID, out Perfiles p);
            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);

            CboAccount.DataSource = new Accounts().GetList();
            CboAccount.DisplayMember = "Description";
            CboAccount.SelectedIndex = -1;
            CboAccount.ValueMember = "AccountID";

            CboMedidas.DataSource = new Medidas().GetList();
            CboMedidas.DisplayMember = "Description";
            CboMedidas.SelectedIndex = -1;
            CboMedidas.ValueMember = "MedidaID";

            switch (p)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            TxtPrice.Enabled = false;

                            break;
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(Convert.ToInt32(Current["HeaderID"]));
                            decimal neto = Convert.ToDecimal(po.Net);
                            decimal exent = Convert.ToDecimal(po.Exent);
                            decimal tax = Convert.ToDecimal(po.Discount);
                            decimal total = Convert.ToDecimal(po.Total);
                            if (neto > 0)
                            {
                                TxtNet.Text = neto.ToString("$#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                            }
                            else
                            {
                                TxtNet.Text = "$ 0";
                            }
                            if (exent > 0)
                            {
                                TxtExent.Text = exent.ToString("$#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                            }
                            else
                            {
                                TxtExent.Text = "$ 0";
                            }
                            if (tax > 0)
                            {
                                TxtTax.Text = tax.ToString("$#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                            }
                            else
                            {
                                TxtTax.Text = "$ 0";
                            }
                            if (total > 0)
                            {
                                TxtTotal.Text = total.ToString("$#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                            }
                            else
                            {
                                TxtTotal.Text = "$ 0";
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case Perfiles.UPR:

                    break;
                case Perfiles.VAL:
                    break;
                default:
                    break;
            }



        }

        private void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            var currentDetail = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                var resultado = rFachada.DeleteDetail(currentDetail, Current);
                if (resultado == "OK")
                {
                    LlenarGrid();
                    ClearControles();
                    Grid.Focus();

                    SetControles();
                    ((FPrincipal)Owner).LlenarGrid();
                    ((FPrincipal)Owner).SetControles();
                }
                else
                {
                    ((FPrincipal)Owner).Msg(resultado, FPrincipal.MsgProceso.Warning);
                }
                Grid.DrawAsFocused = false;
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
        {
            Grid.Header.Cells[e.RowIndex, e.ColIndex].Value = Grid.Cols[e.ColIndex].Width;
        }

        public void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (e.ColIndex > 0)
            {
                SetControles();
            }
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
            var resultado = rFachada.UpdateDetail(e.NewValue, current, Current, Grid.Cols[e.ColIndex].Key);
            if (resultado == "OK")
            {
                LlenarGrid();
                SetControles();
                ((FPrincipal)Owner).LlenarGrid();
                ((FPrincipal)Owner).SetControles();
            }
            else
            {
                e.Result = iGEditResult.Cancel;
                ((FPrincipal)Owner).Msg(resultado, FPrincipal.MsgProceso.Warning);
            }
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtQty_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (TxtQty.Text.Length > 0)
                {
                    var d = Convert.ToDecimal(TxtQty.Text.Replace(".", ","));
                    TxtQty.Text = d.ToString("#,0.##;;''", CultureInfo.GetCultureInfo("es-CL"));
                    TxtQty.SelectionStart = TxtQty.Text.Length;
                    foreach (char c in TxtQty.Text)
                    {
                        if (c < '0' || c > '9')
                        {
                            if (c != ',') //! Solo acepto comas
                            {
                                return;
                            }
                        }
                    }
                }
                //if (!string.IsNullOrEmpty(TxtQty.Text))
                //{
                //    if (Convert.ToDouble(TxtQty.Text) == 0)
                //    {
                //        TxtQty.Text = string.Empty;
                //    }
                //    else
                //    {
                //        var d = Convert.ToDecimal(TxtQty.Text.Replace(".", ","));
                //        TxtQty.Text = d.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"));
                //        TxtQty.SelectionStart = TxtQty.Text.Length;
                //    }
                //}
            }
            catch (FormatException)
            {
                //throw;
            }
        }

        private void TxtPrice_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtPrice.Text))
                {
                    if (Convert.ToDouble(TxtPrice.Text) == 0)
                    {
                        TxtPrice.Text = string.Empty;
                    }
                    else
                    {
                        var d = Convert.ToDecimal(TxtPrice.Text.Replace(".", ","));
                        TxtPrice.Text = d.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"));
                        TxtPrice.SelectionStart = TxtPrice.Text.Length;
                    }
                }
            }
            catch (FormatException)
            {
                //throw;
            }
        }

        private void Grid_RequestCellToolTipText(object sender, iGRequestCellToolTipTextEventArgs e)
        {
            if (Grid.Cols[e.ColIndex].Key == "NameProduct")
            {
                e.Text = Grid.Rows[e.RowIndex].Cells["DescriptionProduct"].Value.ToString();
            }
        }
    }
}
