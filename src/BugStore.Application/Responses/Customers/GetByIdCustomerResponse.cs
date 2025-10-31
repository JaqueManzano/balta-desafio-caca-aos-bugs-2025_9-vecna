using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Customers;

public class GetByIdCustomerResponse
{
    public CustomerDto? Customer {  get; set; }
    public string Message { get; set; } = string.Empty;
}