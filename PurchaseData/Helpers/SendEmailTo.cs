
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using MailKit.Net.Smtp;

using MimeKit;

using PurchaseData.DataModel;

namespace PurchaseData.Helpers
{
    public class SendEmailTo
    {
        private MimeMessage Message { get; set; }

        public string MessageResult { get; set; }

        private readonly ConfigApp configApp;

        public SendEmailTo(ConfigApp configApp)
        {
            this.configApp = configApp;
            Message = new MimeMessage();
        }

        public async Task<string> SendEmail(string path, string asunto, Users user)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            if (Path.GetExtension(path) == ".pdf")
            {
                bodyBuilder.Attachments.Add(path);
            }
            else if (Path.GetExtension(path) == ".html")
            {
                bodyBuilder.HtmlBody = File.ReadAllText(path);

            }
            //! From
            Message.From.Add(new MailboxAddress($"Purchase Manager", configApp.Email));
            Message.ReplyTo.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            Message.Subject = asunto;
            //Message.Importance = MessageImportance.High;
            //! To
            Message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            //! Body
            Message.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.MessageSent += Client_MessageSent; ;
                await client.ConnectAsync("smtp.office365.com", 587, false);
                await client.AuthenticateAsync(configApp.Email, configApp.Password);
                await client.SendAsync(Message);
                await client.DisconnectAsync(true);
                //MessageResult = $"Message sent successfully to: {Message.To[0].Name}.";
                MessageResult = "OK";
                return MessageResult;
            }
        }


        public async Task<string> SendEmailToSupplier(string path, string asunto, Users user, Suppliers supp, List<Attaches> att)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            if (Path.GetExtension(path) == ".pdf")
            {
                bodyBuilder.Attachments.Add(path);
                bodyBuilder.HtmlBody = string.Format(@"<p>Estimado Proveedor {0},<br>
                                        <p>Contacto: {1}<br>    
                                        <p>Adjunto encontrará documentación relacionada con
                                        {2}, favor recuerde contestar a este mail indicando aprovación u observaciones.<br>                                        
                                        <p>-- {3} {4}<br>", supp.Name, supp.ContactName, asunto, user.FirstName, user.LastName);
            }
            else if (Path.GetExtension(path) == ".html")
            {
                bodyBuilder.HtmlBody = File.ReadAllText(path);
            }
            //! Adjuntos
            foreach (var item in att)
            {
                bodyBuilder.Attachments.Add(configApp.FolderApp + @"\" + item.FileName);
            }
            //! From
            Message.From.Add(new MailboxAddress($"Purchase Manager", configApp.Email));
            Message.ReplyTo.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            Message.Subject = asunto;
            //Message.Importance = MessageImportance.High;
            //! To
            Message.To.Add(new MailboxAddress(supp.ContactName, supp.Email));
            //! Body
            Message.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.MessageSent += Client_MessageSent; ;
                await client.ConnectAsync("smtp.office365.com", 587, false);
                await client.AuthenticateAsync(configApp.Email, configApp.Password);
                await client.SendAsync(Message);
                await client.DisconnectAsync(true);
                //MessageResult = $"Message sent successfully to: {Message.To[0].Name}.";
                MessageResult = "OK";
                return MessageResult;
            }
        }

        private void Client_MessageSent(object sender, MailKit.MessageSentEventArgs e)
        {
            //MessageResult = $"Message enviado a ....";
        }
    }
}
