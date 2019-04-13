using System;
using System.Net.Mail;

namespace TestEmail
{
    public class SmtpMail
    {
        public static void Send(MailMessage mail, string SMTPServer, int port)
        {            
            SmtpClient client = new SmtpClient();            
            client.Host = SMTPServer;
            client.Port = port;            
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error sending email using SMTP Server: '{0}' Port: {1}. {2} ", SMTPServer, port, ex.Message), ex);
            }
        }
    }
}
