using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {

        //Red, Green, Refactor
        //faz todos os testes falharem, depois faz teste ter sucesso e por ultimo refatora o codigo
        //Assert.Fail();

        [TestMethod]
        public void ShouldReturnErrorWhenCPNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenCPNPJIsInvalid()
        {
            var doc = new Document("34110468000150", EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }
        // [TestMethod]
        // public void ShouldReturnSuccessWhenCPFIsInvalid()
        // {
        //     var doc = new Document("11111111111", EDocumentType.CPF);
        //     Assert.IsTrue(doc.IsValid);
        // }

        //testanto varias vezes
        [TestMethod]
        [DataTestMethod]
        [DataRow("11111111111")]
        [DataRow("11111111111")]
        [DataRow("11111111111")]
        [DataRow("11111111111")]
        public void ShouldReturnSuccessWhenCPFIsInvalid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}
