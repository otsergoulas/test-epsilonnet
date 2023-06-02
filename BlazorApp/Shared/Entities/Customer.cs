
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        [Required] 
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        public string ContactName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Region { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}
