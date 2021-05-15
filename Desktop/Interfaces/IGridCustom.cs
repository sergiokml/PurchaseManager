
using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Interfaces
{
    internal interface IGridCustom
    {
        void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e);
        void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e);

        void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e);
        void Grid_CustomDrawCellEllipsisButtonForeground(object sender, iGCustomDrawEllipsisButtonEventArgs e);
        void Grid_CustomDrawCellEllipsisButtonBackground(object sender, iGCustomDrawEllipsisButtonEventArgs e);

    }
}
