#region

using System.Configuration;
using System.Net;
using System.Net.Mail;
using FaceLook.Security.Abstract;
using FaceLook.Security.Entities;

#endregion

namespace FaceLook.Security.Concrete
{
    public class EmailSender : IMailSender
    {
        public void SendMail(string subject, string body, string emailTo)
        {
            SmtpClient client = null;
            EmailCredential credential = GetCredentialsFromConfig();

            MailMessage mailToClient = new MailMessage(credential.Email, emailTo)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true
            };

            try
            {
                client = new SmtpClient(credential.Host, credential.Port)
                {
                    EnableSsl = credential.UseSsl,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                client.ServicePoint.MaxIdleTime = 1;
                client.Credentials = new NetworkCredential(credential.Username, credential.Password);

                client.Send(mailToClient);
            }
            catch (SmtpException exc)
            {
                //Add logging
            }
            finally
            {
                if (client != null) client.Dispose();
            }
        }

        private EmailCredential GetCredentialsFromConfig()
        {
            EmailCredential credential = new EmailCredential
            {
                Host = ConfigurationManager.AppSettings["email.credentials.host"],
                Password = ConfigurationManager.AppSettings["email.credentials.pass"],
                Port = int.Parse(ConfigurationManager.AppSettings["email.credentials.port"]),
                UseSsl = bool.Parse(ConfigurationManager.AppSettings["email.credentials.ssl"]),
                Username = ConfigurationManager.AppSettings["email.credentials.user"],
                Email = ConfigurationManager.AppSettings["email.credentials.email"]
            };

            return credential;
        }
    }
}