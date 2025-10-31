using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Requests.Products;

public class CreateProductsRequest : IRequest<CreateProductsResponse>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public decimal Price { get; set; }
}
