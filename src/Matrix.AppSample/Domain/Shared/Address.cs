using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Matrix.AppSample.Domain.Shared
{
    public sealed class Address : ValueObject
    {
        private Address()
        {
            Street = "";
            Number = "";
            Complement = "";
            Neighborhood = "";
            Cep = "";
            City = "";
            State = "";
        }

        public Address(string street, string number, string complement, string neighborhood, string cep, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            Cep = cep;
            City = city;
            State = state;
        }

        public string Street { get; }
        public string Number { get; }
        public string Complement { get; }
        public string Neighborhood { get; }
        public string Cep { get; }
        public string City { get; }
        public string State { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return Complement;
            yield return Neighborhood;
            yield return Cep;
            yield return City;
            yield return State;
        }
    }
}
