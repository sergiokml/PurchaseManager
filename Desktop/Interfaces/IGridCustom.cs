
using TenTec.Windows.iGridLib;

namespace PurchaseCtrl.Desktop.Interfaces
{
    internal interface IGridCustom
    {
        //GridAuxButton GridDeleteBtn { get; set; }
        //GridAuxButton GridAttachBtn { get; set; }

        void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e);
        void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e);
        void Grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e);
        //void GridDeleteButton_CellButtonClick(object sender, GridAuxButton.iGCellButtonClickEventArgs e);

    }
}
