//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Windows.Forms;

//using PurchaseData.DataModel;

//using PurchaseDesktop.Helpers;
//using PurchaseDesktop.Interfaces;

//using TenTec.Windows.iGridLib;

//namespace PurchaseDesktop.Profiles
//{
//    public class PerfilUPR_ : HFunctions, IPerfilActions
//    {
//        private readonly PurchaseManagerContext rContext;

//        public PerfilUPR_(PurchaseManagerContext rContext)
//        {
//            this.rContext = rContext;
//        }

//        public List<OrderDetails> DetailsPO { get; set; }
//        public List<RequisitionDetails> DetailsPR { get; set; }
//        public DataTable TableDetails { get; set; }
//        //public DataRow Current { get; set; }
//        public Users CurrentUser { get; set; }

//        public OrderHeader DocumentPO { get; set; }
//        public RequisitionHeader DocumentPR { get; set; }



//        public DataTable GetVistaFPrincipal()
//        {
//            using (var rContext = new PurchaseManagerContext())
//            {
//                var l = rContext.vRequisitionByMinTransaction
//              .Where(c => c.UserID == CurrentUser.UserID)
//              .OrderByDescending(c => c.DateLast).ToList();
//                return this.ToDataTable<vRequisitionByMinTransaction>(l);
//            }
//        }


//        public DataTable GetVistaDetalles()
//        {
//            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
//            var HeaderID = Convert.ToInt32(Current["HeaderID"]);
//            switch (td)
//            {
//                case TypeDocumentHeader.PR:
//                    return this.ToDataTable<RequisitionDetails>(rContext.RequisitionDetails
//             .Where(c => c.HeaderID == HeaderID).ToList());

//                case TypeDocumentHeader.PO:
//                    return this.ToDataTable<OrderDetails>(rContext.OrderDetails
//             .Where(c => c.HeaderID == HeaderID).ToList());
//            }
//            return null;
//        }


//        public void DeleteDetail(Users user)
//        {
//            var pr = rContext.RequisitionHeader.Find(Convert.ToInt32(Current["HeaderID"]));
//            var tran = new Transactions
//            {
//                Event = EventUserPR.DELETE_DETAIL.ToString(),
//                UserID = user.UserID,
//                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
//            };
//            var detail = rContext.RequisitionDetails.Find(Convert.ToInt32(Current["DetailID"]), pr.RequisitionHeaderID);
//            rContext.RequisitionDetails.Remove(detail);
//            pr.Transactions.Add(tran);
//            rContext.SaveChanges();
//        }


//        public void UpdateItemHeader(Users user, object valor, string campo)
//        {
//            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);

//            var HeaderID = Convert.ToInt32(Current["HeaderID"]);
//            switch (td)
//            {
//                case TypeDocumentHeader.PR:
//                    var pr = rContext.RequisitionHeader.Find(HeaderID);
//                    switch (campo)
//                    {
//                        case "Description":
//                            pr.Description = UCase.ToTitleCase(valor.ToString().ToLower());
//                            break;
//                        case "Type":
//                            pr.Type = Convert.ToByte(valor);
//                            break;
//                        case "StatusID":
//                            pr.StatusID = Convert.ToByte(valor);
//                            break;
//                        case "CompanyID":
//                            pr.CompanyID = valor.ToString();
//                            break;
//                        default:
//                            break;
//                    }
//                    var tran = new Transactions
//                    {
//                        Event = EventUserPR.UPDATE_PR.ToString(),
//                        UserID = user.UserID,
//                        DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
//                    };
//                    using (var trans = rContext.Database.BeginTransaction())
//                    {
//                        pr.Transactions.Add(tran);
//                        rContext.SaveChanges();
//                        trans.Commit();
//                    }
//                    break;
//                case TypeDocumentHeader.PO:
//                    break;
//            }

//        }









//        public DataTable GetVistaAttaches(int IdItem)
//        {
//            var pr = rContext.RequisitionHeader.Find(IdItem);
//            return this.ToDataTable<Attaches>(pr.Attaches.ToList());
//        }

//        public DataTable GetVistaFSupplier()
//        {
//            return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
//        }


//        public void DeleteItemHeader()
//        {
//            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
//            var HeaderID = Convert.ToInt32(Current["HeaderID"]);

//            var pr = rContext.RequisitionHeader.Find(HeaderID);
//            rContext.Transactions.RemoveRange(pr.Transactions);
//            rContext.RequisitionHeader.Remove(pr);
//            rContext.SaveChanges();
//        }


//        public void GetDetailsRequisition(int id)
//        {
//            var details = rContext.RequisitionHeader.Find(id).RequisitionDetails.ToList();
//            foreach (var item in details)
//            {
//                rContext.Entry(item).Reference(c => c.Accounts).Load();
//            }
//            //return details;
//        }

//        public List<Attaches> GetAttaches(int id)
//        {
//            return rContext.RequisitionHeader.Find(id).Attaches.ToList();
//        }

//        public void InsertItemHeader(Users userDB)
//        {

//            var tran = new Transactions
//            {
//                Event = EventUserPR.CREATE_PR.ToString(),
//                UserID = userDB.UserID,
//                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
//            };
//            DocumentPR.Transactions.Add(tran);
//            rContext.RequisitionHeader.Add(DocumentPR);
//            rContext.SaveChanges();
//        }

//        public void GetFunciones()
//        {
//            ReqGroupByCost_Results = rContext.ufnGetReqGroupByCost(2).ToList();
//            OrderGroupByStatus_Results = rContext.ufnGetOrderGroupByStatus().ToList();
//        }

//        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
//        {
//            var pr = rContext.RequisitionHeader.Find(idItem);
//            var tran = new Transactions
//            {
//                Event = EventUserPR.INSERT_DETAIL.ToString(),
//                UserID = userDB.UserID,
//                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
//            };
//            pr.Transactions.Add(tran);
//            pr.RequisitionDetails.Add(detail);
//            rContext.SaveChanges();
//        }

//        public void InsertAttach(int id, Attaches att, Users userDB)
//        {
//            var pr = rContext.RequisitionHeader.Find(id);
//            var tran = new Transactions
//            {
//                Event = EventUserPR.INSERT_ATTACH.ToString(),
//                UserID = userDB.UserID,
//                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
//            };
//            pr.Transactions.Add(tran);
//            pr.Attaches.Add(att);
//            rContext.SaveChanges();
//        }

//        public void DeleteAttach(int headerID, int itemID)
//        {
//            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
//            switch (td)
//            {
//                case TypeDocumentHeader.PR:
//                    var pr = rContext.RequisitionHeader.Find(headerID);
//                    var att = rContext.Attaches.Find(itemID);
//                    var tran = new Transactions
//                    {
//                        Event = EventUserPR.DELETE_ATTACH.ToString(),
//                        UserID = CurrentUser.UserID,
//                        DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
//                    };
//                    pr.Transactions.Add(tran);
//                    rContext.Attaches.Remove(att);
//                    rContext.SaveChanges();
//                    break;
//                case TypeDocumentHeader.PO:
//                    break;
//                default:
//                    break;
//            }

//        }

//        public List<OrderDetails> GetDetailsOrder(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public iGDropDownList GetStatusItem(DataRow dataRow)
//        {
//            throw new NotImplementedException();
//        }

//        public ContextMenuStrip GetMenuStrip()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
