using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(FirstName, 3, "Name.FirstName", "Nome não pode ser menor que 3 caracteres")
                .IsGreaterThan(LastName, 3, "Name.LastName", "Sobrenome não pode ser menor que 3 caracteres")
                .IsLowerThan(LastName, 40, "Name.LastName", "Sobrenome não pode ser maior que 40 caracteres")
                .IsLowerThan(FirstName, 40, "Name.FirstName", "Sobrenome não pode ser maior que 40 caracteres")
            );

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
