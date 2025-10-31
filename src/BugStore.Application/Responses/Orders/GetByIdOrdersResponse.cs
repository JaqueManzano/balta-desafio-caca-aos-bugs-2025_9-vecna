using BugStore.Domain.Entities;

namespace BugStore.Application.Responses.Orders;

public class GetByIdOrdersResponse
{
    public Order? Order { get; set; }
    public string Message { get; set; } = string.Empty;
}

