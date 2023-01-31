namespace bolsaBE.Services.MailServices
{
    public interface ISystemSupportMail
    {
        void SendEmail(string subject, string body, string recipientMail, List<string>? archivos = null, List<byte[]> byteFiles = null);
    }
}
