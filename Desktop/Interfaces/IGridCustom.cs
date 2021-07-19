
using System.Data;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Interfaces
{
    internal interface IGridCustom
    {
        DataRow Current { get; set; }
        iGrid GetGrid();
        iGRow GuardarElPrevioCurrent { get; set; }
        void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e);
        void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e);
        void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e);
        void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e);
        void Grid_CustomDrawCellEllipsisButtonForeground(object sender, iGCustomDrawEllipsisButtonEventArgs e);
        void Grid_CustomDrawCellEllipsisButtonBackground(object sender, iGCustomDrawEllipsisButtonEventArgs e);
        void Grid_CellEllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e);

    }
}
