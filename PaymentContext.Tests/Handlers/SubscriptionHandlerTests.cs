using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class SubscriptionHandlerTests
    {

        //Red, Green, Refactor
        //faz todos os testes falharem, depois faz teste ter sucesso e por ultimo refatora o codigo
        //Assert.Fail();

        [TestMethod]
        public void ShouldReturnErrorWhenEmailExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "vt@h.com";
            command.BarCode = "123";
            command.BoletoNumber = "123";
            command.PaymentNumber = "123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Address = "Bat Caverna";
            command.PayerDocument = "12345678911";
            command.Payer = "His Father";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "hisfather@hotmail.com";
            command.Street = "1233";
            command.Number = "1231";
            command.Neighborhood = "12312";
            command.Country = "12312";
            command.ZipCode = "412123";
            command.State = "312311";
            command.City = "413132";
            handler.Handle(command);

            Assert.AreEqual(false, handler.IsValid);
        }
    }
}
