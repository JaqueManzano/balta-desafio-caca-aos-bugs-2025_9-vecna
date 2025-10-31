using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Requests.Products;

public class DeleteProductsRequest : IRequest<DeleteProductsResponse>
{
    public Guid Id { get; set; }
}

