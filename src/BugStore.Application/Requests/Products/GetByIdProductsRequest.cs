using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Requests.Products;

public class GetByIdProductsRequest : IRequest<GetByIdProductsResponse>
{
    public Guid Id { get; set; }
}

