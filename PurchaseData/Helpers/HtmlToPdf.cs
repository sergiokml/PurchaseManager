
using System;
using System.Data;
using System.IO;

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

        public string ReemplazarDatos(DataRow dataRow, Users user)
        {
            string userItem = $"{dataRow["FirstName"]} {dataRow["LastName"]}";
            string userName = $"{user.FirstName} {user.LastName}";

            var doc = new HtmlAgilityPack.HtmlDocument();
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
            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    doc.GetElementbyId("vandor_name").InnerHtml = $"The user {userItem} from {user.CostID} department " +
                        $"has created a Purchase Rquisition. Please create a Purchase Order and then be sent to the Supplier.";
                    break;
                case "UPR":
                    doc.GetElementbyId("vandor_name").InnerHtml = "The following Purchase Requisition has been created by you. " +
                        "The next step is to create the <b>Purchase Order</b> by the corresponding user so complete in all the required fields. " +
                        "The reference code for this Purchase Requisition is: ";
                    doc.GetElementbyId("code").InnerHtml = $"{dataRow["Code"]}";
                    break;
                case "VAL":
                    break;
            }




            var path = Environment.CurrentDirectory + dataRow["RequisitionHeaderID"].ToString() + ".html";
            doc.Save(path, System.Text.Encoding.UTF8);

            //content = content.Replace("[CompanyID]", dataRow["CompanyID"].ToString()).
            //              Replace("[USERNAME]", user.FirstName + " " + user.LastName).
            //              Replace("[RequisitionHeaderID]", dataRow["RequisitionHeaderID"].ToString());

            //File.WriteAllText(path, doc.Text);
            return path;
        }

    }
}
