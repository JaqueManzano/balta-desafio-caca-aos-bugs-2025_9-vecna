using BugStore.Application.Interfaces;
using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;

namespace BugStore.Infrastructure.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            _repository = repository;
        }

        public async Task<(Product? product, string message)> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return (null, "Invalid product ID.");

            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product == null)
                return (null, "Product not found.");

            return (product, "Product found.");
        }

        public async Task<(List<Product> products, string message)> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);

            if (products == null || products.Count == 0)
                return (new List<Product>(), "No registered products.");

            return (products, string.Empty);
        }

        public async Task<(Product? product, bool success, string message)> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetBySlugAsync(product.Slug, cancellationToken);
            if (exists != null)
                return (exists, false, "Product already registered.");

            await _repository.AddAsync(product, cancellationToken);
            return (product, true, "Product created successfully.");
        }

        public async Task<(Product? product, bool success, string message)> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var existingProduct = await _repository.GetByIdAsync(product.Id, cancellationToken);
            if (existingProduct == null)
                return (null, false, "Product not found.");

            existingProduct.Title = product.Title ?? existingProduct.Title;
            existingProduct.Description = product.Description ?? existingProduct.Description;
            existingProduct.Slug = product.Slug ?? existingProduct.Slug;
            existingProduct.Price = product.Price != default ? product.Price : existingProduct.Price;

            await _repository.UpdateAsync(existingProduct, cancellationToken);

            return (existingProduct, true, "Product updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product == null)
                return (false, "Product not found.");

            await _repository.DeleteAsync(product, cancellationToken);
            return (true, "Product deleted successfully.");
        }
    }
}
