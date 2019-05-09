using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagement.Utilities
{
    public static class EmailSender
    {
        private static readonly SmtpClient Client = new SmtpClient(Configuration.MailServer)
        {
            Port = int.Parse(Configuration.MailPort),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                Configuration.MailAccount,
                Configuration.MailPassword)
        };

        private static readonly MailAddress From = new MailAddress(Configuration.MailAccount);

        public static bool Send(MailMessage message)
        {
            try
            {
                message.From = From;
                Client.Send(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> SendAsync(MailMessage message)
        {
            try
            {
                message.From = From;
                await Client.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
