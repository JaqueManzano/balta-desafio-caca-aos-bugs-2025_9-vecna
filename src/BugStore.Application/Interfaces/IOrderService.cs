using BugStore.Application.Requests.Orders;
using BugStore.Application.Responses.Orders;

namespace BugStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<GetByIdOrdersResponse> GetOrderByIdAsync(GetByIdOrdersRequest request, CancellationToken cancellationToken);
        Task<CreateOrdersResponse> CreateOrderAsync(CreateOrdersRequest request, CancellationToken cancellationToken);
    }
}
