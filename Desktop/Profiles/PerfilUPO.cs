using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;
using PurchaseDesktop.Interfaces;

using TenTec.Windows.iGridLib;

namespace PurchaseDesktop.Profiles
{
    public class PerfilUPO : HFunctions, IPerfilActions
    {
        private readonly PurchaseManagerContext rContext;
        public PerfilUPO(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }

        public List<OrderDetails> DetailsPO { get; set; }
        public List<RequisitionDetails> DetailsPR { get; set; }
        public DataTable TableDetails { get; set; }
        public DataRow Current { get; set; }
        public TypeDocumentHeader TypeDocHeader { get; set; }
        public OrderHeader DocumentPO { get; set; }
        public RequisitionHeader DocumentPR { get; set; }

        public DataTable GetVistaFPrincipal(Users userDB)
        {
            //todo TENGO QUE UNIR LA LISTA DE LAS Po EMITIDAS POR ESTE USUARIO.
            using (var rContext = new PurchaseManagerContext())
            {
                var l = rContext.vOrderByMinTransaction
              .Where(c => c.UserID == userDB.UserID)
              .OrderByDescending(c => c.DateLast).ToList();
                var r = rContext.vRequisitionByMinTransaction.Where(c => c.StatusID == 2).ToList();

                foreach (var item in r)
                {
                    var n = new vOrderByMinTransaction
                    {
                        Code = item.Code,
                        CompanyID = item.CompanyID,
                        CostID = item.CostID,
                        DateLast = item.DateLast,
                        Description = item.Description,
                        Event = item.Event,
                        Exent = 0,
                        Net = 0,
                        HeaderID = item.HeaderID,
                        StatusID = item.StatusID,
                        SupplierID = string.Empty,
                        Tax = 0,
                        Total = 0,
                        Type = item.Type,
                        UserID = item.UserID,
                        CompanyCode = item.CompanyCode,
                        CompanyName = item.CompanyName,
                        NameBiz = item.NameBiz,
                        Status = item.Status,
                        TypeDocumentHeader = item.TypeDocumentHeader
                    };
                    l.Add(n);
                }
                return this.ToDataTable<vOrderByMinTransaction>(l);
            }
        }


        public void UpdateItemHeader(Users user, object valor, string campo)
        {
            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader myStatus);
            var HeaderID = Convert.ToInt32(Current["HeaderID"]);
            switch (myStatus)
            {
                case TypeDocumentHeader.PR:

                    var pr = rContext.RequisitionHeader.Find(HeaderID);
                    switch (campo)
                    {
                        case "Description":
                            pr.Description = UCase.ToTitleCase(valor.ToString().ToLower());
                            break;
                        case "Type":
                            pr.Type = Convert.ToByte(valor);
                            break;
                        case "StatusID":
                            pr.StatusID = Convert.ToByte(valor);
                            break;
                        case "CompanyID":
                            pr.CompanyID = valor.ToString();
                            break;
                        default:
                            break;
                    }
                    var tran = new Transactions
                    {
                        Event = EventUserPR.UPDATE_PO.ToString(),
                        UserID = user.UserID,
                        DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
                    };
                    pr.Transactions.Add(tran);
                    rContext.SaveChanges();
                    break;

                case TypeDocumentHeader.PO:
                    break;
            }
        }

        public DataTable GetVistaFSupplier()
        {
            return this.ToDataTable<Suppliers>(rContext.Suppliers.ToList());
        }

        public void DeleteItemHeader()
        {
            Enum.TryParse(Current["TypeDocumentHeader"].ToString(), out TypeDocumentHeader myStatus);
            var HeaderID = Convert.ToInt32(Current["HeaderID"]);

            var po = rContext.OrderHeader.Find(HeaderID);
            rContext.Transactions.RemoveRange(po.Transactions);
            rContext.OrderHeader.Remove(po);
            rContext.SaveChanges();
        }


        public List<Attaches> GetAttaches(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertItemHeader(Users userDB)
        {
            var tran = new Transactions
            {
                Event = EventUserPR.CREATE_PR.ToString(),
                UserID = userDB.UserID,
                DateTran = rContext.Database.SqlQuery<DateTime>("select convert(datetime2,GETDATE())").Single()
            };
            DocumentPO.Transactions.Add(tran);
            rContext.OrderHeader.Add(DocumentPO);
            rContext.SaveChanges();
        }

        public void GetFunciones()
        {
            ReqGroupByCost_Results = rContext.ufnGetReqGroupByCost(2).ToList();
            OrderGroupByStatus_Results = rContext.ufnGetOrderGroupByStatus().ToList();
        }

        public void InsertRequisitionDetail(RequisitionDetails detail, Users userDB, int idItem)
        {
            throw new NotImplementedException();
        }

        public DataTable GetVistaAttaches(int IdItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteDetail(int idHeader, int idDetailr, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void InsertAttach(int id, Attaches att, Users userDB)
        {
            throw new NotImplementedException();
        }

        public void DeleteAttache(int id, Users userDB, Attaches item)
        {
            throw new NotImplementedException();
        }

        //public void GetDetails(int id, DataRow dataRow)
        //{
        //    if (Convert.ToSByte(dataRow["TypeDocumentHeader"]) == (sbyte)TypeDocumentHeader.PR)
        //    {

        //    }
        //    var details = rContext.OrderHeader.Find(id).OrderDetails.ToList();
        //    foreach (var item in details)
        //    {
        //        rContext.Entry(item).Reference(c => c.Accounts).Load();
        //    }

        //}

        //public void GetDetailsItem(int id, TypeDocumentHeader typeDocument)
        //{
        //    if (typeDocument == TypeDocumentHeader.PO)
        //    {
        //        DetailsPO = rContext.OrderHeader.Find(id).OrderDetails.ToList();
        //        foreach (var item in DetailsPO)
        //        {
        //            rContext.Entry(item).Reference(c => c.Accounts).Load(); //todo PARA QUE ES ESTO?!!
        //        }
        //    }
        //    else if (typeDocument == TypeDocumentHeader.PR)
        //    {
        //        DetailsPR = rContext.RequisitionHeader.Find(id).RequisitionDetails.ToList();
        //        foreach (var item in DetailsPR)
        //        {
        //            rContext.Entry(item).Reference(c => c.Accounts).Load();
        //        }
        //    }

        //}

        public DataTable GetVistaDetalles()
        {

            return this.ToDataTable<OrderDetails>(rContext.OrderDetails
                .Where(c => c.HeaderID == Convert.ToInt32(Current["HeaderID"])).ToList());
        }

        public iGDropDownList GetStatusItem(DataRow dataRow)
        {
            if (dataRow["TypeDocumentHeader"].ToString() == "PR")
            {
                LLenarCombo(this.ToDataTable<RequisitionStatus>(rContext.RequisitionStatus.ToList()));
            }
            else if (dataRow["TypeDocumentHeader"].ToString() == "PO")
            {
                LLenarCombo(this.ToDataTable<OrderStatus>(rContext.OrderStatus.ToList()));
            }
            return ComboBox;
        }

        public void GetDetailsRequisition(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetails> GetDetailsOrder(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDetail(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
