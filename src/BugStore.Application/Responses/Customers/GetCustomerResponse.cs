using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Customers;

public class GetCustomerResponse
{
    public List<CustomerDto> Customers { get; set; } = new();
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}