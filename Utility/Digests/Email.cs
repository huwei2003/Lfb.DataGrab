using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Comm.Tools.Utility
{
    public class Email
    {
        public static int SendEmail(string mailFrom, string mailTo, string subject, string body, string mailServer, string mailUserName, string mailUserPassword, string displayName = "")
        {
            try
            {
                var message = new MailMessage(mailFrom, mailTo);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Priority = MailPriority.High;
                message.From = new MailAddress(mailFrom, displayName);
                var client = new SmtpClient(mailServer);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailUserName, mailUserPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);

                return 1;
            }
            catch(Exception e)
            {
                new Log().Error(e.Message + e.StackTrace);
                return -1;
            }
        }
    }
}