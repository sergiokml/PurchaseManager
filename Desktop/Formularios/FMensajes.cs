using System.Text;
using System.Windows.Forms;

namespace PurchaseDesktop.Formularios
{
    public partial class FMensajes : Form
    {
        public FPrincipal fPrincipal { get; set; }
        public DialogResult Resultado { get; set; }
        public StringBuilder Mensaje { get; set; }

        public FMensajes(FPrincipal fPrincipal)
        {
            InitializeComponent();
            this.fPrincipal = fPrincipal;
            ShowInTaskbar = false;
            Owner = fPrincipal;
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            Resultado = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            //((FPrincipal)Owner).Msg("Operation Cancelled.", MsgProceso.Warning);
            Resultado = DialogResult.Cancel;
            Close();
        }

        private void BtnCerrar_Click(object sender, System.EventArgs e)
        {
            Resultado = DialogResult.Cancel;
            Close();
        }

        private void FMensajes_Load(object sender, System.EventArgs e)
        {
            LblMensaje.Text = Mensaje.ToString();
        }
    }
}
