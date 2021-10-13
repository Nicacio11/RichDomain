using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IEmailService
    {
        void Send(string to, string email, string subject, string body);

    }
}
