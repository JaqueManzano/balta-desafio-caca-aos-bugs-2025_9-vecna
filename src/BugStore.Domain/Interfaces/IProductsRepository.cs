using BugStore.Domain.Entities;

namespace BugStore.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product, CancellationToken cancellationToken);
        Task<List<Product>> SearchAsync(
            string? title,
            string? description,
            string? slug,
            decimal? price,
            CancellationToken cancellationToken);
    }
}
