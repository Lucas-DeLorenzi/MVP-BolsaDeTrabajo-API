using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace bolsaBE.Services.MailServices
{
    public abstract class MailServices
    {
        private SmtpClient SmtpClient;
        protected string SenderMail { get; set; }
        protected string Password { get; set; }
        protected string Host { get; set; }
        protected int Port { get; set; }
        protected bool Ssl { get; set; }

        protected void initializeSmtpClient()
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Credentials = new NetworkCredential(SenderMail, Password);
            SmtpClient.Host = this.Host;
            SmtpClient.Port = this.Port;
            SmtpClient.EnableSsl = this.Ssl;

        }


        public void SendEmail(string subject, 
            string body, 
            string recipientMail, 
            List<string>? archivos = null, 
            List<byte[]> byteFiles = null)
        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(SenderMail);
                
                mailMessage.To.Add(recipientMail);
                
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.IsBodyHtml = true;

                if( archivos != null)
                {
                    foreach(var archivo in archivos)
                    {
                        if(File.Exists(@archivo))
                            mailMessage.Attachments.Add(new Attachment(@archivo));
                    }
                }

                if (byteFiles != null)
                {
                    int counter = 0;
                    foreach (var byteFile in byteFiles)
                    {
                        Attachment att = new Attachment(new MemoryStream(byteFile), $"archivo{counter}.pfd", MediaTypeNames.Application.Pdf);
                        mailMessage.Attachments.Add(att);
                       counter++;
                    }
                }


                SmtpClient.Send(mailMessage);

            }
            catch (Exception)
            {
            }
            finally
            {
                mailMessage.Dispose();
                SmtpClient.Dispose();
            }
        }
    }
}
