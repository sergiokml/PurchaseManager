using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

using PurchaseData.DataModel;

namespace PurchaseDesktop.Formularios
{
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }

        public Users UserDB { get; set; }
        public bool UserSuccessfullyAuthenticated { get; private set; }

        // PRIVATE PROP
        private IList<Users> list = new List<Users>();

        private static readonly Encoding Encoding1252 = Encoding.GetEncoding(1252);



        private void TxtUser_Leave(object sender, EventArgs e)
        {
            GetUser();
        }

        private void GetUser()
        {
            UserDB = list.FirstOrDefault(x => x.UserID == TxtID.Text.Trim());
            if (UserDB != null)
            {
                TxtPassword.Visible = true;
                LblBienvenida.Text = $"Welcome, {UserDB.FirstName} { UserDB.LastName}";
                TxtPassword.Focus();
            }
            else
            {
                TxtID.Text = "";
                TxtPassword.Visible = false;
                TxtID.Focus();
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrEmpty(TxtID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                return false;
            }
            byte[] txtPassword = SHA1HashValue(TxtPassword.Text);
            if (txtPassword.Length == UserDB.Password.Length)
            {
                int i = 0;
                while ((i < txtPassword.Length) && (txtPassword[i] == UserDB.Password[i]))
                {
                    i += 1;
                }
                if (i == txtPassword.Length) { return true; }
            }
            return false;
        }

        private static byte[] SHA1HashValue(string s)
        {
            byte[] bytes = Encoding1252.GetBytes(s);

            SHA512 sha1 = SHA512.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return hashBytes;
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icons8_survey;
            list = new Users().GetList();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0.0)
            {
                //Opacity -= 0.025;
                Opacity -= 0.5;
            }
            else
            {
                TimerFLogin.Stop();
                Close();
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (TxtPassword.Visible == true && Validar())
            {
                TimerFLogin.Start();
                UserSuccessfullyAuthenticated = true;
            }
        }

        private void BtnLogin_Click_1(object sender, EventArgs e)
        {
            if (UserDB != null)
            {
                if (Validar())
                {
                    TimerFLogin.Start();
                    UserSuccessfullyAuthenticated = true;
                }
            }
            else
            {
                TxtPassword.Text = "";
                GetUser();
            }
        }
    }
}