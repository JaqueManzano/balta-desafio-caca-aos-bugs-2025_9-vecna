using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;

namespace BugStore.Application.Tests.Fakes.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();

        public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = _orders
                .FirstOrDefault(o => o.Id == id);
            return Task.FromResult(order);
        }

        public Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task<List<Order>> SearchAsync(string? customerName, string? customerEmail, string? customerPhone, string? productTitle, string? productDescription, string? productSlug, decimal? productPriceStart, decimal? productPriceEnd, DateTime? createdAtStart, DateTime? createdAtEnd, DateTime? updatedAtStart, DateTime? updatedAtEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
