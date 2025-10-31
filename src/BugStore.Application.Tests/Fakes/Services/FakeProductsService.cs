using BugStore.Application.Interfaces;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Domain.Entities;

namespace BugStore.Application.Tests.Fakes.Services
{
    public class FakeProductsService : IProductsService
    {
        private readonly FakeProductsRepository _repo;

        public FakeProductsService(FakeProductsRepository repo)
        {
            _repo = repo;
        }

        public async Task<(Product? product, string message)> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(id, cancellationToken);
            var message = product == null ? "Product not found." : "Product found.";
            return (product, message);
        }

        public async Task<(List<Product> products, string message)> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _repo.GetAllAsync(cancellationToken);
            var message = products.Count == 0 ? "No products found." : "Products retrieved.";
            return (products, message);
        }

        public async Task<(Product? product, bool success, string message)> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            var exists = await _repo.GetBySlugAsync(product.Slug, cancellationToken);
            if (exists != null)
                return (exists, false, "Product already exists.");

            await _repo.AddAsync(product, cancellationToken);
            return (product, true, "Product created successfully.");
        }

        public async Task<(Product? product, bool success, string message)> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(product.Id, cancellationToken);
            if (existing == null)
                return (null, false, "Product not found.");

            existing.Title = product.Title ?? existing.Title;
            existing.Description = product.Description ?? existing.Description;
            existing.Slug = product.Slug ?? existing.Slug;
            existing.Price = product.Price;

            await _repo.UpdateAsync(existing, cancellationToken);
            return (existing, true, "Product updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(id, cancellationToken);
            if (product == null)
                return (false, "Product not found.");

            await _repo.DeleteAsync(product, cancellationToken);
            return (true, "Product deleted successfully.");
        }

        public async Task AddProductAsync(Product product)
        {
            await _repo.AddAsync(product, CancellationToken.None);
        }
    }
}
