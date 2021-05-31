
using System.IO;
using System.Threading.Tasks;

using MailKit.Net.Smtp;

using MimeKit;

using PurchaseData.DataModel;

namespace PurchaseData.Helpers
{
    public class SendEmailTo
    {
        private string Email { get; set; }
        private string Password { get; set; }
        private MimeMessage Message { get; set; }

        public string MessageResult { get; set; }

        public SendEmailTo(string email, string password)
        {
            Email = email;
            Password = password;
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
            Message.From.Add(new MailboxAddress($"Purchase Manager", Email));
            Message.ReplyTo.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            Message.Subject = asunto;
            //Message.Importance = MessageImportance.High;
            //! To
            Message.To.Add(new MailboxAddress("Sergio Ayala", "sergiokml@outlook.com"));
            //! Body
            Message.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.MessageSent += Client_MessageSent; ;
                await client.ConnectAsync("smtp.office365.com", 587, false);
                await client.AuthenticateAsync(Email, Password);
                await client.SendAsync(Message);
                await client.DisconnectAsync(true);
                //MessageResult = $"Message sent successfully to: {Message.To[0].Name}.";
                MessageResult = "OK";
                return MessageResult;
            }
        }

        private void SendMail()
        {

        }

        private void Client_MessageSent(object sender, MailKit.MessageSentEventArgs e)
        {
            //MessageResult = "Message enviado.";
        }
    }
}
