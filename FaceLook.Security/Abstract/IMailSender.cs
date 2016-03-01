namespace FaceLook.Security.Abstract
{
    public interface IMailSender
    {
        void SendMail(string subject, string body, string emailTo);
    }
}