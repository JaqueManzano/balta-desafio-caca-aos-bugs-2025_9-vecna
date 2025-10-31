using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Customers;

public class CreateCustomerResponse
{
    public CustomerDto? Customer {  get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}