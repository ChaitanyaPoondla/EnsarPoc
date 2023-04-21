namespace Ensar_Smtp.Models
{
    public class SmtpConfiguration
    {
        public string host { get; set; }
        public int port { get; set; }
        public string fromEmail { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool enableSsl { get; set; }
    }
}
