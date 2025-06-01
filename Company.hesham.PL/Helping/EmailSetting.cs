using System.Net;
using System.Net.Mail;

namespace Company.hesham.PL.Helping
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials =new NetworkCredential("heshammathana2@gmail.com")
        }
    }
}
