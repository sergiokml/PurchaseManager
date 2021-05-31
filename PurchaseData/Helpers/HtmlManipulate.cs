
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
        private ConfigApp dbConfig { get; set; }

        public HtmlManipulate(ConfigApp dbConfig)
        {
            this.dbConfig = dbConfig;
            HtmlDoc = new HtmlDocument();
        }

        public Task<string> ConvertHtmlToPdf(string path, string id)
        {

            try
            {
                return Task.Run(() =>
                  {
                      var pdf = Pdf.From(File.ReadAllText(path))
                          .OfSize(PaperSize.Letter)
                          //.WithTitle("Title")
                          //.WithoutOutline()
                          //.WithMargins(1.25.Centimeters())
                          //.Portrait()
                          //.Comressed()
                          .Content();

                      File.WriteAllBytes(Path.GetTempPath() + id + ".pdf", pdf);
                      return Path.GetTempPath() + id + ".pdf";

                  });

            }
            catch (IOException)
            {
                throw;
            }
            //return null;
        }

        public string ReemplazarDatos(DataRow headerDR, Users user, List<RequisitionDetails> details)
        {
            string path = Environment.CurrentDirectory + @"\HtmlDocuments\RequisitionDoc.html";
            HtmlDoc.Load(path);
            string userName = $"{user.FirstName} {user.LastName}";
            int line = 1;
            HtmlDoc.GetElementbyId("userName").InnerHtml = userName;

            HtmlDoc.GetElementbyId("CompanyName").InnerHtml = $"{headerDR["CompanyName"]}";
            HtmlDoc.GetElementbyId("NameBiz").InnerHtml = $"{headerDR["NameBiz"]}";
            HtmlDoc.GetElementbyId("CompanyID").InnerHtml = $"{headerDR["CompanyID"]}";
            HtmlDoc.GetElementbyId("CompanyCode").InnerHtml = $"{headerDR["CompanyCode"]}";
            HtmlDoc.GetElementbyId("Code").InnerHtml = $"{headerDR["Code"]}";
            HtmlDoc.GetElementbyId("DateLast").
                InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(headerDR["DateLast"]))}";
            HtmlDoc.GetElementbyId("NOW").InnerHtml = $"{string.Format("{0:dd MMMM yyyy}", DateTime.Now)}";
            HtmlDoc.GetElementbyId("STATUS_DESC").InnerHtml = $"{headerDR["Status"]}";
            HtmlDoc.GetElementbyId("Description").InnerHtml = $"{headerDR["Description"]}";


            HtmlNode table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/table[1]");
            HtmlDoc.GetElementbyId("HeaderID").InnerHtml = $"{headerDR["HeaderID"]}";

            if (user.ProfileID == "UPO")
            {
                HtmlDoc.GetElementbyId("vandor_name").InnerHtml = $"The following Purchase Requisition has been created by the user ID <b>{headerDR["UserID"]}</b>, " +
                              "the next step is to create the <b>Purchase Order</b> by You. " +
                              "The reference code for this Purchase Requisition is: ";
            }
            else if (user.ProfileID == "UPR")
            {

                HtmlDoc.GetElementbyId("vandor_name").InnerHtml = "The following Purchase Requisition has been created by you, " +
                           "the next step is to create the <b>Purchase Order</b> by the corresponding user so complete all the required fields. " +
                           "The reference code for this Purchase Requisition is: ";
            }


            HtmlDoc.GetElementbyId("code").InnerHtml = $"{headerDR["Code"]}";
            if (details.Count > 0)
            {
                foreach (RequisitionDetails item in details)
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
            string pathcomplete = Environment.CurrentDirectory + @"\" + headerDR["HeaderID"].ToString() + ".html";
            HtmlDoc.Save(pathcomplete, System.Text.Encoding.UTF8);
            return pathcomplete;
        }

        public string ReemplazarDatos(DataRow headerDR, Users user, List<OrderDetails> details)
        {
            string path = Environment.CurrentDirectory + @"\HtmlDocuments\OrderDoc.html";
            HtmlDoc.Load(path);
            int line = 1;

            HtmlDoc.GetElementbyId("NAMECOMPANY")
                .InnerHtml = $"{headerDR["CompanyName"]}";
            HtmlDoc.GetElementbyId("CODE").InnerHtml = $"N° {headerDR["Code"]} | ({headerDR["Status"]})";
            HtmlDoc.GetElementbyId("NAMEBIZ")
                .InnerHtml = $"{headerDR["NameBiz"]} - {headerDR["CompanyCode"]}";
            HtmlDoc.GetElementbyId("RUTCOMPANY")
                .InnerHtml = $"{headerDR["CompanyID"]}-{new ValidadorRut().Digito(Convert.ToInt32(headerDR["CompanyID"]))}";
            HtmlDoc.GetElementbyId("GiroCompany").InnerHtml = dbConfig.GiroCompany;
            HtmlDoc.GetElementbyId("AddressCompany").InnerHtml = dbConfig.AddressCompany;
            HtmlDoc.GetElementbyId("PhoneCompany").InnerHtml = dbConfig.PhoneCompany;
            HtmlDoc.GetElementbyId("EMAIL").InnerHtml = dbConfig.Email;
            HtmlDoc.GetElementbyId("NAMECONTACT").InnerHtml = "Contacto yo?";

            //! Supplier
            var supp = new Suppliers().GetList(headerDR["SupplierID"].ToString());
            HtmlDoc.GetElementbyId("SUPPLIERNAME").InnerHtml = supp.Name;
            HtmlDoc.GetElementbyId("SupplierID")
                .InnerHtml = $"{supp.SupplierID}-{new ValidadorRut().Digito(Convert.ToInt32(supp.SupplierID))}";
            if (supp.Giro != null)
            {
                HtmlDoc.GetElementbyId("GiroCompanySupplier").InnerHtml = supp.Giro;
            }
            if (supp.Phone != null)
            {
                HtmlDoc.GetElementbyId("PhoneCompanySupplier").InnerHtml = supp.Phone;
            }
            if (supp.Address != null)
            {
                HtmlDoc.GetElementbyId("AddressCompanySupplier").InnerHtml = supp.Address;
            }
            if (supp.Email != null)
            {
                HtmlDoc.GetElementbyId("EMAILSupplier").InnerHtml = supp.Email;
            }
            if (supp.ContactName != null)
            {
                HtmlDoc.GetElementbyId("NAMECONTACTSupplier").InnerHtml = supp.ContactName;
            }
            var neto = Convert.ToDecimal(headerDR["Net"]);
            HtmlDoc.GetElementbyId("NET").InnerHtml = $"${neto.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"))}";

            var exent = Convert.ToDecimal(headerDR["Exent"]);
            HtmlDoc.GetElementbyId("EXENT").InnerHtml = $"${exent.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"))}";

            var tax = Convert.ToDecimal(headerDR["Tax"]);
            HtmlDoc.GetElementbyId("TAX").InnerHtml = $"${tax.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"))}";

            var total = Convert.ToDecimal(headerDR["Total"]);
            HtmlDoc.GetElementbyId("GRANDTOTAL").InnerHtml = $"${total.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"))}";

            var discount = Convert.ToDecimal(headerDR["Discount"]);
            HtmlDoc.GetElementbyId("DISCOUNT").InnerHtml = $"${discount.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"))}";
            //! Details
            HtmlNode table = HtmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div/div[2]/div[3]/table");
            if (details.Count > 0)
            {
                int count = 0;
                foreach (var item in details)
                {
                    count++;
                    string node = "<tr><td class='text-right' style='width: 50px;'>";
                    node += $"<span class='mono'>{line++}</span><br><small class='text-muted'></small></td>";
                    node += $"<td>{item.NameProduct}<br>";
                    node += $"<small class='text-muted'>{item.DescriptionProduct}</small></td>";
                    node += $"<td class='text-right'><span class='mono'>${item.Qty.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}</span><br>";
                    node += $"<small class='text-muted'>{item.Medidas.Description}</small></td>";
                    node += $"<td class='text-right'><span class='mono'>${item.Price.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}</span><br>";
                    node += "<small class='text-muted'>Definitivo</small></td><td class='text-right'>";
                    node += $"<strong class='mono'>${item.Total.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}</strong><br><small class='text-muted mono'>CLP</small>";

                    table.AppendChild(HtmlNode.CreateNode(node));

                }
            }
            HtmlDoc.GetElementbyId("GLOSA").InnerHtml = headerDR["Description"].ToString();

            string pathcomplete = Environment.CurrentDirectory + @"\" + headerDR["HeaderID"].ToString() + ".html";
            HtmlDoc.Save(pathcomplete, System.Text.Encoding.UTF8);
            return pathcomplete;
        }
        public async Task<string> ReemplazarDatos()
        {
            string path = Environment.CurrentDirectory + @"\HtmlBanner\Banner.html";
            HtmlDoc.Load(path);
            HtmlNode table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
            DateTime semana = DateTime.Now.AddDays(-7);
            try
            {
                //await Task.Run(() =>
                //   {

                var dolar = await new IndicadorDolar(dbConfig).GetPosterior(semana);
                IndicadorUf uf = await new IndicadorUf(dbConfig).GetPosterior(semana);
                IndicadorEuro euro = await new IndicadorEuro(dbConfig).GetPosterior(semana);
                IndicadorIpc ipc = await new IndicadorIpc(dbConfig).GetPosterior(semana.AddMonths(-6));
                IndicadorUtm utm = await new IndicadorUtm(dbConfig).GetPosterior(semana.AddMonths(-6));
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
            string fecha = string.Empty;
            decimal res = 0;
            string node = string.Empty;
            CultureInfo culture = new CultureInfo("es-CL");
            if (indicadors.Count > 2)
            {
                ultimo = indicadors.ElementAtOrDefault(indicadors.Count() - 1).Valor; // today
                fecha = indicadors.ElementAtOrDefault(indicadors.Count() - 1).Fecha;

                string penultimo = indicadors.ElementAtOrDefault(indicadors.Count() - 2).Valor;
                if (ultimo == null || penultimo == null)
                {
                    return;
                }
                res = Convert.ToDecimal(ultimo, culture) - Convert.ToDecimal(penultimo, culture);
            }
            else
            {
                ultimo = indicadors.ElementAtOrDefault(indicadors.Count() - 1).Valor; // today
                fecha = indicadors.ElementAtOrDefault(indicadors.Count() - 1).Fecha;

            }
            if (res > 0)
            {
                node += $"<a title='{string.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(fecha))}' class='currency inc tooltip_link left'>";
            }
            else if (res < 0)
            {
                node += $"<a title='{string.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(fecha))}' class='currency dec tooltip_link left'>";
            }
            else if (res == 0)
            {
                node += $"<a title='{string.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(fecha))}' class='currency zero tooltip_link left'>";
            }
            switch (money)
            {
                case "USD":
                    node += $"<div class='flag alt' style='background-image: url(./icons8_usa_16.png)'></div>";
                    break;
                case "UF":
                    node += $"<div class='flag alt' style='background-image: url(./icons8_chile_16.png)'></div>";
                    break;
                case "EUR":
                    node += $"<div class='flag alt' style='background-image: url(./icons8_flag_of_europe_16.png)'></div>";
                    break;
                case "IPC":
                    node += $"<div class='flag alt' style='background-image: url(./icons8_chile_16.png)'></div>";
                    break;
                case "UTM":
                    node += $"<div class='flag alt' style='background-image: url(./icons8_chile_16.png)'></div>";
                    break;
            }
            //node += $"<div class='flag alt' style='background-position:-560px - 64px'></div>";
            node += $"<div class='currency-name'>{money}</div>/";
            // node += $"<div class='flag flag-cl'></div>";
            node += $"<div class='currency-name'></div>";
            node += $"<div class='rate'>{ultimo}</div>";
            if (res > 0)
            {
                node += $"<div class='change'>{res:+#.##;-#.##;}</div></a>";
            }
            else
            {
                node += $"<div class='change'>{res}</div></a>";
            }
            //node += $"<div class='change'>{res:+#.##;-#.##;}</div></a>";
            table.AppendChild(HtmlNode.CreateNode(node));
        }
    }
}
