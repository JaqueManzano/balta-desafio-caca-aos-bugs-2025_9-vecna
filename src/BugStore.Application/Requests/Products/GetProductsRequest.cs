using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Requests.Products;

public class GetProductsRequest : IRequest<GetProductsResponse>
{
}

