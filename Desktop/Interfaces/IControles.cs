using System.Globalization;

namespace PurchaseDesktop.Interfaces
{
    internal interface IControles
    {
        // int HeaderID { get; set; }
        TextInfo UCase { get; set; }
        bool ValidarControles();
        void ClearControles();
        void SetControles();
        void LlenarGrid();
    }
}
