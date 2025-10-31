using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Orders;
using BugStore.Application.Responses.Orders;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Domain.Entities;

namespace BugStore.Application.Tests.Fakes.Services
{
    public class FakeOrderService : IOrderService
    {
        private readonly FakeOrderRepository _repo;

        public FakeOrderService(FakeOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreateOrdersResponse> CreateOrderAsync(CreateOrdersRequest request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                Lines = request.Lines.Select(l => new OrderLine
                {
                    ProductId = l.ProductId,
                    Quantity = l.Quantity
                }).ToList()
            };

            await _repo.AddAsync(order, cancellationToken);

            return new CreateOrdersResponse
            {
                Order = order,
                Success = true,
                Message = "Order created."
            };
        }

        public Task AddOrderAsync(Order order)
        {
            return _repo.AddAsync(order, CancellationToken.None);
        }

        public async Task<GetByIdOrdersResponse> GetOrderByIdAsync(GetByIdOrdersRequest request, CancellationToken cancellationToken)
        {
            var order = await _repo.GetByIdAsync(request.Id, cancellationToken);

            return new GetByIdOrdersResponse
            {
                Order = order,
                Message = string.Empty
            };
        }

    }
}
