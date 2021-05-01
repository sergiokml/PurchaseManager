
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using HtmlAgilityPack;

using OpenHtmlToPdf;

using PurchaseData.DataModel;

namespace PurchaseData.Helpers
{
    public class HtmlToPdf
    {
        public string ConvertHtmlToPdf(string path, string id)
        {
            IPdfDocument pdfDocument = Pdf.From(path);
            try
            {
                File.WriteAllBytes(Path.GetTempPath() + id + ".pdf", pdfDocument.Content());
            }
            catch (IOException)
            {
                throw;
            }
            return Path.GetTempPath() + id + ".pdf";
        }

        public string ReemplazarDatos(DataRow dataRow, Users user,
            List<RequisitionDetails> details, List<Attaches> attaches)
        {
            // string userItem = $"{dataRow["FirstName"]} {dataRow["LastName"]}";
            string userName = $"{user.FirstName} {user.LastName}";
            var line = 1;
            var doc = new HtmlDocument();
            doc.Load(Environment.CurrentDirectory + @"\Helpers\RequisitionDoc.html");
            doc.GetElementbyId("userName").InnerHtml = userName;
            doc.GetElementbyId("RequisitionHeaderID").InnerHtml = $"tst-inv-{dataRow["RequisitionHeaderID"]}";
            doc.GetElementbyId("CompanyName").InnerHtml = $"{dataRow["CompanyName"]}";
            doc.GetElementbyId("NameBiz").InnerHtml = $"{dataRow["NameBiz"]}";
            doc.GetElementbyId("CompanyID").InnerHtml = $"{dataRow["CompanyID"]}";
            doc.GetElementbyId("CompanyCode").InnerHtml = $"{dataRow["CompanyCode"]}";
            doc.GetElementbyId("Code").InnerHtml = $"{dataRow["Code"]}";
            DateTime date = Convert.ToDateTime(dataRow["DateLast"]);
            doc.GetElementbyId("DateLast").InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", date)}";
            doc.GetElementbyId("Status").InnerHtml = $"{dataRow["Status"]}";
            doc.GetElementbyId("Description").InnerHtml = $"{dataRow["Description"]}";

            var table = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/table[1]");

            var att = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[2]/table[1]");

            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    // doc.GetElementbyId("vandor_name").InnerHtml = $"The user {userItem} from {user.CostID} department " +
                    //   $"has created a Purchase Rquisition. Please create a Purchase Order and then be sent to the Supplier.";
                    break;
                case "UPR":
                    doc.GetElementbyId("vandor_name").InnerHtml = "The following Purchase Requisition has been created by you. " +
                        "The next step is to create the <b>Purchase Order</b> by the corresponding user so complete in all the required fields, " +
                        "the reference code for this Purchase Requisition is: ";
                    doc.GetElementbyId("code").InnerHtml = $"{dataRow["Code"]}";
                    if (details.Count > 0)
                    {
                        foreach (var item in details)
                        {
                            string node = "<tr class='list-item'>";
                            node += $"<td data-label='Line' class='tableitem' id='line_num'>{line++}</td>";
                            node += $"<td data-label='Description' class='tableitem' id='item_description'>{item.NameProduct} - {item.DescriptionProduct}</br>{item.Accounts.Description}</td>";
                            node += $"<td data-label='Quantity' class='tableitem' id='quantity'>{item.Qty}</td>";
                            node += $"<td data-label='Account' class='tableitem' id='Account'>{item.AccountID}</td>";
                            table.AppendChild(HtmlNode.CreateNode(node));
                        }

                        string attachesUnido = "Attaches: ";
                        foreach (var item in attaches)
                        {
                            var extension = item.FileName.Substring(item.FileName.Length - 4, 4);
                            attachesUnido += $"{item.Description}{extension} {item.Description}{extension}";
                        }
                        string attt = "<tr class='list-item'>";
                        attt += $"<td data-label='Line' class='tableitem' id='line_num'>{attachesUnido}</td>";
                        att.AppendChild(HtmlNode.CreateNode(attt));
                    }


                    //var grandtotal = "<tr class='list-item total-row'>";
                    //grandtotal += "<th colspan='8' class='tableitem'>Attach in Database:</th>";
                    //grandtotal += $"<td data-label='Grand Total' class='tableitem'>{attaches[0].Description}</td>";
                    //grandtotal += " </tr>";
                    //table.AppendChild(HtmlNode.CreateNode(grandtotal));
                    break;
                case "VAL":
                    break;
            }




            var path = Environment.CurrentDirectory + @"\" + dataRow["RequisitionHeaderID"].ToString() + ".html";
            doc.Save(path, System.Text.Encoding.UTF8);

            //content = content.Replace("[CompanyID]", dataRow["CompanyID"].ToString()).
            //              Replace("[USERNAME]", user.FirstName + " " + user.LastName).
            //              Replace("[RequisitionHeaderID]", dataRow["RequisitionHeaderID"].ToString());

            //File.WriteAllText(path, doc.Text);
            return path;
        }

    }
}
