using BugStore.Application.Responses.Orders;
using MediatR;

namespace BugStore.Application.Requests.Orders;

public class CreateOrdersRequest : IRequest<CreateOrdersResponse>
{
    public Guid CustomerId { get; set; }

    public List<CreateOrderLineRequestDto> Lines { get; set; } = new();
}

public class CreateOrderLineRequestDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

