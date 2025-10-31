using BugStore.Domain.Interfaces;

namespace BugStore.Application.UseCases.Products.Search;

public class SearchProductsUseCase : ISearchProductsUseCase
{
    private readonly IProductsRepository _productRepository;

    public SearchProductsUseCase(IProductsRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Response>> ExecuteAsync(Request request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.SearchAsync(
            request.Title,
            request.Description,
            request.Slug,
            request.Price,
            cancellationToken
        );

        return products.Select(p => new Response
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Slug = p.Slug,
            Price = p.Price
        });
    }
}
