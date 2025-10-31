using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Products;
using BugStore.Application.Responses.Products;
using MediatR;

public class DeleteProductsHandler : IRequestHandler<DeleteProductsRequest, DeleteProductsResponse>
{
    private readonly IProductsService _service;

    public DeleteProductsHandler(IProductsService service)
    {
        _service = service;
    }

    public async Task<DeleteProductsResponse> Handle(DeleteProductsRequest request, CancellationToken cancellationToken)
    {
        var (success, message) = await _service.DeleteAsync(request.Id, cancellationToken);

        return new DeleteProductsResponse
        {
            Success = success,
            Message = message
        };
    }
}
