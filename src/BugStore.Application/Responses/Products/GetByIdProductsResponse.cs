using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Products;

public class GetByIdProductsResponse
{
    public ProductDto? Product { get; set; }
    public string Message { get; set; } = string.Empty;
}

