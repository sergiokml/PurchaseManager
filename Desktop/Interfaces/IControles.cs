using System.Globalization;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Interfaces
{
    internal interface IControles
    {
        int ItemID { get; set; }
        TextInfo UCase { get; set; }
        int ItemStatus { get; set; }
        bool ValidarControles();
        void ClearControles();
        void SetControles();
        iGrid GetGrid();
        void LlenarGrid();

    }
}
