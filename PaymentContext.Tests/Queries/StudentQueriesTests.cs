using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            for (int i = 0; i < 10; i++)
            {
                _students = new List<Student>();
                _students.Add(
                    new Student(
                        new Name("Student", i.ToString()),
                        new Email(i.ToString() + "@example.com"),
                        new Document("1111111111" + i.ToString(), EDocumentType.CPF)
                        )
                    );
            }
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }
    }
}
