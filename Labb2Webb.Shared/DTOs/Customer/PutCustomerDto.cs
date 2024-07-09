using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Shared.DTOs.Customer;

public class PutCustomerDto : CustomerDto
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}