using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models.ViewModels;
using System.Net;

namespace services.Common
{
    public class EmailService
    {
        public static void SendMail(MailSettingViewModel settings)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            
            var fromAddress = new MailAddress(smtpSection.Network.UserName, smtpSection.Network.UserName);
            var toAddress = new MailAddress(settings.ToMailId, settings.ToMailName);

            string fromPassword = smtpSection.Network.Password;

            var smtp = new SmtpClient
            {
                Host = smtpSection.Network.Host,
                Port = smtpSection.Network.Port,
                EnableSsl = smtpSection.Network.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = settings.Subject,
                Body = settings.Body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
