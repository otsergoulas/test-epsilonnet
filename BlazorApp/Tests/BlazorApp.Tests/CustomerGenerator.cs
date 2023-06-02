using BlazorApp.Shared.Entities;
using Bogus;

namespace BlazorApp.Tests
{
    public class CustomerGenerator
    {
        public static List<Customer> GenerateFakeCustomer(int count)
        {
            var faker = new Faker<Customer>()
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
                .RuleFor(c => c.ContactName, f => f.Person.FullName)
                .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Region, f => f.Address.State())
                .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
                .RuleFor(c => c.Country, f => f.Address.Country())
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber());

            return faker.Generate(count);
        }
    }
}
