
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            HtmlDoc.GetElementbyId("CompanyName").InnerHtml = $"{dataRow["CompanyName"]}";
            HtmlDoc.GetElementbyId("NameBiz").InnerHtml = $"{dataRow["NameBiz"]}";
            HtmlDoc.GetElementbyId("CompanyID").InnerHtml = $"{dataRow["CompanyID"]}";
            HtmlDoc.GetElementbyId("CompanyCode").InnerHtml = $"{dataRow["CompanyCode"]}";
            HtmlDoc.GetElementbyId("Code").InnerHtml = $"{dataRow["Code"]}";
            HtmlDoc.GetElementbyId("DateLast").
                InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(dataRow["DateLast"]))}";
            HtmlDoc.GetElementbyId("NOW").InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", DateTime.Now)}";
            HtmlDoc.GetElementbyId("STATUS_DESC").InnerHtml = $"{dataRow["Status"]}";
            HtmlDoc.GetElementbyId("Description").InnerHtml = $"{dataRow["Description"]}";


            var table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/table[1]");
            #region Logos      
            var logo = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]");
            //logo.AppendChild(HtmlNode.CreateNode("<img />"));

            var icon = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]");
            //icon.AppendChild(HtmlNode.CreateNode("<img src=''/>"));
            #endregion

            switch (user.UserProfiles.ProfileID)
            {
                case "ADM":
                    break;
                case "BAS":
                    break;
                case "UPO":
                    HtmlDoc.GetElementbyId("HeaderID").InnerHtml = $"{dataRow["OrderHeaderID"]}";
                    break;
                case "UPR":
                    HtmlDoc.GetElementbyId("HeaderID").InnerHtml = $"{dataRow["RequisitionHeaderID"]}";
                    break;
                case "VAL":
                    break;
            }

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
                HtmlDoc.GetElementbyId("DETAILS_COUNT").InnerHtml = $"{details.Count}";
            }
            var path = Environment.CurrentDirectory + @"\" + dataRow["RequisitionHeaderID"].ToString() + ".html";
            HtmlDoc.Save(path, System.Text.Encoding.UTF8);
            return path;
        }

        public string ReemplazarDatos(DataRow dataRow, Users user, List<OrderDetails> details)
        {
            var path = Environment.CurrentDirectory + @"\HtmlDocuments\OrderDoc.html";
            HtmlDoc.Load(path);



            HtmlDoc.Save(path, System.Text.Encoding.UTF8);
            return path;
        }
        public async Task<string> ReemplazarDatos()
        {
            var path = Environment.CurrentDirectory + @"\HtmlBanner\Banner.html";
            HtmlDoc.Load(path);
            HtmlNode table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
            var semana = DateTime.Now.AddDays(-7);
            try
            {
                //await Task.Run(() =>
                //   {
                var dolar = await new IndicadorDolar().GetPosterior(semana);
                var uf = await new IndicadorUf().GetPosterior(semana);
                var euro = await new IndicadorEuro().GetPosterior(semana);
                var ipc = await new IndicadorIpc().GetPosterior(semana.AddMonths(-6));
                var utm = await new IndicadorUtm().GetPosterior(semana.AddMonths(-6));
                if (dolar != null)
                {
                    CrearNodo(table, dolar.Dolar, "USD");
                }
                if (uf != null)
                {
                    CrearNodo(table, uf.Uf, "UF");
                }
                if (euro != null)
                {
                    CrearNodo(table, euro.Euro, "EUR");
                }
                if (ipc != null)
                {
                    CrearNodo(table, ipc.Ipc, "IPC");
                }
                if (utm != null)
                {
                    CrearNodo(table, utm.Utm, "UTM");
                }

                // });

                //! Save
                HtmlDoc.Save(path, System.Text.Encoding.UTF8);
                return path;
            }
            catch (NullReferenceException)
            {

                throw;
            }

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
