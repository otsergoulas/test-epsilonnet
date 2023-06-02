using BlazorApp.Server.Helpers;
using BlazorApp.Shared;
using BlazorApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(PaginationDto pagination);
        Task<Customer?> GetCustomerAsync(Guid id);
        Task<bool> CreateAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid id);
        Task<double> CountAsync();
    }

    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        private readonly ILogger<CustomerService> _logger;

        public CustomerService(DataContext context, ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(PaginationDto pagination)
        {
            var queryable = _context.Customers.AsQueryable();
            return await queryable.Paginate(pagination).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Customer?> GetCustomerAsync(Guid id)
        {
            var user = await _context.Customers.FindAsync(id).ConfigureAwait(false);

            return user;
        }

        public async Task<bool> CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer).ConfigureAwait(false);

            return await SaveChangesInDbAsync().ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);

            return await SaveChangesInDbAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = _context.Customers.First(item => item.Id == id);

            _context.Customers.Remove(customer);

            return await SaveChangesInDbAsync().ConfigureAwait(false);
        }

        public async Task<double> CountAsync()
        {
            var queryable = _context.Customers.AsQueryable();
            return await queryable.CountAsync().ConfigureAwait(false);
        }

        private async Task<bool> SaveChangesInDbAsync()
        {
            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("CustomerService: Error {Name} - {@Error}", GetType().FullName, ex.Message);

                return false;
            }
        }
    }
}
