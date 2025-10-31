using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Orders;
using BugStore.Application.Responses.Orders;
using MediatR;

namespace BugStore.Application.Handlers.Order
{
    public class GetByIdOrdersHandler(IOrderService _service) : IRequestHandler<GetByIdOrdersRequest, GetByIdOrdersResponse>
    {
        public async Task<GetByIdOrdersResponse> Handle(GetByIdOrdersRequest request, CancellationToken cancellationToken)
        {
            return await _service.GetOrderByIdAsync(request, cancellationToken);
        }
    }
}
