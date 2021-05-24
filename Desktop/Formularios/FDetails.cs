﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
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
        public TextInfo UCase { get; set; }
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
            rFachada.CargarGrid(Grid, "FDetails");
            LlenarGrid();

            //! Eventos
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
                RequisitionDetails rd;
                OrderDetails od;
                switch (p)
                {
                    case Perfiles.ADM:
                        break;
                    case Perfiles.BAS:
                        break;
                    case Perfiles.UPR:
                        rd = new RequisitionDetails
                        {
                            AccountID = ((Accounts)CboAccount.SelectedItem).AccountID,
                            Qty = Convert.ToInt32(TxtQty.Text),
                            NameProduct = TxtName.Text.Trim(),
                            DescriptionProduct = TxtDescription.Text.Trim(),
                            HeaderID = Convert.ToInt32(Current["HeaderID"]),
                            MedidaID = ((Medidas)CboMedidas.SelectedItem).MedidaID

                        };
                        if (rFachada.InsertDetail(rd, Current))
                        {
                            LlenarGrid();
                            ClearControles();
                            SetControles();
                        }

                        break;
                    case Perfiles.VAL:
                        break;
                    case Perfiles.UPO:
                        od = new OrderDetails
                        {
                            AccountID = ((Accounts)CboAccount.SelectedItem).AccountID,
                            Qty = Convert.ToInt32(TxtQty.Text.Replace(".", "")),
                            NameProduct = TxtName.Text.Trim(),
                            DescriptionProduct = TxtDescription.Text.Trim(),
                            HeaderID = Convert.ToInt32(Current["HeaderID"]),
                            Price = Convert.ToInt32(TxtPrice.Text.Replace(".", "")),
                            MedidaID = ((Medidas)CboMedidas.SelectedItem).MedidaID

                        };
                        List<OrderDetails> ods = new OrderDetails().GetListByID(Convert.ToInt32(Current["HeaderID"]));
                        int suma = ods.Sum(s => s.Total) + (od.Price * od.Qty);
                        if (suma < 1000000000 && suma > 0)
                        {
                            if (rFachada.InsertDetail(od, Current))
                            {
                                LlenarGrid();
                                ClearControles();
                                SetControles();
                            }
                        }

                        break;
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
            var user = rFachada.CurrentUser();
            Enum.TryParse(user.ProfileID, out Perfiles p);

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

            switch (p)
            {
                case Perfiles.ADM:
                    break;
                case Perfiles.BAS:
                    break;
                case Perfiles.UPO:
                    if (!int.TryParse(TxtPrice.Text.Replace(".", ""), out int parsedValue))
                    {
                        return false;
                    }
                    break;
                case Perfiles.UPR:
                    break;
                case Perfiles.VAL:
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
            CboAccount.DataSource = new Accounts().GetList();
            CboAccount.DisplayMember = "Description";
            CboAccount.SelectedIndex = -1;
            CboAccount.ValueMember = "AccountID";

            CboMedidas.DataSource = new Medidas().GetList();
            CboMedidas.DisplayMember = "Description";
            CboMedidas.SelectedIndex = -1;
            CboMedidas.ValueMember = "MedidaID";

            //!Update totales en header
            List<OrderDetails> ods = new OrderDetails().GetListByID(Convert.ToInt32(Current["HeaderID"]));
            int suma = ods.Sum(s => s.Total);
            rFachada.UpdateItem(suma, Current, "Net");
            //rFachada.UpdateItem(0, Current, "Exent");

            TxtTotal.Text = $"$ {suma.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}";
        }

        private void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            Grid.DrawAsFocused = true;
            var currentDetail = (DataRow)Grid.Rows[e.RowIndex].Tag;
            if (Grid.Cols["delete"].Index == e.ColIndex)
            {
                if (rFachada.DeleteDetail(currentDetail, Current))
                {
                    LlenarGrid();
                    ClearControles();
                    Grid.Focus();
                    Grid.DrawAsFocused = false;
                    SetControles();
                }
                else
                {
                    ((FPrincipal)Owner).Msg("No se puede eliminar.", FPrincipal.MsgProceso.Error);
                }
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
            throw new NotImplementedException();
        }

        public void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtQty_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtQty.Text))
                {
                    if (Convert.ToDouble(TxtQty.Text) == 0)
                    {
                        TxtQty.Text = string.Empty;
                    }
                    else
                    {
                        var d = Convert.ToDecimal(TxtQty.Text.Replace(".", ","));
                        TxtQty.Text = d.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"));
                        TxtQty.SelectionStart = TxtQty.Text.Length;
                    }
                }
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
    }
}
