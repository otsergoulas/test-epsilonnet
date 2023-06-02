using BlazorApp.Server;
using BlazorApp.Server.Services;
using BlazorApp.Shared;
using BlazorApp.Shared.Entities;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BlazorApp.Tests
{
    public class TestDataContext : DataContext
    {
        public TestDataContext() : base(GetTestConfiguration())
        {
        }

        private static IConfiguration GetTestConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            return configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseInMemoryDatabase("MockDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }

    [TestFixture]
    public class ServiceTests
    {
        private CustomerService _service;
        private TestDataContext _testContext;
        private List<Customer> _customers;
        private ILogger<CustomerService> _logger;
        private readonly int _numOfCustomers = 10;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<CustomerService>>()!.Object;
            _testContext = new TestDataContext();
            _service = new CustomerService(_testContext, _logger);
            _customers = CustomerGenerator.GenerateFakeCustomer(_numOfCustomers);
        }

        [Test]
        public async Task It_should_add_a_customer_successfully_in_store()
        {
            //arrange
            var customer = _customers.FirstOrDefault();

            //act
            await _service.CreateAsync(customer).ConfigureAwait(false);

            //assert
            Assert.IsTrue(_testContext.Customers.Any());
        }

        [Test]
        public async Task It_should_find_a_customer_successfully()
        {
            //arrange
            await PopulateCustomers().ConfigureAwait(false);

            //act
            var customerFound = await _service.GetCustomerAsync(_customers.FirstOrDefault().Id).ConfigureAwait(false);

            //assert
            Assert.IsNotNull(customerFound);
        }

        [Test]
        public async Task It_should_Not_find_a_customer_successfully()
        {
            //arrange
            await PopulateCustomers().ConfigureAwait(false);

            //act
            var customerFound = await _service.GetCustomerAsync(Guid.NewGuid()).ConfigureAwait(false);

            //assert
            Assert.IsNull(customerFound);
        }

        [Test]
        public async Task It_should_delete_a_customer_successfully()
        {
            //arrange
            await PopulateCustomers().ConfigureAwait(false);

            //act
            await _service.DeleteAsync(_customers.FirstOrDefault().Id).ConfigureAwait(false);
            var count = _testContext.Customers.Local.Count;

            //assert
            Assert.IsTrue(count < _numOfCustomers);
        }

        [Test]
        public async Task It_should_get_customers_with_pagination()
        {
            //arrange
            await PopulateCustomers().ConfigureAwait(false);

            //act
            var customers = await _service.GetCustomersAsync(new PaginationDto
            {
                Page = 0,
                ItemsPerPage = 10
            }).ConfigureAwait(false);

            //assert
            Assert.IsTrue(customers.Count() == _numOfCustomers);
        }

        private async Task PopulateCustomers()
        {
            foreach (var customer in _customers)
            {
                await _service.CreateAsync(customer).ConfigureAwait(false);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _testContext.Dispose();
        }
    }
}