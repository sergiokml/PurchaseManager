using System.Globalization;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Interfaces
{
    internal interface IControles
    {
        // int HeaderID { get; set; }
        TextInfo UCase { get; set; }
        bool ValidarControles();
        void ClearControles();
        void SetControles();
        iGrid GetGrid();
        void LlenarGrid();

    }
}
