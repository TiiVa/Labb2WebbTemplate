using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb2Webb.Shared.Entities;

public class Customer
{
   
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string  Email { get; set; }
    [Phone]
    public string Phone { get; set; }
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    [JsonIgnore]
    public List<Order> Orders { get; set; }
}