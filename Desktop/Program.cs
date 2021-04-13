using System;
using System.Windows.Forms;

using PurchaseData.DataModel;

namespace Desktop
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            using (var contextDB = new PurchaseManagerContext())
            {
                var PR = "25406408";

                var user = contextDB.OrderUsers.Find(PR);
                contextDB.Entry(user).Reference(c => c.UserProfiles).Load();




            }

        }
    }
}
