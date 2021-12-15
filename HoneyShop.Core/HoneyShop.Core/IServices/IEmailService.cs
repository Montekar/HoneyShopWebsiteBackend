namespace HoneyShop.Core.IServices
{
    public interface IEmailService
    {
        bool SendEmail(string receiverEmail,string subject,string body);
    }
}