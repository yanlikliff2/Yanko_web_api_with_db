namespace Yanko_web3_v2.Authorization
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
