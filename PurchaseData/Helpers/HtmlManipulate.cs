
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

using Spire.Pdf;

namespace PurchaseData.Helpers
{
    public class HtmlManipulate
    {
        public HtmlDocument HtmlDoc { get; set; }
        private readonly ConfigApp configApp;

        public HtmlManipulate(ConfigApp configApp)
        {
            this.configApp = configApp;
            HtmlDoc = new HtmlDocument();
        }

        public Task<string> ConvertHtmlToPdf(List<string> path, string id)
        {
            try
            {
                int count = 0;
                string[] save = new string[path.Count];
                return Task.Run(() =>
                  {
                      //https://github.com/vilppu/OpenHtmlToPdf
                      foreach (var item in path)
                      {
                          var pdf = Pdf.From(File.ReadAllText(item)).OfSize(OpenHtmlToPdf.PaperSize.Letter)
                              //.WithTitle("Title")
                              //.WithoutOutline()
                              //.WithMargins(1.25.Centimeters())
                              //.Portrait()
                              //.Comressed()                          
                              //.WithGlobalSetting("pageOffset", "88888").WithGlobalSetting("copies ", "4")
                              .Content();

                          save[count] = Path.GetTempPath() + id + count + ".pdf";
                          File.WriteAllBytes(Path.GetTempPath() + id + count + ".pdf", pdf);
                          count++;
                          pdf = null;
                      }

                      PdfDocumentBase docfinal = PdfDocument.MergeFiles(save);
                      docfinal.Save(Path.GetTempPath() + id + ".pdf", FileFormat.PDF);

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
            //! PR
            var headerID = Convert.ToInt32(headerDR["headerID"]);
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
            HtmlDoc.GetElementbyId("Description").InnerHtml = $"{headerID}";

            HtmlDoc.GetElementbyId("USER_CREATION").InnerHtml = new Users()
                .GetUserByID(headerDR["UserID"].ToString()).FirstName.Substring(0, 1) + " " + new Users()
                .GetUserByID(headerDR["UserID"].ToString()).LastName;

            HtmlNode table = HtmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/table[1]");
            HtmlDoc.GetElementbyId("HeaderID").InnerHtml = $"{headerDR["HeaderID"]}";

            if (user.ProfileID == "UPO")
            {
                HtmlDoc.GetElementbyId("vandor_name")
                    .InnerHtml = $"The following Purchase Requisition has been created by the user ID <b>{headerDR["UserID"]}</b>, " +
                              "the next step is to create the <b>Purchase Order</b> by You. " +
                              "The reference code for this Purchase Requisition is: ";
            }
            else if (user.ProfileID == "UPR")
            {
                HtmlDoc.GetElementbyId("vandor_name")
                    .InnerHtml = "The following Purchase Requisition has been created by you, " +
                    "the next step is to create the <b>Purchase Order</b> by the corresponding " +
                    "user so complete all the required fields. " +
                    "The reference code for this Purchase Requisition is: ";
            }
            HtmlDoc.GetElementbyId("code").InnerHtml = $"{headerDR["Code"]}";
            var att = new Attaches().GetListByID(headerID).Where(c => c.Modifier == 1).ToList();
            HtmlDoc.GetElementbyId("NRO_ATTACH").InnerHtml = $"{att.Count}";
            if (details.Count > 0)
            {
                HtmlDoc.GetElementbyId("NRO_DETAILS").InnerHtml = $"{details.Count}";
                foreach (RequisitionDetails item in details)
                {
                    string node = "<tr class='list-item'>";
                    node += $"<td data-label='Line' class='tableitem' id='line_num'>{line++}</td>";
                    node += $"<td data-label='Description' class='tableitem' id='item_description'>{item.NameProduct} - {item.DescriptionProduct}</br>{item.Accounts.Description}</td>";
                    node += $"<td data-label='Quantity' class='tableitem' id='quantity'>{item.Qty}</td>";
                    node += $"<td data-label='Account' class='tableitem' id='Account'>{item.AccountID}</td>";
                    table.AppendChild(HtmlNode.CreateNode(node));
                }
                // HtmlDoc.GetElementbyId("DETAILS_COUNT").InnerHtml = $"{details.Count}";
            }
            string pathcomplete = Path.GetTempPath() + headerDR["HeaderID"].ToString() + ".html";
            HtmlDoc.Save(pathcomplete, System.Text.Encoding.UTF8);
            return pathcomplete;
        }

        public List<string> ReemplazarDatos(DataRow headerDR, Users user, List<OrderDetails> details)
        {
            //! PO
            List<string> lista = new List<string>();
            string path = Environment.CurrentDirectory + @"\HtmlDocuments\OrderDoc.html";


            var currency = headerDR["CurrencyID"].ToString();

            //! Details
            string pathcomplete;

            int line = 0;

            if (details.Count > 0)
            {
                int count = 0;
                float pages = (details.Count / 5.0f);
                int page = 1;

                for (int i = 0; i < Math.Ceiling(pages); i++)
                {
                    //for (int c = 0; c < 5; c++)
                    //{
                    HtmlDoc.Load(path);
                    HtmlNode table = HtmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div[3]/div/table");
                    //string node = string.Empty;
                    while (count < 5 && line <= details.Count - 1)
                    {

                        string node = "<tr><td class='text-right' style='width: 50px;'>";
                        node += $"<span class='mono'>{line + 1}</span><br><small class='text-muted'></small></td>";
                        node += $"<td>{details[line].NameProduct}<br>";
                        node += $"<small class='text-muted'>{details[line].DescriptionProduct}</small></td>";
                        node += $"<td class='text-right'><span class='mono'>{details[line].Qty.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}</span><br>";
                        node += $"<small class='text-muted'>{details[line].Medidas.Description}</small></td>";
                        node += $"<td class='text-right'><span class='mono'>${details[line].Price.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}</span><br>";
                        node += "<small class='text-muted'>Definitivo</small></td><td class='text-right'>";
                        node += $"<strong class='mono'>${details[line].Total.ToString("#,0.", CultureInfo.GetCultureInfo("es-CL"))}</strong><br><small class='text-muted mono'>{currency}</small>";

                        table.AppendChild(HtmlNode.CreateNode(node));
                        count++;
                        line++;

                        //! Paginacion
                        HtmlDoc.GetElementbyId("CODE").InnerHtml =
                            $"N° {headerDR["Code"]} | ({headerDR["Status"]}) | Página { page} de { Math.Ceiling(pages)}";
                    }

                    //! Cargar Doc
                    pathcomplete = Path.GetTempPath() + headerDR["HeaderID"].ToString() + "_" + page + ".html";
                    CargarDocumento(headerDR, user, pathcomplete);


                    //}
                    page++;
                    count = 0;

                    lista.Add(pathcomplete);
                }
            }
            return lista;
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

                var dolar = await new IndicadorDolar(configApp).GetPosterior(semana);
                IndicadorUf uf = await new IndicadorUf(configApp).GetPosterior(semana);
                IndicadorEuro euro = await new IndicadorEuro(configApp).GetPosterior(semana);
                IndicadorIpc ipc = await new IndicadorIpc(configApp).GetPosterior(semana.AddMonths(-6));
                IndicadorUtm utm = await new IndicadorUtm(configApp).GetPosterior(semana.AddMonths(-6));
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

        private void CargarDocumento(DataRow headerDR, Users user, string pathcomplete)
        {
            string path = Environment.CurrentDirectory + @"\HtmlDocuments\OrderDoc.html";
            // HtmlDoc.Load(path);

            var headerID = Convert.ToInt32(headerDR["HeaderID"]);
            DateTime creationFile = Convert.ToDateTime(headerDR["DateLast"]);


            //! Company
            HtmlDoc.GetElementbyId("NAMECOMPANY").InnerHtml = $"<strong>{headerDR["CompanyName"]}</strong>";
            //HtmlDoc.GetElementbyId("CODE").InnerHtml = $"N° {headerDR["Code"]} | ({headerDR["Status"]})";
            // HtmlDoc.GetElementbyId("NAMEBIZ").InnerHtml = $"{headerDR["NameBiz"]} - {headerDR["CompanyCode"]}";
            HtmlDoc.GetElementbyId("RUTCOMPANY").InnerHtml = $"{headerDR["CompanyID"]}-{new ValidadorRut().Digito(Convert.ToInt32(headerDR["CompanyID"]))}";
            HtmlDoc.GetElementbyId("GiroCompany").InnerHtml = configApp.GiroCompany;
            HtmlDoc.GetElementbyId("AddressCompany").InnerHtml = configApp.AddressCompany;
            HtmlDoc.GetElementbyId("PhoneCompany").InnerHtml = configApp.PhoneCompany;
            HtmlDoc.GetElementbyId("EMAIL").InnerHtml = configApp.Email;
            HtmlDoc.GetElementbyId("NAMECONTACT").InnerHtml = $"{user.FirstName} {user.LastName}";


            //! Supplier
            var supp = new Suppliers().GetByID(headerDR["SupplierID"].ToString());
            HtmlDoc.GetElementbyId("SUPPLIERNAME").InnerHtml = $"<strong>{supp.Name}</strong>";
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
            //! Hitos
            HtmlNode tablehitos = HtmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div[5]/div[1]/div/div[2]/table");
            var hitos = new OrderHitos().GetListByID(headerID);
            if (hitos.Count > 0)
            {
                foreach (var item in hitos)
                {
                    DateTime fecha = creationFile.AddDays(item.Days);
                    var n = ((Convert.ToDecimal(headerDR["Net"]) + Convert.ToDecimal(headerDR["Exent"])) * item.Porcent) / 100;
                    string node = $"<tr><td>{item.Description}:&nbsp;&nbsp;<strong>{item.Porcent}%</strong>&nbsp;&nbsp;&nbsp;";
                    node += $"<small>{fecha:dd-MM-yyyy}&nbsp;&nbsp;({item.Days} Días)</small></td><td class='text-right'>";
                    node += $"<span class='mono'>${n.ToString("#,0.00", CultureInfo.GetCultureInfo("es-CL"))}</span></td>";
                    tablehitos.AppendChild(HtmlNode.CreateNode(node));
                }
            }
            //! Notas
            HtmlNode tablenotas = HtmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div[5]/div[3]/div/div/ul");
            var notes = new OrderNotes().GetListByID(headerID);
            if (notes.Count > 0)
            {
                foreach (var item in notes)
                {
                    if (item.Modifier == 1) // 1 = Público
                    {
                        string node = $"<li>{item.Title} : <span class='mono'>{item.Description}</span></li>";
                        tablenotas.AppendChild(HtmlNode.CreateNode(node));
                    }
                }
            }
            //! Fechas de entrega
            HtmlNode tabledelivery = HtmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div[5]/div[2]/div/div[2]/table");
            var delivery = new OrderDelivery().GetListByID(headerID);
            if (delivery.Count > 0)
            {
                foreach (var item in delivery)
                {
                    string node = $"<tr><td>{item.Description}</td><td class='text-right'><span class='mono'>{item.Date:dd-MM-yyyy}</span></td>";
                    tabledelivery.AppendChild(HtmlNode.CreateNode(node));
                }
            }

            //! totales
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

            //! Glosa
            HtmlDoc.GetElementbyId("GLOSA").InnerHtml = headerDR["Description"].ToString();

            //! Comentarios
            HtmlDoc.GetElementbyId("COMENTARIOS").InnerHtml = configApp.Comentarios;
            HtmlDoc.GetElementbyId("APPYEAR").InnerHtml = configApp.Year.ToString();

            //! Firmas
            HtmlDoc.GetElementbyId("FIRMA1").InnerHtml = $"pp Cliente:{0}";
            HtmlDoc.GetElementbyId("FIRMA2").InnerHtml = $"pp Proveedor:{0}";

            HtmlDoc.Save(pathcomplete, System.Text.Encoding.UTF8);
        }
    }
}
