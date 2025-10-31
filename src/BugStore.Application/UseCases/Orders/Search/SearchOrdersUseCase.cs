using BugStore.Domain.Interfaces;

namespace BugStore.Application.UseCases.Orders.Search;

public class SearchOrdersUseCase : ISearchOrdersUseCase
{
    private readonly IOrderRepository _orderRepository;

    public SearchOrdersUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Response>> ExecuteAsync(Request request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.SearchAsync(
            request.CustomerName,
            request.CustomerEmail,
            request.CustomerPhone,
            request.ProductTitle,
            request.ProductDescription,
            request.ProductSlug,
            request.ProductPriceStart > 0 ? request.ProductPriceStart : null,
            request.ProductPriceEnd > 0 ? request.ProductPriceEnd : null,
            request.CreatedAtStart != DateTime.MinValue ? request.CreatedAtStart : null,
            request.CreatedAtEnd != DateTime.MinValue ? request.CreatedAtEnd : null,
            request.UpdatedAtStart != DateTime.MinValue ? request.UpdatedAtStart : null,
            request.UpdatedAtEnd != DateTime.MinValue ? request.UpdatedAtEnd : null,
            cancellationToken
        );

        return orders.Select(o => new Response
        {
            Id = o.Id,
            CustomerName = o.Customer?.Name ?? string.Empty,
            CustomerEmail = o.Customer?.Email ?? string.Empty,
            CustomerPhone = o.Customer?.Phone ?? string.Empty,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt,
            Products = o.Lines.Select(l => new ProductResponse
            {
                Title = l.Product?.Title ?? string.Empty,
                Description = l.Product?.Description ?? string.Empty,
                Slug = l.Product?.Slug ?? string.Empty,
                Price = l.Product?.Price ?? 0,
                Quantity = l.Quantity,
                Total = l.Total
            }).ToList()
        });
    }
}
