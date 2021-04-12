using System.Globalization;

namespace PurchaseCtrl.Desktop.Interfaces
{
    internal interface IControles
    {
        TextInfo UCase { get; set; }
        bool ValidarControles();
        void ClearControles();
        void SetControles();
    }
}
