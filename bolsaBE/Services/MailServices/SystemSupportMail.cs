namespace bolsaBE.Services.MailServices
{
    public class SystemSupportMail : MailServices, ISystemSupportMail
    {

        public SystemSupportMail()
        {
            SenderMail = "utnfrro.rosario@gmail.com";
            Password = "abxwddrbjieyqzlo";
            Host = "smtp.gmail.com";
            Port = 587;
            Ssl = true;
            initializeSmtpClient();
        }
        
    }
}

