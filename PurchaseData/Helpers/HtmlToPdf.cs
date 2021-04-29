
using System.IO;

using OpenHtmlToPdf;

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

    }
}
