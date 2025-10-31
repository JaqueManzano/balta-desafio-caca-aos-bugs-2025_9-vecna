using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Orders;
using BugStore.Application.Responses.Orders;
using MediatR;

namespace BugStore.Application.Handlers.Order;

public class CreateOrdersHandler(IOrderService _service) : IRequestHandler<CreateOrdersRequest, CreateOrdersResponse>
{
    public async Task<CreateOrdersResponse> Handle(CreateOrdersRequest request, CancellationToken cancellationToken)
    {
        return await _service.CreateOrderAsync(request, cancellationToken);
    }
}
