using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

using PurchaseData.DataModel;

using PurchaseDesktop.Formularios;
using PurchaseDesktop.Helpers;
using PurchaseDesktop.Profiles;

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

            //! Internet?
            if (CheckForInternetConnection())
            {
                //! Los Perfiles: 
                UserProfileUPO perfilPo = new UserProfileUPO(new PurchaseManagerEntities());
                UserProfileUPR perfilPr = new UserProfileUPR(new PurchaseManagerEntities());
                UserProfileVAL perfilVal = new UserProfileVAL(new PurchaseManagerEntities());
                Users user;
                using (var contextDB = new PurchaseManagerEntities())
                {
                    //var P = "14720891"; // PR Jhoana

                    var P = "15332723"; // PO (TODAS LAS PR)
                    //var P = "15960233"; // VAL DEV 

                    //var P = "16003040"; // FCA "ADM VALIDADOR TODOS" 

                    user = contextDB.Users.Find(P);
                    //contextDB.Entry(user).Reference(c => c.UserProfiles).Load();
                    //CargarUPR(10);
                    //CargaUPO(4, "13779971"); // Booorador PO (Po user)
                }

                FLogin loginForm = new FLogin();
                Application.Run(loginForm);

                if (loginForm.UserSuccessfullyAuthenticated)
                {
                    PerfilFachada facade = new PerfilFachada(perfilPr, perfilPo, perfilVal, loginForm.UserDB);
                    FPrincipal f = new FPrincipal(facade);
                    Application.Run(f);
                }
            }
        }

        private static void CargarUPR(int veces)
        {
            List<Accounts> acc;
            List<Companies> companies;
            List<Users> users;
            using (PurchaseManagerEntities rContext = new PurchaseManagerEntities())
            {
                users = rContext.Users.Where(c => c.ProfileID == "UPR").ToList();
                users.OrderBy(c => c.UserID).ToList();
                companies = rContext.Companies.ToList();
                acc = rContext.Accounts.ToList();
                foreach (Users user in users)
                {
                    for (int i = 0; i < veces; i++)
                    {
                        var n = new Random(0).Next(4);
                        RequisitionHeader pr = new RequisitionHeader
                        {
                            Description = $"Purchase Requisition N°{i + 1}",
                            Type = (byte)n,
                            StatusID = 2, // 1: Borrrador PR                        
                            CompanyID = companies[new Random().Next(companies.Count())].CompanyID,
                        };
                        for (int y = 0; y < 2; y++)
                        {
                            RequisitionDetails detail = new RequisitionDetails
                            {
                                AccountID = acc[new Random().Next(acc.Count())].AccountID,
                                Qty = new Random(y).Next(10),
                                NameProduct = "Producto Foo",
                                DescriptionProduct = "Description Bar Lorem ipsum dolor sit amet",
                                MedidaID = "UN"
                            };
                            // Thread.Sleep(500);
                            pr.RequisitionDetails.Add(detail);
                        }
                        //! no se puede crear directo el dato de fecha en sql porque al crear la entidad así te pide el dato por aca.
                        // ! esta fecha se debe conseguir desde un método por ejemplo en savewait.!           
                        Transactions tran = new Transactions
                        {
                            Event = "CREATE_PR",
                            DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                            UserID = user.UserID
                        };
                        pr.Transactions.Add(tran);
                        //Attaches att = new Attaches
                        //{
                        //    Description = $"Archivo_autocad_2018_A{new Random(i).Next(100)}",
                        //    FileName = @"Licencia_registro_animal_137182_1616120103375.pdf"
                        //};
                        //pr.Attaches.Add(att);
                        rContext.RequisitionHeader.Add(pr);
                        rContext.SaveChanges();
                        // Thread.Sleep(500);
                        //var pragain = contextDB.OrderHeader.Find(pr.OrderHeaderID);
                        //OrderTransactions tran2 = new OrderTransactions
                        //{
                        //    Event = "UPDATE_PR",
                        //    DateTran = contextDB.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                        //    UserID = user.UserID,
                        //    StatuID = 2
                        //};
                        //pragain.OrderTransactions.Add(tran2);
                        //pragain.StatusID = 2; // 2: Vigente PR    
                        //contextDB.SaveChanges();
                        // Thread.Sleep(500);
                    }
                }
            }
        }

        //private static void CargaUPO(byte estado, string iduser)
        //{
        //    List<OrderHeader> orders;
        //    using (PurchaseCtrlEntities contextDB = new PurchaseCtrlEntities())
        //    {
        //        orders = contextDB.OrderHeaders.Where(c => c.IDOrderStatus == 3).ToList();
        //        int i = 1;
        //        foreach (var item in orders)
        //        {
        //            // var pragain = contextDB.OrderHeaders.Find(item.OrderHeaderID);
        //            //? ESTO DEBE IR EN UNA CLASE CUSTOM DE DBCONTESXT (VERSION CERO DE PURSAHSE)
        //            TranHistory tran = new TranHistory
        //            {
        //                TranHistoyDescription = "SOLO TEST",
        //                TranHistoyDate = contextDB.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
        //                IDUserDB = iduser,
        //                IDOrderStatus = estado
        //            };
        //            //orders[i].IDOrderStatus = estado;
        //            ////
        //            //contextDB.Entry(item).State = EntityState.Modified;
        //            //contextDB.SaveChanges();
        //            //item.TranHistories.Add(tran);
        //            //contextDB.SaveChanges();
        //            //pragain.TranHistories.Add(tran);
        //            //contextDB.TranHistories.Add(tran);
        //            contextDB.OrderHeaders.Attach(item).TranHistories.Add(tran);
        //            item.IDOrderStatus = estado;
        //            contextDB.SaveChanges();
        //            i++;

        //            //if (i == 5)
        //            //{
        //            //    return;
        //            //}

        //        }
        //    }
        //}
        //private static void CargarValidaciones(byte estado, string id, int veces)
        //{
        //    List<OrderHeader> orders;
        //    using (PurchaseCtrlEntities contextDB = new PurchaseCtrlEntities())
        //    {
        //        orders = contextDB.OrderHeaders.Where(c => c.IDOrderStatus == 4).ToList();
        //        int i = 1;
        //        foreach (var item in orders)
        //        {
        //            TranHistory tran = new TranHistory
        //            {
        //                TranHistoyDescription = "BORRADOR_PO",
        //                TranHistoyDate = contextDB.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
        //                IDUserDB = id,
        //                IDOrderStatus = estado
        //            };
        //            item.IDOrderStatus = estado;
        //            contextDB.Entry(item).State = EntityState.Modified;
        //            contextDB.SaveChanges();
        //            contextDB.OrderHeaders.Attach(item).TranHistories.Add(tran);
        //            contextDB.SaveChanges();
        //            i++;
        //            if (i == veces)
        //            {
        //                return;
        //            }
        //            break;
        //        }
        //    }

        //}

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url = "http://www.gstatic.com/generate_204";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
