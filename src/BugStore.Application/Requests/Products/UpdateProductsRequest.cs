using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Requests.Products;

public class UpdateProductsRequest : IRequest<UpdateProductsResponse>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public decimal? Price { get; set; }
}
