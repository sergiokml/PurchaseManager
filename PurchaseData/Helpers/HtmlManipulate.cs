
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

using HtmlAgilityPack;

using OpenHtmlToPdf;

using PurchaseData.DataModel;
using PurchaseData.Indicadores;

namespace PurchaseData.Helpers
{
    public class HtmlManipulate
    {
        public HtmlDocument HtmlDoc { get; set; }

        public HtmlManipulate()
        {
            HtmlDoc = new HtmlDocument();
        }

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

        public string ReemplazarDatos(DataRow dataRow, Users user, List<RequisitionDetails> details)
        {
            HtmlDoc.Load(Environment.CurrentDirectory + @"\HtmlDocuments\RequisitionDoc.html");
            string userName = $"{user.FirstName} {user.LastName}";
            var line = 1;
            HtmlDoc.GetElementbyId("userName").InnerHtml = userName;
            HtmlDoc.GetElementbyId("RequisitionHeaderID").InnerHtml = $"{dataRow["RequisitionHeaderID"]}";
            HtmlDoc.GetElementbyId("CompanyName").InnerHtml = $"{dataRow["CompanyName"]}";
            HtmlDoc.GetElementbyId("NameBiz").InnerHtml = $"{dataRow["NameBiz"]}";
            HtmlDoc.GetElementbyId("CompanyID").InnerHtml = $"{dataRow["CompanyID"]}";
            HtmlDoc.GetElementbyId("CompanyCode").InnerHtml = $"{dataRow["CompanyCode"]}";
            HtmlDoc.GetElementbyId("Code").InnerHtml = $"{dataRow["Code"]}";
            HtmlDoc.GetElementbyId("DateLast").
                InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(dataRow["DateLast"]))}";
            HtmlDoc.GetElementbyId("NOW").InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", DateTime.Now)}";
            HtmlDoc.GetElementbyId("Status").InnerHtml = $"{dataRow["Status"]}";
            HtmlDoc.GetElementbyId("Description").InnerHtml = $"{dataRow["Description"]}";

            var table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/table[1]");
            #region Logos      
            var logo = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]");
            logo.AppendChild(HtmlNode.CreateNode("<img src='data:image/png;base64,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'/>"));

            var icon = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]");
            icon.AppendChild(HtmlNode.CreateNode("<img src='data:image/png;base64,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'/>"));
            #endregion

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
                    HtmlDoc.GetElementbyId("vandor_name").InnerHtml = "The following Purchase Requisition has been created by you, " +
                        "the next step is to create the <b>Purchase Order</b> by the corresponding user so complete all the required fields. " +
                        "The reference code for this Purchase Requisition is: ";
                    HtmlDoc.GetElementbyId("code").InnerHtml = $"{dataRow["Code"]}";
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

                        //string attachesUnido = "Attaches: ";
                        //foreach (var item in attaches)
                        //{
                        //    var extension = item.FileName.Substring(item.FileName.Length - 4, 4);
                        //    attachesUnido += $"{item.Description}{extension} {item.Description}{extension}";
                        //}
                        //string attt = string.Empty;
                        //attt += $"{attachesUnido}";
                        //att.AppendChild(HtmlNode.CreateNode(attt));
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
            HtmlDoc.Save(path, System.Text.Encoding.UTF8);
            return path;
        }

        public string ReemplazarDatos()
        {
            var path = Environment.CurrentDirectory + @"\HtmlBanner\Banner.html";
            HtmlDoc.Load(path);
            HtmlNode table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
            var semana = DateTime.Now.AddDays(-7);
            CrearNodo(table, new IndicadorDolar().GetPosterior(semana).Dolar, "USD");
            CrearNodo(table, new IndicadorUf().GetPosterior(semana).Uf, "UF");
            CrearNodo(table, new IndicadorUtm().GetPosterior(semana.AddMonths(-6)).Utm, "UTM");
            CrearNodo(table, new IndicadorIpc().GetPosterior(semana.AddMonths(-6)).Ipc, "IPC");
            CrearNodo(table, new IndicadorEuro().GetPosterior(semana).Euro, "EUR");
            //! Save
            HtmlDoc.Save(path, System.Text.Encoding.UTF8);
            return path;
        }

        private void CrearNodo(HtmlNode table, List<Indicador> indicadors, string money)
        {
            string ultimo = string.Empty;
            decimal res = 0;
            string node = string.Empty;
            CultureInfo culture = new CultureInfo("es-CL");
            if (indicadors.Count > 2)
            {
                ultimo = indicadors.ElementAtOrDefault(indicadors.Count() - 1).Valor; // today
                string penultimo = indicadors.ElementAtOrDefault(indicadors.Count() - 2).Valor;
                if (ultimo == null || penultimo == null)
                {
                    return;
                }
                res = Convert.ToDecimal(ultimo, culture) - Convert.ToDecimal(penultimo, culture);
            }
            if (res > 0)
            {
                node += $"<a class='currency inc'>";
            }
            else
            {
                node += $"<a class='currency dec'>";
            }
            node += $"<div></div><div class='currency-name'>CLP</div>/";
            node += "<div class='flag flag-cl'></div>";
            node += $"<div class='currency-name'>{money}</div>";
            node += $"<div class='rate'>{ultimo}</div>";
            node += $"<div class='change'>{res}</div></a>";
            table.AppendChild(HtmlNode.CreateNode(node));
        }
    }
}
