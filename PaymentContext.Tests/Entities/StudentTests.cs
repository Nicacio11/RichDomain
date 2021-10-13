using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Waayne");
            _document = new Document("11111111111", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("Cajacity", "Cajacity", "Cajacity", "Cajacity", "Cajacity", "Cajacity", "Cajacity");
            _subscription = new Subscription(null);
            _student = new Student(_name, _email, _document);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PaypalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Jhon", _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(!_student.IsValid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenHadSubscriptionNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(!_student.IsValid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            {
                var payment = new PaypalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Jhon", _email);
                _subscription.AddPayment(payment);
                _student.AddSubscription(_subscription);
                Assert.IsTrue(_student.IsValid);
            }
        }
    }
}
