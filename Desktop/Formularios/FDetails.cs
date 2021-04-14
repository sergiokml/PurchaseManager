using System.Windows.Forms;

using PurchaseDesktop.Helpers;

namespace PurchaseDesktop.Formularios
{
    public partial class FDetails : Form
    {
        private readonly PerfilFachada rFachada;
        public FDetails(Helpers.PerfilFachada rFachada)
        {
            this.rFachada = rFachada;
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
