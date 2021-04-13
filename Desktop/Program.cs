using System;
using System.Collections.Generic;
using System.Linq;
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
                CargarUPR(10);
                //CargaUPO(4, "13779971"); // Booorador PO (Po user)
            }
        }

        private static void CargarUPR(int veces)
        {
            List<OrderAccounts> acc;
            List<OrderCompanies> companies;
            List<OrderUsers> users;
            using (PurchaseManagerContext contextDB = new PurchaseManagerContext())
            {
                users = contextDB.OrderUsers.Where(c => c.ProfileID == "UPR").ToList();
                companies = contextDB.OrderCompanies.ToList();
                acc = contextDB.OrderAccounts.ToList();
                foreach (OrderUsers user in users)
                {
                    for (int i = 0; i < veces; i++)
                    {
                        var n = new Random(0).Next(4);
                        OrderHeader pr = new OrderHeader
                        {
                            Description = $"Purchase Requisition N°{i + 1} [Borrador]",
                            Type = (byte)n,
                            StatusID = 1, // 1: Borrrador PR                        
                            CompanyID = companies[new Random().Next(companies.Count())].CompanyID,
                        };

                        for (int y = 0; y < 2; y++)
                        {
                            OrderDetails detail = new OrderDetails
                            {
                                AccountID = acc[new Random().Next(acc.Count())].AccountID,
                                Qty = new Random(y).Next(10),
                                NameProduct = "Producto Foo",
                                DescriptionProduct = "Description Bar Lorem ipsum dolor sit amet"
                            };
                            // Thread.Sleep(500);
                            pr.OrderDetails.Add(detail);
                        }
                        //! no se puede crear directo el dato de fecha en sql porque al crear la entidad así te pide el dato por aca.
                        // ! esta fecha se debe conseguir desde un método por ejemplo en savewait.!           

                        OrderTransactions tran = new OrderTransactions
                        {
                            Event = "CREATE_PR",
                            DateTran = contextDB.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single(),
                            UserID = user.UserID,
                            StatuID = 1
                        };
                        pr.OrderTransactions.Add(tran);
                        OrderAttaches att = new OrderAttaches
                        {
                            Description = $"Archivo_autocad_2018_A{new Random(i).Next(100)}",
                            FileName = @"C:\PurshaseCtrl\Files\Licencia_registro_animal_137182_1616120103375.pdf"
                        };
                        pr.OrderAttaches.Add(att);
                        contextDB.OrderHeader.Add(pr);
                        contextDB.SaveChanges();
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
    }
}
