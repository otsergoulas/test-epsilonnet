using BlazorApp.Server.Helpers;
using BlazorApp.Server.Services;
using BlazorApp.Shared;
using BlazorApp.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationDto pagination)
        {
            var total = await _customerService.CountAsync().ConfigureAwait(false);

            HttpContext.AddPaginationInfo(total, pagination.ItemsPerPage);

            var users = await _customerService.GetCustomersAsync(pagination).ConfigureAwait(false);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _customerService.GetCustomerAsync(id).ConfigureAwait(false);
            return user != null ? Ok(user) : StatusCode(500);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var dbUpdateSuccessful =  await _customerService.CreateAsync(customer).ConfigureAwait(false);
            return dbUpdateSuccessful ? Ok(new { message = "Customer created" }) : StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            var dbUpdateSuccessful =  await _customerService.UpdateAsync(customer).ConfigureAwait(false);
            return dbUpdateSuccessful ? Ok(new { message = "Customer updated" }) : StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dbUpdateSuccessful =  await _customerService.DeleteAsync(id).ConfigureAwait(false);
            return dbUpdateSuccessful ? Ok(new { message = "Customer deleted" }) : StatusCode(500);
        }
    }
}
