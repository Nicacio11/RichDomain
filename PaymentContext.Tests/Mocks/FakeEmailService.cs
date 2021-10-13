using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {
        }
    }
}
