
using System.IO;

using MimeKit;

namespace PurchaseData.Helpers
{
    public class SendEmailTo
    {
        public void SendEmail(string path)
        {
            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody = File.ReadAllText(path)
            };

        }

    }
}
