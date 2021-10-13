using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string country, string zipCode, string state, string city)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            Country = country;
            ZipCode = zipCode;
            State = state;
            City = city;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(Street, 3, "Address.Street", "Street não pode ser menor que 3 caracteres")
                .IsGreaterThan(Number, 3, "Address.Number", "Number não pode ser menor que 3 caracteres")
                .IsGreaterThan(Neighborhood, 3, "Address.Neighborhood", "Neighborhood não pode ser menor que 3 caracteres")
                .IsGreaterThan(Country, 3, "Address.Country", "Country não pode ser menor que 3 caracteres")
                .IsGreaterThan(ZipCode, 3, "Address.ZipCode", "ZipCode não pode ser menor que 3 caracteres")
                .IsGreaterThan(State, 3, "Address.State", "State não pode ser menor que 3 caracteres")
                .IsGreaterThan(City, 3, "Address.City", "City não pode ser menor que 3 caracteres")
            );
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
    }
}
