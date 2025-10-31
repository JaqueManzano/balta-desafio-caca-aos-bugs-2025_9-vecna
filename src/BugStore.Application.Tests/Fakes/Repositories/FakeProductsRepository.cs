using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;

namespace BugStore.Application.Tests.Fakes.Repositories
{
    public class FakeProductsRepository : IProductsRepository
    {
        private readonly List<Product> _products = new();

        public Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
        {
            var product = _products.FirstOrDefault(p => p.Slug == slug);
            return Task.FromResult(product);
        }

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_products.ToList());
        }

        public Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index >= 0)
                _products[index] = product;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            _products.Remove(product);
            return Task.CompletedTask;
        }

        public Task<List<Product>> SearchAsync(string? title, string? description, string? slug, decimal? price, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
