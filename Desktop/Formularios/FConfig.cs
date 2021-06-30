using System;
using System.Security.Cryptography;
using System.Windows.Forms;

using PurchaseData.DataModel;

namespace PurchaseDesktop.Formularios
{
    public partial class FConfig : Form
    {
        private readonly Users user;
        public FConfig(Users user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FConfig_Load(object sender, EventArgs e)
        {
            TxtFirstName.Text = user.FirstName;
            TxtlastName.Text = user.LastName;
            TxtEmail.Text = user.Email;
            //string hashedpassword = Convert.ToBase64String(user.Password);
            //TxtPassword.Text = user.Password;
            TxtPosition.Text = user.Position;
            TxtUserID.Text = user.UserID;

            //byte[] data = new byte[DATA_SIZE];
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(user.Password);


        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            using (var contextDB = new PurchaseManagerEntities())
            {
                contextDB.UPDATE_PASSWORD_HASH(TxtPassword.Text.Trim(), user.UserID);
            }
        }
    }
}
