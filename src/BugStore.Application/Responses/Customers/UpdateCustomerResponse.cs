using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Customers;

public class UpdateCustomerResponse
{
    public CustomerDto? Customer { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}

