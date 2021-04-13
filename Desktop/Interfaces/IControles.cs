using System.Globalization;

namespace PurchaseDesktop.Interfaces
{
    internal interface IControles
    {
        TextInfo UCase { get; set; }
        bool ValidarControles();
        void ClearControles();
        void SetControles();
    }
}
