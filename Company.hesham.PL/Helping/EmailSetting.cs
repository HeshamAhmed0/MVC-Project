using System.Net;
using System.Net.Mail;

namespace Company.hesham.PL.Helping
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            try{
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("heshammathana1@gmail.com", "rzwkozvopkrdtjlt");
                client.Send("heshammathana1@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
       
        }
    }
}
