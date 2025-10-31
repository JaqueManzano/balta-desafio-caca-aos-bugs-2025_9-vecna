using BugStore.Domain.Entities;

namespace BugStore.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task<List<Order>> SearchAsync(
        string? customerName,
        string? customerEmail,
        string? customerPhone,
        string? productTitle,
        string? productDescription,
        string? productSlug,
        decimal? productPriceStart,
        decimal? productPriceEnd,
        DateTime? createdAtStart,
        DateTime? createdAtEnd,
        DateTime? updatedAtStart,
        DateTime? updatedAtEnd,
        CancellationToken cancellationToken);
    }
}
